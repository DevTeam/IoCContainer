namespace IoC.Core.Collections
{
    using System.Runtime.CompilerServices;

    internal static class HashTableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public static TValue Search<TValue>(this HashTable<Key, TValue> hashTable, Key key)
        {
            var hashCode = key.HashCode;
            var tree = hashTable.Buckets[hashCode & (hashTable.Divisor - 1)];
            while (tree.Height != 0 && tree.HashCode != hashCode)
            {
                tree = hashCode < tree.HashCode ? tree.Left : tree.Right;
            }

            if (tree.Height != 0 && (ReferenceEquals(tree.Key, key) || tree.Key.Equals(key)))
            {
                return tree.Value;
            }

            if (tree.Duplicates.Items.Length > 0)
            {
                foreach (var keyValue in tree.Duplicates.Items)
                {
                    if (ReferenceEquals(keyValue.Key, key) || keyValue.Key.Equals(key))
                    {
                        return keyValue.Value;
                    }
                }
            }

            return default(TValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashTable<TKey, TValue> Add<TKey, TValue>(this HashTable<TKey, TValue> hashTable, TKey key, TValue value)
        {
            return new HashTable<TKey, TValue>(hashTable, key, value);
        }
    }
}