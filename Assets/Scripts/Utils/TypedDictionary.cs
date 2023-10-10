using System;
using System.Collections.Generic;
using AreYouFruits.Nullability;

namespace Growing.Utils
{
    public sealed class TypedDictionary<TBase>
    {
        private readonly Dictionary<Type, TBase> values;
        private readonly DispatchType addedTypesDispatch;
        
        public TypedDictionary(DispatchType addedTypesDispatch)
        {
            values = new Dictionary<Type, TBase>();
            this.addedTypesDispatch = addedTypesDispatch;
        }

        public Optional<T> Get<T>()
            where T : TBase
        {
            if (values.TryGetValue(typeof(T), out TBase value))
            {
                return (T)value;
            }

            return default;
        }

        public bool Add<T>(T value)
            where T : TBase
        {
            return values.TryAdd(GetType(value), value);
        }

        public bool Remove<T>()
            where T : TBase
        {
            return values.Remove(typeof(T));
        }

        private Type GetType<T>(T value)
            where T : TBase
        {
            Type type = typeof(T);

            if (addedTypesDispatch is DispatchType.Dynamic && !type.IsSealed)
            {
                type = value.GetType();
            }

            return type;
        }
    }
}
