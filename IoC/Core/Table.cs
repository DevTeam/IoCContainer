namespace IoC.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal class Table<TKey, TValue>: IEnumerable<Table<TKey, TValue>.KeyValue>
    {
        public static readonly Table<TKey, TValue> Empty = new Table<TKey, TValue>(CoreExtensions.CreateArray(4, Bucket.EmptyBucket), 3, 0);
        public readonly int Count;
        private readonly int _divisor;
        private readonly Bucket[] _buckets;

        [MethodImpl((MethodImplOptions)256)]
        private Table(Bucket[] buckets, int divisor, int count)
        {
            _buckets = buckets;
            _divisor = divisor;
            Count = count;
        }

        [MethodImpl((MethodImplOptions)256)]
        private Table(Table<TKey, TValue> origin, int hashCode, TKey key, TValue value)
        {
            int newBucketIndex;
            Count = origin.Count + 1;
            if (origin.Count > origin._divisor)
            {
                _divisor = (origin._divisor + 1) << 2 - 1;
                _buckets = CoreExtensions.CreateArray(_divisor + 1, Bucket.EmptyBucket);
                var originBuckets = origin._buckets;
                for (var originBucketIndex = 0; originBucketIndex < originBuckets.Length; originBucketIndex++)
                {
                    var originKeyValues = originBuckets[originBucketIndex].KeyValues;
                    for (var index = 0; index < originKeyValues.Length; index++)
                    {
                        var keyValue = originKeyValues[index];
                        newBucketIndex = keyValue.HashCode & _divisor;
                        _buckets[newBucketIndex] = _buckets[newBucketIndex].Add(keyValue);
                    }
                }
            }
            else
            {
                _divisor = origin._divisor;
                _buckets = origin._buckets.Copy();
            }

            newBucketIndex = hashCode & _divisor;
            _buckets[newBucketIndex] = _buckets[newBucketIndex].Add(new KeyValue(hashCode, key, value));
        }

        [MethodImpl((MethodImplOptions) 256)]
        [Pure]
        public Bucket GetBucket(int hashCode)
        {
            return _buckets[hashCode & _divisor];
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public IEnumerator<KeyValue> GetEnumerator()
        {
            for (var bucketIndex = 0; bucketIndex < _buckets.Length; bucketIndex++)
            {
                var keyValues = _buckets[bucketIndex].KeyValues;
                for (var index = 0; index < keyValues.Length; index++)
                {
                    var keyValue = keyValues[index];
                    yield return keyValue;
                }
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public Table<TKey, TValue> Set(int hashCode, TKey key, TValue value)
        {
            return new Table<TKey, TValue>(this, hashCode, key, value);
        }

        [Pure]
        public Table<TKey, TValue> Remove(int hashCode, TKey key, out bool removed)
        {
            removed = false;
            var newBuckets = CoreExtensions.CreateArray(_divisor + 1, Bucket.EmptyBucket);
            var newBucketsArray = newBuckets;
            var bucketIndex = hashCode & _divisor;
            for (var curBucketInex = 0; curBucketInex < _buckets.Length; curBucketInex++)
            {
                if (curBucketInex != bucketIndex)
                {
                    newBucketsArray[curBucketInex] = _buckets[curBucketInex].Copy();
                    continue;
                }

                // Bucket to remove an element
                var bucket = _buckets[bucketIndex];
                var keyValues = bucket.KeyValues;
                for (var index = 0; index < keyValues.Length; index++)
                {
                    var keyValue = keyValues[index];
                    // Remove the element
                    if (keyValue.HashCode == hashCode && (ReferenceEquals(keyValue.Key, key) || Equals(keyValue.Key, key)))
                    {
                        newBucketsArray[bucketIndex] = bucket.Remove(index);
                        removed = true;
                    }
                }
            }

            return new Table<TKey, TValue>(newBuckets, _divisor, removed ? Count - 1: Count);
        }

        internal struct KeyValue
        {
            public readonly int HashCode;
            public readonly TKey Key;
            public readonly TValue Value;

            [MethodImpl((MethodImplOptions)256)]
            public KeyValue(int hashCode, TKey key, TValue value)
            {
                HashCode = hashCode;
                Key = key;
                Value = value;
            }
        }

        internal struct Bucket
        {
            public static readonly Bucket EmptyBucket = new Bucket(0);
            public readonly KeyValue[] KeyValues;

            [MethodImpl((MethodImplOptions)256)]
            private Bucket(KeyValue[] keyValues)
            {
                KeyValues = keyValues.Length == 0 ? CoreExtensions.EmptyArray<KeyValue>() : keyValues.Copy();
            }

            [MethodImpl((MethodImplOptions)256)]
            private Bucket(int count)
            {
                KeyValues = CoreExtensions.CreateArray<KeyValue>(count);
            }

            [MethodImpl((MethodImplOptions)256)]
            public Bucket Copy()
            {
                return new Bucket(KeyValues);
            }

            [Pure]
            [MethodImpl((MethodImplOptions)256)]
            public Bucket Add(KeyValue keyValue)
            {
                return new Bucket(KeyValues.Add(keyValue));
            }

            [Pure]
            [MethodImpl((MethodImplOptions)256)]
            public Bucket Remove(int index)
            {
                var count = KeyValues.Length;
                var newBucket = new Bucket(count - 1);
                var newKeyValues = newBucket.KeyValues;
                var keyValues = KeyValues;
                for (var newIndex = 0; newIndex < index; newIndex++)
                {
                    newKeyValues[newIndex] = keyValues[newIndex];
                }

                for (var newIndex = index + 1; newIndex < count; newIndex++)
                {
                    newKeyValues[newIndex - 1] = keyValues[newIndex];
                }

                return newBucket;
            }
        }
    }
}