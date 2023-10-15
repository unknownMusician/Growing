using System;

namespace Growing.Utils
{
    [Serializable]
    public struct SerializedPair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }
}
