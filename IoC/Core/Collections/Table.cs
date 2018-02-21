namespace IoC.Core.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class Table<TKey, TValue> : IEnumerable<Tree<TKey, TValue>.KeyValue>
    {
        public static readonly Table<TKey, TValue> Empty = new Table<TKey, TValue>();
        private readonly int _count;
        private readonly int _divisor;
        private readonly Tree<TKey, TValue>[] _buckets;

        private Table(Table<TKey, TValue> origin, int hashCode, TKey key, TValue value)
        {
            int bucketIndex;
            _count = origin._count + 1;
            if (origin._count >= origin._divisor)
            {
                _divisor = origin._divisor << 1;
                _buckets = new Tree<TKey, TValue>[_divisor];
                InitializeBuckets(0, _divisor);
                var originBuckets = origin._buckets;
                foreach (var originBucket in originBuckets)
                {
                    foreach (var keyValue in originBucket)
                    {
                        bucketIndex = CreateBucketIndex(keyValue.HashCode);
                        _buckets[bucketIndex] = _buckets[bucketIndex].Set(keyValue.HashCode, keyValue.Key, keyValue.Value);
                    }
                }
            }
            else
            {
                _divisor = origin._divisor;
                _buckets = new Tree<TKey, TValue>[_divisor];
                Array.Copy(origin._buckets, _buckets, origin._divisor);
            }

            bucketIndex = CreateBucketIndex(hashCode);
            _buckets[bucketIndex] = _buckets[bucketIndex].Set(hashCode, key, value);
        }

        private Table(Table<TKey, TValue> origin)
        {
            _divisor = origin._divisor;
            _count = origin._count;
            var length = origin._buckets.Length;
            _buckets = new Tree<TKey, TValue>[length];
            Array.Copy(origin._buckets, _buckets, length);
        }

        private Table()
        {
            _buckets = new Tree<TKey, TValue>[2];
            _divisor = 2;
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
            var bucketIndex = CreateBucketIndex(hashCode);
            var tree = _buckets[bucketIndex];
            return tree.TryGet(hashCode, key, out value);
        }

        [MethodImpl((MethodImplOptions)256)]
        public Table<TKey, TValue> Remove(int hashCode, TKey key)
        {
            var bucketIndex = CreateBucketIndex(hashCode);
            var newTable = new Table<TKey, TValue>(this);
            newTable._buckets[bucketIndex] = newTable._buckets[bucketIndex].Remove(hashCode, key);
            return newTable;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Tree<TKey, TValue>.KeyValue> GetEnumerator()
        {
            return _buckets.SelectMany(i => i).GetEnumerator();
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private void InitializeBuckets(int startIndex, int count)
        {
#if NETCOREAPP2_0
            Array.Fill(_buckets, Tree<TKey, TValue>.Empty);
#else
            for (var i = startIndex; i < count; i++)
            {
                _buckets[i] = Tree<TKey, TValue>.Empty;
            }
#endif
        }

        [MethodImpl((MethodImplOptions)256)]
        private int CreateBucketIndex(int hashCode)
        {
            return hashCode & (_divisor - 1);
        }
    }
}