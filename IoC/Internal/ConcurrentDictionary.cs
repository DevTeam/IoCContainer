#if NETSTANDARD1_0
// ReSharper disable once CheckNamespace
namespace System.Collections.Concurrent
{
    using Generic;

    internal class ConcurrentDictionary<TKey, TValue>: IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            lock (_dictionary)
            {
                return _dictionary.GetEnumerator();
            }
        }

        internal TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            lock (_dictionary)
            {
                if (_dictionary.TryGetValue(key, out var value))
                {
                    return value;
                }

                value = factory(key);
                _dictionary.Add(key, value);
                return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_dictionary)
            {
                return ((IEnumerable) _dictionary).GetEnumerator();
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            lock (_dictionary)
            {
                _dictionary.Clear();
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        public int Count
        {
            get
            {
                lock (_dictionary)
                {
                    return _dictionary.Count;
                }
            }
        }

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            lock (_dictionary)
            {
                _dictionary.Add(key, value);
            }
        }

        public bool ContainsKey(TKey key)
        {
            lock (_dictionary)
            {
                return _dictionary.ContainsKey(key);
            }
        }

        public bool Remove(TKey key)
        {
            lock (_dictionary)
            {
                return _dictionary.Remove(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (_dictionary)
            {
                return _dictionary.TryGetValue(key, out value);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                lock (_dictionary)
                {
                    return _dictionary[key];
                }
            }

            set
            {
                lock (_dictionary)
                {
                    _dictionary[key] = value;
                }
            }
        }

        public ICollection<TKey> Keys {
            get
            {
                lock (_dictionary)
                {
                    return _dictionary.Keys;
                }
            }
        }

        public ICollection<TValue> Values {
            get
            {
                lock (_dictionary)
                {
                    return _dictionary.Values;
                }
            }
        }
    }
}
#endif
