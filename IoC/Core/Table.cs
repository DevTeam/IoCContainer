namespace IoC.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    
    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal sealed class Table<TKey, TValue>: IEnumerable<Table<TKey, TValue>.KeyValue>
    {
        private static readonly Bucket EmptyBucket = new Bucket(CoreExtensions.EmptyArray<KeyValue>());
        public static readonly Table<TKey, TValue> Empty = new Table<TKey, TValue>(CoreExtensions.CreateArray(8, EmptyBucket), 7, 0);
        public readonly int Count;
        public readonly int Divisor;
        public readonly Bucket[] Buckets;

        private Table(Bucket[] buckets, int divisor, int count)
        {
            Buckets = buckets;
            Divisor = divisor;
            Count = count;
        }

        [MethodImpl((MethodImplOptions)256)]
        private Table(Table<TKey, TValue> origin, TKey key, TValue value)
        {
            int newBucketIndex;
            Count = origin.Count + 1;
            if (origin.Count > origin.Divisor)
            {
                Divisor = (origin.Divisor + 1) << 3 - 1;
                Buckets = CoreExtensions.CreateArray(Divisor + 1, EmptyBucket);
                var originBuckets = origin.Buckets;
                for (var originBucketIndex = 0; originBucketIndex < originBuckets.Length; originBucketIndex++)
                {
                    var originKeyValues = originBuckets[originBucketIndex];
                    for (var index = 0; index < originKeyValues.Length; index++)
                    {
                        var keyValue = originKeyValues.KeyValues[index];
                        newBucketIndex = keyValue.Key.GetHashCode() & Divisor;
                        Buckets[newBucketIndex] = Buckets[newBucketIndex].Add(keyValue);
                    }
                }
            }
            else
            {
                Divisor = origin.Divisor;
                Buckets = origin.Buckets.Copy();
            }

            newBucketIndex = key.GetHashCode() & Divisor;
            Buckets[newBucketIndex] = Buckets[newBucketIndex].Add(new KeyValue(key, value));
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public TValue Get(TKey key)
        {
            var bucket = Buckets[key.GetHashCode() & Divisor];
            if (Equals(key, bucket.FirstKey))
            {
                return bucket.FirstValue;
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 1; index < bucket.Length; index++)
            {
                var item = bucket.KeyValues[index];
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
                var bucket = Buckets[bucketIndex];
                for (var index = 0; index < bucket.Length; index++)
                {
                    yield return bucket.KeyValues[index];
                }
            }
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public Table<TKey, TValue> Set(TKey key, TValue value) =>
            new Table<TKey, TValue>(this, key, value);

        [Pure]
        public Table<TKey, TValue> Remove(TKey key, out bool removed)
        {
            removed = false;
            var newBuckets = CoreExtensions.CreateArray(Divisor + 1, EmptyBucket);
            var newBucketsArray = newBuckets;
            var hashCode = key.GetHashCode();
            var bucketIndex = hashCode & Divisor;
            for (var curBucketIndex = 0; curBucketIndex < Buckets.Length; curBucketIndex++)
            {
                var bucket = Buckets[curBucketIndex];
                if (curBucketIndex != bucketIndex)
                {
                    newBucketsArray[curBucketIndex] = bucket.Copy();
                    continue;
                }

                // Bucket to remove an element
                for (var index = 0; index < bucket.Length; index++)
                {
                    var keyValue = bucket.KeyValues[index];
                    // Remove the element
                    if (keyValue.Key.GetHashCode() == hashCode && (ReferenceEquals(keyValue.Key, key) || Equals(keyValue.Key, key)))
                    {
                        newBucketsArray[bucketIndex] = bucket.Remove(index);
                        removed = true;
                    }
                }
            }

            return new Table<TKey, TValue>(newBuckets, Divisor, removed ? Count - 1: Count);
        }

        internal struct Bucket
        {
            public readonly KeyValue[] KeyValues;
            public readonly int Length;
            public readonly TKey FirstKey;
            public readonly TValue FirstValue;

            public Bucket(KeyValue[] keyValues)
            {
                KeyValues = keyValues;
                Length = keyValues.Length;
                if (Length > 0)
                {
                    var item = keyValues[0];
                    FirstKey = item.Key;
                    FirstValue = item.Value;
                }
                else
                {
                    FirstKey = default(TKey);
                    FirstValue = default(TValue);
                }
            }

            [MethodImpl((MethodImplOptions)256)]
            public Bucket Add(KeyValue keyValue) =>
                new Bucket(KeyValues.Add(keyValue));

            [MethodImpl((MethodImplOptions)256)]
            public Bucket Copy() =>
                Length == 0 ? EmptyBucket : new Bucket(KeyValues.Copy());

            [MethodImpl((MethodImplOptions)256)]
            public Bucket Remove(int index)
            {
                var newLeyValues = new KeyValue[Length - 1];
                for (var newIndex = 0; newIndex < index; newIndex++)
                {
                    newLeyValues[newIndex] = KeyValues[newIndex];
                }

                for (var newIndex = index + 1; newIndex < Length; newIndex++)
                {
                    newLeyValues[newIndex - 1] = KeyValues[newIndex];
                }

                return new Bucket(newLeyValues);
            }
        }

        internal struct KeyValue
        {
            public readonly TKey Key;
            public readonly TValue Value;

            public KeyValue(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}