namespace IoC.Core.Collections
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class Extensions
    {
        [MethodImpl((MethodImplOptions)256)]
        public static bool SequenceEqual<T>([NotNull] this T[] array1, [NotNull] T[] array2)
        {
            return ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }

        [MethodImpl((MethodImplOptions)256)]
        public static int GetHash<T>([NotNull] this IEnumerable<T> items)
        {
            return items.Aggregate(0, (code, key) =>
            {
                unchecked
                {
                    return (code * 397) ^ key?.GetHashCode() ?? 0;
                }
            });
        }

        [MethodImpl((MethodImplOptions)256)]
        public static TValue FastGet<TKey, TValue>(this Table<TKey, TValue> table, int hashCode, TKey key)
            where TKey: class
        {
            var items = table.Buckets.Items[hashCode & (table.Divisor - 1)].KeyValues.Items;
            for (var index = 0; index < items.Length; index++)
            {
                var keyValue = items[index];
                if (keyValue.Key == key)
                {
                    return keyValue.Value;
                }
            }

            return default(TValue);
        }
    }
}
