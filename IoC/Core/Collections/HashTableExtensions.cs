namespace IoC.Core.Collections
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal static class HashTableExtensions
    {
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static TValue Get<TKey, TValue>(this HashTable<TKey, TValue> hashTable, TKey key)
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
            var result = HashTable<TKey, TValue>.Empty;
            foreach (var keyValue in hashTable.Enumerate())
            {
                if (Equals(keyValue.Key, key))
                {
                    removed = true;
                    continue;
                }

                result = new HashTable<TKey, TValue>(result, keyValue.Key, keyValue.Value);
            }

            return result;
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
                foreach (var keyValue in bucket.Enumerate())
                {
                    yield return keyValue;
                }
            }
        }
    }
}