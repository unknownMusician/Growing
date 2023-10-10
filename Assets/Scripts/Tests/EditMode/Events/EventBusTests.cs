using System;
using Growing.Events;
using NUnit.Framework;

namespace Growing.Tests.EditMode.Events
{
    public sealed class SubscribersOrdererMock : ISubscribersOrderer
    {
        private readonly Func<Type, int> order;

        public SubscribersOrdererMock(Func<Type, int> order)
        {
            this.order = order;
        }

        public int Order(Type subscriberType) => order(subscriberType);
    }

    public sealed class EventBusTests
    {
        [Test]
        public void SubscribedHandler_Invoked_WhenSameTypePassed()
        {
            // todo: should depend on interface
            var eventBus = new EventBus(new SubscribersOrdererMock(_ => 0));
            var handledValue = 0;
            const int firedValue = 5;

            eventBus.Subscribe<int>(this, x => handledValue = x);
            eventBus.Invoke<int>(firedValue);

            Assert.That(handledValue == firedValue);
        }

        [Test]
        public void SubscribedHandler_NotInvoked_WhenOtherTypePassed()
        {
            // todo: should depend on interface
            var eventBus = new EventBus(new SubscribersOrdererMock(_ => 0));
            const int initialValue = 0;
            const float firedValue = 5;
            var handledValue = initialValue;

            eventBus.Subscribe<int>(this, x => handledValue = x);
            eventBus.Invoke<float>(firedValue);

            Assert.That(handledValue == initialValue);
        }
        
        

        // todo: ordering
    }
}
