using System;
using System.Collections.Generic;
using AreYouFruits.Nullability;

namespace Growing.Utils.ExternalPolymorphism
{
    public sealed class VirtualAction<TParameterBase>
    {
        private struct Entry
        {
            public object Action;
            public Action<TParameterBase> Wrapper;
        }
        
        private readonly Dictionary<Type, Entry> registered = new();

        public bool Register<T>(Action<T> action)
            where T : TParameterBase
        {
            var entry = new Entry
            {
                Action = action,
                Wrapper = value => action((T)value),
            };
            return registered.TryAdd(typeof(T), entry);
        }

        public bool Unregister<T>(Action<T> action)
            where T : TParameterBase
        {
            Type type = typeof(T);
            
            if (!registered.TryGetValue(type, out Entry entry))
            {
                return false;
            }

            if ((Action<T>)entry.Action != action)
            {
                return false;
            }

            registered.Remove(type);

            return true;
        }

        public bool Invoke<T>(T value)
            where T : TParameterBase
        {
            if (!CreateCache(value).TryGet(out Action<T> action))
            {
                return false;
            }

            action(value);
            return true;
        }
        
        public Optional<Action<TParameterBase>> CreateCache(TParameterBase value) => CreateCache(value.GetType());

        public Optional<Action<T>> CreateCache<T>(T value)
            where T : TParameterBase
        {
            if (CreateCacheSealed<T>().TryGet(out Action<T> action))
            {
                return action;
            }

            if (CreateCache(value.GetType()).TryGet(out Action<TParameterBase> cache))
            {
                return (Action<T>)(x => cache(x));
            }

            return default;
        }

        public Optional<Action<T>> CreateCache<T>()
            where T : TParameterBase
        {
            if (CreateCacheSealed<T>().TryGet(out Action<T> action))
            {
                return action;
            }

            if (CreateCache(typeof(T)).TryGet(out Action<TParameterBase> cache))
            {
                return (Action<T>)(x => cache(x));
            }

            return default;
        }

        public Optional<Action<TParameterBase>> CreateCache(Type type)
        {
            Optional<Type> optionalType = type;

            while (optionalType.TryGet(out Type notNullType))
            {
                if (registered.TryGetValue(notNullType, out Entry entry))
                {
                    return entry.Wrapper;
                }

                optionalType = notNullType.BaseType;
            }

            return default;
        }

        private Optional<Action<T>> CreateCacheSealed<T>()
            where T : TParameterBase
        {
            Type type = typeof(T);

            if (!type.IsSealed)
            {
                return default;
            }
            
            if (!registered.TryGetValue(type, out Entry entry))
            {
                throw new NoTypeRegisteredException(type);
            }

            return (Action<T>)entry.Action;
        }
    }
    
    public sealed class VirtualAction<TParameterBase, T1>
    {
        private struct Entry
        {
            public object Action;
            public Action<TParameterBase, T1> Wrapper;
        }
        
        private readonly Dictionary<Type, Entry> registered = new();

        public bool Register<T>(Action<T, T1> action)
            where T : TParameterBase
        {
            var entry = new Entry
            {
                Action = action,
                Wrapper = (p0, p1) => action((T)p0, p1),
            };
            return registered.TryAdd(typeof(T), entry);
        }

        public bool Unregister<T>(Action<T, T1> action)
            where T : TParameterBase
        {
            Type type = typeof(T);
            
            if (!registered.TryGetValue(type, out Entry entry))
            {
                return false;
            }

            if ((Action<T, T1>)entry.Action != action)
            {
                return false;
            }

            registered.Remove(type);

            return true;
        }

        public bool Invoke<T>(T p0, T1 p1)
            where T : TParameterBase
        {
            if (CreateCache(p0).TryGet(out Action<T, T1> action))
            {
                action(p0, p1);
                return true;
            }

            return false;
        }

        public Optional<Action<TParameterBase, T1>> CreateCache(TParameterBase value) => CreateCache(value.GetType());

        public Optional<Action<T, T1>> CreateCache<T>(T value)
            where T : TParameterBase
        {
            if (CreateCacheSealed<T>().TryGet(out Action<T, T1> action))
            {
                return action;
            }

            if (CreateCache(value.GetType()).TryGet(out Action<TParameterBase, T1> cache))
            {
                return (Action<T, T1>)((p0, p1) => cache(p0, p1));
            }

            return default;
        }

        public Optional<Action<T, T1>> CreateCache<T>()
            where T : TParameterBase
        {
            if (CreateCacheSealed<T>().TryGet(out Action<T, T1> action))
            {
                return action;
            }

            if (CreateCache(typeof(T)).TryGet(out Action<TParameterBase, T1> cache))
            {
                return (Action<T, T1>)((p0, p1) => cache(p0, p1));
            }

            return default;
        }

        public Optional<Action<TParameterBase, T1>> CreateCache(Type type)
        {
            Optional<Type> optionalType = type;

            while (optionalType.TryGet(out Type notNullType))
            {
                if (registered.TryGetValue(notNullType, out Entry entry))
                {
                    return entry.Wrapper;
                }

                optionalType = notNullType.BaseType;
            }

            return default;
        }

        private Optional<Action<T, T1>> CreateCacheSealed<T>()
            where T : TParameterBase
        {
            Type type = typeof(T);

            if (!type.IsSealed)
            {
                return default;
            }
            
            if (!registered.TryGetValue(type, out Entry entry))
            {
                throw new NoTypeRegisteredException(type);
            }

            return (Action<T, T1>)entry.Action;
        }
    }
}