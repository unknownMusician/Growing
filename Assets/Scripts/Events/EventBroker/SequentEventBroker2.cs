using System;
using System.Linq;
using AreYouFruits.Assertions;
using AreYouFruits.ConstructorGeneration;
using Growing.Utils;
using Growing.Utils.ExternalPolymorphism;
using Growing.Utils.SequentEvents;

namespace Growing.Events
{
    public sealed partial class SequentEventBroker<TMessageBase, T1>
    {
        private readonly VirtualAction<TMessageBase, T1> virtualAction = new();
        private readonly TypedDictionary<object> sequentActions = new(DispatchType.Static);
        
        [GenerateConstructor] private readonly bool allowSameOrder;

        public bool Invoke<TMessage>(TMessage message, T1 p1)
            where TMessage : TMessageBase
        {
            return virtualAction.Invoke(message, p1);
        }

        public bool Subscribe<TMessage>(Action<TMessage, T1> handler, int order = default)
            where TMessage : TMessageBase
        {
            if (sequentActions.Get<SequentDelegate<Action<TMessage, T1>, int>>().TryGet(out var sequentAction))
            {
                return sequentAction.Add(handler, order);
            }

            sequentAction = new(allowSameOrder: allowSameOrder);
            sequentAction.Add(handler, order).Expect(true);

            sequentActions.Add(sequentAction);
            virtualAction.Register<TMessage>(sequentAction.Invoke).Expect(true);

            return true;
        }

        public bool Unregister<TMessage>(Action<TMessage, T1> handler, int order = default)
            where TMessage : TMessageBase
        {
            if (!sequentActions.Get<SequentDelegate<Action<TMessage, T1>, int>>().TryGet(out var sequentAction))
            {
                return false;
            }

            sequentAction.Remove(handler, order).Expect(true);

            if (!sequentAction.All.Any())
            {
                sequentActions.Remove<SequentDelegate<Action<TMessage, T1>, int>>().Expect(true);
                virtualAction.Unregister<TMessage>(sequentAction.Invoke).Expect(true);
            }

            return true;
        }
    }
}
