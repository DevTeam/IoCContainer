using System.Collections.Generic;
using System.Linq;

namespace IoC.Core.Collections
{
    // ReSharper disable once RedundantUsingDirective
    using System;
    using System.Runtime.CompilerServices;

    internal static class HashTableExtensions
    {
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static TValue Find<TKey, TValue>(this HashTable<TKey, TValue> hashTable, TKey key)
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
        public static HashTable<TKey, TValue> Remove<TKey, TValue>(this HashTable<TKey, TValue> hashTable, TKey key, out bool removed)
        {
            removed = false;
            foreach (var keyValue in hashTable.Enumerate())
            {
                if (Equals(keyValue.Key, key))
                {
                    removed = true;
                    continue;
                }

                hashTable = new HashTable<TKey, TValue>(hashTable, keyValue.Key, keyValue.Value);
            }

            return hashTable;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static IEnumerable<KeyValue<TKey, TValue>> Enumerate<TKey, TValue>(this HashTable<TKey, TValue> hashTable)
        {
            var buckets = hashTable.Buckets;
            for (var bucketIndex = 0; bucketIndex < buckets.Length; bucketIndex++)
            {
                var bucket = buckets[bucketIndex];
                if (bucket.IsEmpty)
                {
                    continue;
                }

                yield return new KeyValue<TKey, TValue>(bucket.Key, bucket.Value);

                var duplicates = bucket.Duplicates.Items;
                for (var duplicateIndex = 0; duplicateIndex < bucket.Duplicates.Count; duplicateIndex++)
                {
                    yield return duplicates[duplicateIndex];
                }
            }
        }
    }
}