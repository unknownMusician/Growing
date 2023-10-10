using Growing.Events;
using NUnit.Framework;

namespace Growing.Tests.EditMode.Events
{
    public sealed class EventBusTests
    {
        [Test]
        public void SubscribedHandler_SubscribedTypePassed_SubscriberInvoked()
        {
            // todo: should depend on interface
            var eventBus = new EventBus(new SubscribersOrdererMock
            {
                Order = _ => 0,
            });
            int handledValue = 0;
            const int firedValue = 5;

            eventBus.Subscribe<int>(this, x => handledValue = x);
            eventBus.Invoke<int>(firedValue);

            Assert.That(handledValue, Is.EqualTo(firedValue));
        }

        [Test]
        public void SubscribedHandler_SubscribedTypePassed_Ignored()
        {
            // todo: should depend on interface
            var eventBus = new EventBus(new SubscribersOrdererMock
            {
                Order = _ => 0,
            });
            const int initialValue = 0;
            const float firedValue = 5;
            int handledValue = initialValue;

            eventBus.Subscribe<int>(this, x => handledValue = x);
            eventBus.Invoke<float>(firedValue);

            Assert.That(handledValue, Is.EqualTo(initialValue));
        }


        // todo: ordering
    }
}
