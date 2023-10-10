using System;

namespace Growing.Events
{
    public interface ISubscribersOrderer
    {
        public int Order(Type subscriberType);
    }
}
