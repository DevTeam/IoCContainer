namespace IoC.Core.Collections
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;

    internal sealed class HashTable<TKey, TValue>
    {
        public static readonly HashTable<TKey, TValue> Empty = new HashTable<TKey, TValue>();
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly int Count;
        internal readonly HashTree<TKey, TValue>[] Buckets;
        internal readonly int Divisor;

        internal HashTable(HashTable<TKey, TValue> previous, TKey key, TValue value)
        {
            Count = previous.Count + 1;
            if (previous.Count >= previous.Divisor)
            {
                Divisor = previous.Divisor * 2;
                Buckets = new HashTree<TKey, TValue>[Divisor];
                InitializeBuckets(0, Divisor);
                AddExistingValues(previous);
            }
            else
            {
                Divisor = previous.Divisor;
                Buckets = new HashTree<TKey, TValue>[Divisor];
                Array.Copy(previous.Buckets, Buckets, previous.Divisor);
            }

            var hashCode = key.GetHashCode();
            var bucketIndex = hashCode & (Divisor - 1);
            Buckets[bucketIndex] = Buckets[bucketIndex].Add(key, value);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private HashTable()
        {
            Buckets = new HashTree<TKey, TValue>[2];
            Divisor = 2;
            InitializeBuckets(0, 2);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void AddExistingValues(HashTable<TKey, TValue> previous)
        {
            for (var index = 0; index < previous.Buckets.Length; index++)
            {
                var bucket = previous.Buckets[index];
                foreach (var keyValue in bucket.Enumerate())
                {
                    var hashCode = keyValue.Key.GetHashCode();
                    var bucketIndex = hashCode & (Divisor - 1);
                    Buckets[bucketIndex] = Buckets[bucketIndex].Add(keyValue.Key, keyValue.Value);
                }
            }
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void InitializeBuckets(int startIndex, int count)
        {
            for (var i = startIndex; i < count; i++)
            {
                Buckets[i] = HashTree<TKey, TValue>.Empty;
            }
        }
    }
}