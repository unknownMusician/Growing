using System;
using System.Collections.Generic;
using System.Linq;
using Growing.Utils;

namespace Growing.Events
{
    public sealed class SubscribersOrderer : ISubscribersOrderer
    {
        private const int DefaultOrder = 0;
        
        private readonly Dictionary<Type, int> ordering;
        
        public SubscribersOrderer()
        {
            var types = new[]
            {
                typeof(int),
                typeof(float),
            };

            ordering = types
                .Select((t, i) => (t, i - types.Length))
                .ToDictionaryFromTuple();
        }

        public int Order(Type subscriberType)
        {
            if (ordering.TryGetValue(subscriberType, out var order))
            {
                return order;
            }

            return DefaultOrder;
        }
    }
}
