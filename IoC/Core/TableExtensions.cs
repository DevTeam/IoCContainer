namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class TableExtensions
    {
        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public static bool TryGetByType<TValue>(this Table<Type, TValue> table, int hashCode, Type key, out TValue value)
        {
            var items = table.Buckets[hashCode & table.Divisor];
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (key == item.Key)
                {
                    value = item.Value;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public static bool TryGetByKey<TValue>(this Table<Key, TValue> table, int hashCode, Key key, out TValue value)
        {
            var items = table.Buckets[hashCode & table.Divisor];
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (CoreExtensions.Equals(key, item.Key))
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