using System;
using System.Collections.Generic;
using Growing.Events;
using NUnit.Framework;

namespace Growing.Tests.EditMode.Events
{
    public sealed class EventBusTests
    {
        [Test]
        public void SubscribedHandler_SubscribedTypePassed_SubscriberInvoked()
        {
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
        public void SubscribedHandler_OtherTypePassed_Ignored()
        {
            var eventBus = new EventBus(new SubscribersOrdererMock
            {
                Order = _ => 0,
            });
            const int initialValue = 0;
            const float firedValue = 5;
            var handledValue = initialValue;

            eventBus.Subscribe<int>(this, x => handledValue = x);
            eventBus.Invoke<float>(firedValue);

            Assert.That(handledValue, Is.EqualTo(initialValue));
        }

        [Test]
        public void UnsubscribedHandler_SubscribedTypePassed_Ignored()
        {
            var eventBus = new EventBus(new SubscribersOrdererMock
            {
                Order = _ => 0,
            });

            bool isChanged = false;

            Action<int> handler = _ => isChanged = true;
            
            eventBus.Subscribe<int>(this, handler);
            eventBus.Unsubscribe<int>(this, handler);
            
            eventBus.Invoke(5);
            
            Assert.That(!isChanged);
        }

        [Test]
        public void OrderingWorks()
        {
            var ordering = new Dictionary<Type, int>
            {
                [typeof(long)] = 1,
                [typeof(float)] = 2,
                [typeof(string)] = 3,
            };
            var eventBus = new EventBus(new SubscribersOrdererMock
            {
                Order = t => ordering[t],
            });
            bool result = true;
            int v = 0;
            eventBus.Subscribe<double>((long)5, _ => result &= ordering[typeof(long)] == (v += 1));
            eventBus.Subscribe<double>((float)5, _ => result &= ordering[typeof(float)] == (v += 1));
            eventBus.Subscribe<double>((string)"5", _ => result &= ordering[typeof(string)] == (v += 1));
            
            eventBus.Invoke((double)2);
            
            Assert.That(result);
        }
    }
}
