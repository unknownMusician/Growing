using System;
using AreYouFruits.ConstructorGeneration;

namespace Growing.Events.Authorization
{
    public readonly partial struct EventBusAuthorisedAccess
    {
        [GenerateConstructor] private readonly EventBus eventBus;
        [GenerateConstructor] private readonly object subscriber;

        public bool Subscribe<T>(Action<T> handler)
        {
            ThrowIfInvalid();
            return eventBus.Subscribe(subscriber, handler);
        }

        public bool Unsubscribe<T>(Action<T> handler)
        {
            ThrowIfInvalid();
            return eventBus.Unsubscribe(subscriber, handler);
        }

        private void ThrowIfInvalid()
        {
            if (eventBus is null || subscriber is null)
            {
                throw new InvalidOperationException($"Invalid {nameof(EventBusAuthorisedAccess)} usage.");
            }
        }
    }
}
