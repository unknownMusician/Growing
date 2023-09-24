using System;
using System.Collections.Generic;
using AreYouFruits.Nullability;

namespace Growing.Utils.ExternalPolymorphism
{
    public sealed class VirtualFunc<TParameterBase, TResult>
    {
        private readonly Dictionary<Type, Func<TParameterBase, TResult>> registered = new();

        public void Register<T>(Func<T, TResult> func)
            where T : TParameterBase
        {
            registered[typeof(T)] = value => func((T)value);
        }

        public TResult Invoke(TParameterBase value)
        {
            return CreateCache(value).Invoke(value);
        }

        public Func<TParameterBase, TResult> CreateCache(Type type)
        {
            Optional<Type> optionalType = type;

            while (optionalType.TryGet(out type))
            {
                if (registered.TryGetValue(type, out Func<TParameterBase, TResult> func))
                {
                    return func;
                }

                optionalType = type.BaseType;
            }

            throw new NoTypeRegisteredException(type);
        }

        public Func<TParameterBase, TResult> CreateCache(TParameterBase value)
        {
            return CreateCache(value.GetType());
        }

        public Func<T, TResult> CreateCache<T>()
            where T : TParameterBase
        {
            Func<TParameterBase, TResult> cache = CreateCache(typeof(T));
            return value => cache(value);
        }
    }
}