using System;
using Growing.Events;

namespace Growing.Tests.EditMode.Events
{
    public struct SubscribersOrdererMock : ISubscribersOrderer
    {
        public Func<Type, int> Order { get; set; }

        int ISubscribersOrderer.Order(Type subscriberType) => Order(subscriberType);
    }
}
