using System;
using System.Collections.Generic;
using System.Linq;

namespace Growing.Utils.SequentEvents
{
    public static class SequentDelegateExtensions
    {
        public static void Invoke<TOrder>(this SequentDelegate<Action, TOrder> sequentDelegate)
            where TOrder : struct
        {
            Invoke(sequentDelegate, x => x());
        }
        
        public static void Invoke<TOrder, T0>(this SequentDelegate<Action<T0>, TOrder> sequentDelegate, T0 p0)
            where TOrder : struct
        {
            Invoke(sequentDelegate, x => x(p0));
        }
        
        public static void Invoke<TOrder, T0, T1>(this SequentDelegate<Action<T0, T1>, TOrder> sequentDelegate, T0 p0, T1 p1)
            where TOrder : struct
        {
            Invoke(sequentDelegate, x => x(p0, p1));
        }
        
        public static void Invoke<TOrder, T0, T1, T2>(this SequentDelegate<Action<T0, T1, T2>, TOrder> sequentDelegate, T0 p0, T1 p1, T2 p2)
            where TOrder : struct
        {
            Invoke(sequentDelegate, x => x(p0, p1, p2));
        }
        
        public static void Invoke<TOrder, T0, T1, T2, T3>(this SequentDelegate<Action<T0, T1, T2, T3>, TOrder> sequentDelegate, T0 p0, T1 p1, T2 p2, T3 p3)
            where TOrder : struct
        {
            Invoke(sequentDelegate, x => x(p0, p1, p2, p3));
        }
        
        private static void Invoke<TDelegate, TOrder>(this SequentDelegate<TDelegate, TOrder> sequentDelegate, 
        Action<TDelegate> invoker)
            where TDelegate : Delegate
            where TOrder : struct
        {
            var exceptions = new List<Exception>();
            
            // todo: ToArray is less memory-efficient, but allows collection change while iterating
            foreach (TDelegate @delegate in sequentDelegate.All.ToArray())
            {
                try
                {
                    invoker(@delegate);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}