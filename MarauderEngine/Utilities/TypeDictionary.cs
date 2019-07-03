using System;
using System.Collections.Generic;

namespace MarauderEngine.Utilities
{
    public class TypeDictionary<TValue> : Dictionary<Type, TValue>
    {

        public TValue Get<T>()
        {
            return this[typeof(T)];
        }

        public TValue Add<T>(TValue value)
        {
            Add(typeof(T), value);

            return Get<T>();
        }

        public void ForceAdd<T>(TValue value)
        {
            Add(value.GetType(), value);

        }

        public bool Remove<T>()
        {
            return Remove(typeof(T));
        }

        public bool TryGetValue<T>(out TValue value)
        {
            return TryGetValue(typeof(T), out value);
        }

        public bool ContainsKey<T>()
        {
            return ContainsKey(typeof(T));
        }
    }
}
