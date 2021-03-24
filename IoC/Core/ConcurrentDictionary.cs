#if NETSTANDARD1_0
// ReSharper disable once CheckNamespace
namespace System.Collections.Concurrent
{
    using Generic;
    using Linq;

    internal class ConcurrentDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> factoryFunc)
        {
            lock (_dict)
            {
                if (!_dict.TryGetValue(key, out var value))
                {
                    value = factoryFunc(key);
                    _dict.Add(key, value);
                }

                return value;
            }
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            lock (_dict)
            {
                if (_dict.TryGetValue(key, out value))
                {
                    _dict.Remove(key);
                    return true;
                }

                return false;
            }
        }

        public KeyValuePair<TKey, TValue>[] ToArray()
        {
            lock (_dict)
            {
                return _dict.ToArray();
            }
        }
    }
}
#endif