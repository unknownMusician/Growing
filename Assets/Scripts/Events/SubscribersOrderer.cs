using System;

namespace Growing.Events
{
    public sealed class SubscribersOrderer
    {
        public int Order(Type subscriberType)
        {
            // todo
            return subscriberType.GetHashCode();
        }
    }
}
