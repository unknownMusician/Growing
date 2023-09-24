using System;
using System.Collections.Generic;
using System.Linq;
using AreYouFruits.ConstructorGeneration;
using Growing.Utils;

namespace Growing.Events
{
    public sealed partial class StraightStaticEventBus
        // todo: unusable but shows how EventBus (in EventBus.cs) works
    {
        private readonly bool allowSameOrder;
        private readonly TypedDictionary<object> handlers = new(DispatchType.Static);

        [GenerateConstructor] private readonly SubscribersOrderer subscribersOrderer;

        public bool Subscribe<T>(object subscriber, Action<T> handler)
        {
            int order = Order(subscriber);

            if (!handlers.Get<SortedDictionary<int, List<Action<T>>>>().TryGet(out var sequence))
            {
                sequence = new();
                handlers.Add(sequence);
            }

            if (!sequence.TryGetValue(order, out var sameOrderHandlers))
            {
                sameOrderHandlers = new();
                sequence.Add(order, sameOrderHandlers);
            }
            else if (allowSameOrder)
            {
                return false;
            }

            sameOrderHandlers.Add(handler);

            return true;
        }

        public bool Unsubscribe<T>(object subscriber, Action<T> handler)
        {
            int order = Order(subscriber);

            if (!handlers.Get<SortedDictionary<int, List<Action<T>>>>().TryGet(out var sequence))
            {
                return false;
            }
            
            if (!sequence.TryGetValue(order, out var sameOrderHandlers))
            {
                return false;
            }

            sameOrderHandlers.Remove(handler);

            if (!sameOrderHandlers.Any())
            {
                handlers.Remove<SortedDictionary<int, List<Action<T>>>>();
            }

            return true;
        }

        public void Invoke<T>(T message)
        {
            if (!handlers.Get<SortedDictionary<int, List<Action<T>>>>().TryGet(out var sequence))
            {
                return;
            }

            foreach (List<Action<T>> sameOrderHandlers in sequence.Values.ToArray())
            {
                foreach (Action<T> handler in sameOrderHandlers)
                {
                    handler(message);
                }
            }
        }

        private int Order(object subscriber)
        {
            return subscribersOrderer.Order(subscriber.GetType());
        }
    }
}
