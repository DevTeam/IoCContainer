namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class TableExtensions
    {
        [MethodImpl((MethodImplOptions)0x200)]
        [Pure]
        public static bool TryGetByType<TValue>(this Table<Type, TValue> table, Type key, out TValue value)
        {
            var bucket = table.Buckets[key.GetHashCode() & table.Divisor];
            foreach (var item in bucket.KeyValues)
            {
                if (key == item.Key)
                {
                    value = item.Value;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        [MethodImpl((MethodImplOptions)0x200)]
        [Pure]
        public static bool TryGetByKey<TValue>(this Table<Key, TValue> table, Key key, out TValue value)
        {
            var bucket = table.Buckets[key.GetHashCode() & table.Divisor];
            foreach (var item in bucket.KeyValues)
            {
                if (key.Equals(item.Key))
                {
                    value = item.Value;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }
    }
}