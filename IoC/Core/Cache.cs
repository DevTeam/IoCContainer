namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using Collections;

    internal class Cache<TKey, TValue>
    {
        [NotNull] private readonly object _lockObject = new object();
        [NotNull] private Table<TKey, TValue> _table = Table<TKey, TValue>.Empty;

        [MethodImpl((MethodImplOptions)256)]
        public TValue GetOrCreate(TKey key, Func<TValue> factory)
        {
            var hashCode = key.GetHashCode();
            lock (_lockObject)
            {
                var value = _table.Get(hashCode, key);
                if (Equals(value, default(TValue)))
                {
                    value = factory();
                    _table = _table.Set(hashCode, key, value);
                }

                return value;
            }
        }
    }
}
