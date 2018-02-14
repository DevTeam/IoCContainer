using System.Collections.Generic;
using System.Linq;

namespace IoC.Core.Collections
{
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;

    internal static class HashTableExtensions
    {
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static TValue Search<TKey, TValue>(this HashTable<TKey, TValue> hashTable, TKey key)
        {
            var hashCode = key.GetHashCode();
            var bucketIndex = hashCode & (hashTable.Divisor - 1);
            var tree = hashTable.Buckets[bucketIndex];

            while (tree.Height != 0 && tree.HashCode != hashCode)
            {
                tree = hashCode < tree.HashCode ? tree.Left : tree.Right;
            }

            if (tree.Height != 0 && (ReferenceEquals(tree.Key, key) || Equals(tree.Key, key)))
            {
                return tree.Value;
            }

            if (tree.Duplicates.Items.Length > 0)
            {
                foreach (var keyValue in tree.Duplicates.Items)
                {
                    if (ReferenceEquals(keyValue.Key, key) || Equals(keyValue.Key, key))
                    {
                        return keyValue.Value;
                    }
                }
            }

            return default(TValue);
        }


#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static HashTable<TKey, TValue> Add<TKey, TValue>(this HashTable<TKey, TValue> hashTable, TKey key, TValue value)
        {
            return new HashTable<TKey, TValue>(hashTable, key, value);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static IEnumerable<KeyValue<TKey, TValue>> Enumerate<TKey, TValue>(this HashTable<TKey, TValue> hashTable)
        {
            return hashTable.Buckets.SelectMany(t => t.Duplicates.Items);
        }
    }
}