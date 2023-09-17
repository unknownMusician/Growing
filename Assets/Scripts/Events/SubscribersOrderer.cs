using System;

namespace Events
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
