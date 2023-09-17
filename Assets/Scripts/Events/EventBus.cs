using System;
using AreYouFruits.ConstructorGeneration;

namespace Events
{
    public sealed partial class EventBus
    {
        private readonly SequentEventBroker<object> sequentEventBroker = new(allowSameOrder: false);
        
        [GenerateConstructor] private readonly SubscribersOrderer subscribersOrderer;

        public bool Subscribe<T>(object subscriber, Action<T> handler)
        {
            int order = subscribersOrderer.Order(subscriber.GetType());
            return sequentEventBroker.Subscribe(handler, order);
        }

        public bool Unsubscribe<T>(object subscriber, Action<T> handler)
        {
            int order = subscribersOrderer.Order(subscriber.GetType());
            return sequentEventBroker.Unsubscribe(handler, order);
        }

        public void Invoke<T>(T message)
        {
            sequentEventBroker.Invoke(message);
        }
    }
}
