namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class Cache<TKey, TValue>
        where TKey: class
    {
        [NotNull] private readonly Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

        [MethodImpl((MethodImplOptions)256)]
        public TValue GetOrCreate(TKey key, Func<TValue> factory)
        {
            lock (_dict)
            {
                if (_dict.TryGetValue(key, out var val))
                {
                    return val;
                }

                val = factory();
                _dict.Add(key, val);
                return val;
            }
        }
    }
}
