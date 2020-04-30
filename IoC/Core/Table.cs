namespace IoC.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    
    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal sealed class Table<TKey, TValue>: IEnumerable<Table<TKey, TValue>.KeyValue>
    {
        private static readonly KeyValue[] EmptyBucket = CoreExtensions.EmptyArray<KeyValue>();
        public static readonly Table<TKey, TValue> Empty = new Table<TKey, TValue>(CoreExtensions.CreateArray(4, EmptyBucket), 3, 0);
        public readonly int Count;
        public readonly int Divisor;
        public readonly KeyValue[][] Buckets;

        private Table(KeyValue[][] buckets, int divisor, int count)
        {
            Buckets = buckets;
            Divisor = divisor;
            Count = count;
        }

        [MethodImpl((MethodImplOptions)256)]
        private Table(Table<TKey, TValue> origin, int hashCode, TKey key, TValue value)
        {
            int newBucketIndex;
            Count = origin.Count + 1;
            if (origin.Count > origin.Divisor)
            {
                Divisor = (origin.Divisor + 1) << 2 - 1;
                Buckets = CoreExtensions.CreateArray(Divisor + 1, EmptyBucket);
                var originBuckets = origin.Buckets;
                for (var originBucketIndex = 0; originBucketIndex < originBuckets.Length; originBucketIndex++)
                {
                    var originKeyValues = originBuckets[originBucketIndex];
                    for (var index = 0; index < originKeyValues.Length; index++)
                    {
                        var keyValue = originKeyValues[index];
                        newBucketIndex = keyValue.HashCode & Divisor;
                        Buckets[newBucketIndex] = Buckets[newBucketIndex].Add(keyValue);
                    }
                }
            }
            else
            {
                Divisor = origin.Divisor;
                Buckets = origin.Buckets.Copy();
            }

            newBucketIndex = hashCode & Divisor;
            Buckets[newBucketIndex] = Buckets[newBucketIndex].Add(new KeyValue(hashCode, key, value));
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public TValue Get(int hashCode, TKey key)
        {
            var items = this.Buckets[hashCode & this.Divisor];
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (Equals(key, item.Key))
                {
                    return item.Value;
                }
            }

            return default(TValue);
        }

        [Pure]
        public IEnumerator<KeyValue> GetEnumerator()
        {
            for (var bucketIndex = 0; bucketIndex < Buckets.Length; bucketIndex++)
            {
                var keyValues = Buckets[bucketIndex];
                for (var index = 0; index < keyValues.Length; index++)
                {
                    var keyValue = keyValues[index];
                    yield return keyValue;
                }
            }
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public Table<TKey, TValue> Set(int hashCode, TKey key, TValue value) =>
            new Table<TKey, TValue>(this, hashCode, key, value);

        [Pure]
        public Table<TKey, TValue> Remove(int hashCode, TKey key, out bool removed)
        {
            removed = false;
            var newBuckets = CoreExtensions.CreateArray(Divisor + 1, EmptyBucket);
            var newBucketsArray = newBuckets;
            var bucketIndex = hashCode & Divisor;
            for (var curBucketIndex = 0; curBucketIndex < Buckets.Length; curBucketIndex++)
            {
                var bucket = Buckets[curBucketIndex];
                if (curBucketIndex != bucketIndex)
                {
                    newBucketsArray[curBucketIndex] = bucket.Length == 0 ? EmptyBucket : bucket.Copy();
                    continue;
                }

                // Bucket to remove an element
                for (var index = 0; index < bucket.Length; index++)
                {
                    var keyValue = bucket[index];
                    // Remove the element
                    if (keyValue.HashCode == hashCode && (ReferenceEquals(keyValue.Key, key) || Equals(keyValue.Key, key)))
                    {
                        newBucketsArray[bucketIndex] = Remove(bucket, index);
                        removed = true;
                    }
                }
            }

            return new Table<TKey, TValue>(newBuckets, Divisor, removed ? Count - 1: Count);
        }

        [Pure]
        [MethodImpl((MethodImplOptions)256)]
        private static KeyValue[] Remove(KeyValue[] bucket, int index)
        {
            var count = bucket.Length;
            var newBucket = new KeyValue[count - 1];
            var keyValues = bucket;
            for (var newIndex = 0; newIndex < index; newIndex++)
            {
                newBucket[newIndex] = keyValues[newIndex];
            }

            for (var newIndex = index + 1; newIndex < count; newIndex++)
            {
                newBucket[newIndex - 1] = keyValues[newIndex];
            }

            return newBucket;
        }

        internal struct KeyValue
        {
            public readonly int HashCode;
            public readonly TKey Key;
            public readonly TValue Value;

            public KeyValue(int hashCode, TKey key, TValue value)
            {
                HashCode = hashCode;
                Key = key;
                Value = value;
            }
        }
    }
}