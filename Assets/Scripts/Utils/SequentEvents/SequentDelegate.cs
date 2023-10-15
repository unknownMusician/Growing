using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Growing.Utils.SequentEvents
{
    public sealed class SequentDelegate<TDelegate, TOrder> : IEnumerable<TDelegate>
        where TDelegate : Delegate
        where TOrder : struct
    {
        private readonly SortedDictionary<TOrder, List<TDelegate>> delegates;
        private readonly bool allowSameOrder;

        public IEnumerable<TDelegate> All => delegates.Values.SelectMany(actionsValue => actionsValue);

        public SequentDelegate(bool allowSameOrder = true)
        {
            delegates = new SortedDictionary<TOrder, List<TDelegate>>();
            this.allowSameOrder = allowSameOrder;
        }

        public bool Add(TDelegate @delegate, TOrder order = default)
        {
            if (!delegates.TryGetValue(order, out var sameOrderActions))
            {
                sameOrderActions = new List<TDelegate> { @delegate };
                delegates.Add(order, sameOrderActions);

                return true;
            }

            if (!allowSameOrder)
            {
                return false;
            }

            sameOrderActions.Add(@delegate);
            return true;
        }

        public bool Remove(TDelegate @delegate, TOrder order = default)
        {
            if (!delegates.TryGetValue(order, out var sameOrderActions))
            {
                return false;
            }

            if (!sameOrderActions.Remove(@delegate))
            {
                return false;
            }

            if (!sameOrderActions.Any())
            {
                delegates.Remove(order);
            }

            return true;

        }

        public void Clear() => delegates.Clear();

        public IEnumerator<TDelegate> GetEnumerator() => All.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}