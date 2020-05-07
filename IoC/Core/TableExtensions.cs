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
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < bucket.KeyValues.Length; index++)
            {
                var item = bucket.KeyValues[index];
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
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < bucket.KeyValues.Length; index++)
            {
                var item = bucket.KeyValues[index];
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