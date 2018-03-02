namespace IoC.Core.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal sealed class Table<TKey, TValue> : IEnumerable<Tree<TKey, TValue>.KeyValue>
    {
        public static readonly Table<TKey, TValue> Empty = new Table<TKey, TValue>();
        public readonly int Count;
        public readonly int Divisor;
        public readonly Tree<TKey, TValue>[] Buckets;

        private Table(Table<TKey, TValue> origin, int hashCode, TKey key, TValue value)
        {
            int bucketIndex;
            Count = origin.Count + 1;
            if (origin.Count >= origin.Divisor)
            {
                Divisor = origin.Divisor << 1;
                Buckets = new Tree<TKey, TValue>[Divisor];
                InitializeBuckets(0, Divisor);
                var originBuckets = origin.Buckets;
                foreach (var originBucket in originBuckets)
                {
                    foreach (var keyValue in originBucket)
                    {
                        bucketIndex = keyValue.HashCode & (Divisor - 1);
                        Buckets[bucketIndex] = Buckets[bucketIndex].Set(keyValue.HashCode, keyValue.Key, keyValue.Value);
                    }
                }
            }
            else
            {
                Divisor = origin.Divisor;
                Buckets = new Tree<TKey, TValue>[Divisor];
                Array.Copy(origin.Buckets, Buckets, origin.Divisor);
            }

            bucketIndex = hashCode & (Divisor - 1);
            Buckets[bucketIndex] = Buckets[bucketIndex].Set(hashCode, key, value);
        }

        private Table(Table<TKey, TValue> origin)
        {
            Divisor = origin.Divisor;
            Count = origin.Count;
            var length = origin.Buckets.Length;
            Buckets = new Tree<TKey, TValue>[length];
            Array.Copy(origin.Buckets, Buckets, length);
        }

        private Table()
        {
            Buckets = new Tree<TKey, TValue>[2];
            Divisor = 2;
            InitializeBuckets(0, 2);
        }

        [MethodImpl((MethodImplOptions)256)]
        public Table<TKey, TValue> Set(int hashCode, TKey key, TValue value)
        {
            return new Table<TKey, TValue>(this, hashCode, key, value);
        }

        [MethodImpl((MethodImplOptions)256)]
        public bool TryGet(int hashCode, TKey key, out TValue value)
        {
            return Buckets[hashCode & (Divisor - 1)].TryGet(hashCode, key, out value);
        }

        [MethodImpl((MethodImplOptions)256)]
        public Table<TKey, TValue> Remove(int hashCode, TKey key)
        {
            var bucketIndex = hashCode & (Divisor - 1);
            var newTable = new Table<TKey, TValue>(this);
            newTable.Buckets[bucketIndex] = newTable.Buckets[bucketIndex].Remove(hashCode, key);
            return newTable;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Tree<TKey, TValue>.KeyValue> GetEnumerator()
        {
            return Buckets.SelectMany(i => i).GetEnumerator();
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private void InitializeBuckets(int startIndex, int count)
        {
#if NETCOREAPP2_0
            Array.Fill(Buckets, Tree<TKey, TValue>.Empty);
#else
            for (var i = startIndex; i < count; i++)
            {
                Buckets[i] = Tree<TKey, TValue>.Empty;
            }
#endif
        }
    }
}