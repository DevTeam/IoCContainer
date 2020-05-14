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
        public static readonly Table<TKey, TValue> Empty = new Table<TKey, TValue>(CoreExtensions.CreateArray(4, EmptyBucket), 3, 0);
        public readonly int Count;
        public readonly int Divisor;
        public readonly Bucket[] Buckets;

        private Table(Bucket[] buckets, int divisor, int count)
        {
            Buckets = buckets;
            Divisor = divisor;
            Count = count;
        }

        [MethodImpl((MethodImplOptions)0x200)]
        private Table(Table<TKey, TValue> origin, TKey key, TValue value)
        {
            int newBucketIndex;
            Count = origin.Count + 1;
            if (origin.Count > origin.Divisor)
            {
                Divisor = (origin.Divisor + 1) * 4 - 1;
                Buckets = CoreExtensions.CreateArray(Divisor + 1, EmptyBucket);
                var originBuckets = origin.Buckets;
                for (var originBucketIndex = 0; originBucketIndex < originBuckets.Length; originBucketIndex++)
                {
                    var originBucket = originBuckets[originBucketIndex];
                    for (var index = 0; index < originBucket.KeyValues.Length; index++)
                    {
                        var keyValue = originBucket.KeyValues[index];
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

        [MethodImpl((MethodImplOptions)0x200)]
        [Pure]
        public TValue Get(TKey key)
        {
            var bucket = Buckets[key.GetHashCode() & Divisor];
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < bucket.KeyValues.Length; index++)
            {
                var item = bucket.KeyValues[index];
                if (ReferenceEquals(key, item.Key) || Equals(key, item.Key))
                {
                    return item.Value;
                }
            }

            return default(TValue);
        }

        [MethodImpl((MethodImplOptions)0x200)]
        [Pure]
        public IEnumerator<KeyValue> GetEnumerator()
        {
            for (var bucketIndex = 0; bucketIndex < Buckets.Length; bucketIndex++)
            {
                var bucket = Buckets[bucketIndex];
                for (var index = 0; index < bucket.KeyValues.Length; index++)
                {
                    yield return bucket.KeyValues[index];
                }
            }
        }

        [Pure]
        [MethodImpl((MethodImplOptions)0x100)]
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        [MethodImpl((MethodImplOptions)0x100)]
        [Pure]
        public Table<TKey, TValue> Set(TKey key, TValue value) =>
            new Table<TKey, TValue>(this, key, value);

        [MethodImpl((MethodImplOptions)0x200)]
        [Pure]
        public Table<TKey, TValue> Remove(TKey key, out bool removed)
        {
            removed = false;
            var newBuckets = CoreExtensions.CreateArray(Divisor + 1, EmptyBucket);
            var hashCode = key.GetHashCode();
            var bucketIndex = hashCode & Divisor;
            for (var curBucketIndex = 0; curBucketIndex < Buckets.Length; curBucketIndex++)
            {
                var bucket = Buckets[curBucketIndex];
                if (curBucketIndex != bucketIndex)
                {
                    newBuckets[curBucketIndex] = bucket.Copy();
                    continue;
                }

                // Bucket to remove an element
                for (var index = 0; index < bucket.KeyValues.Length; index++)
                {
                    var keyValue = bucket.KeyValues[index];
                    // Remove the element
                    if (keyValue.Key.GetHashCode() == hashCode && (ReferenceEquals(keyValue.Key, key) || Equals(keyValue.Key, key)))
                    {
                        newBuckets[bucketIndex] = bucket.Remove(index);
                        removed = true;
                    }
                }
            }

            return new Table<TKey, TValue>(newBuckets, Divisor, removed ? Count - 1: Count);
        }

        internal struct Bucket
        {
            public readonly KeyValue[] KeyValues;

            public Bucket(KeyValue[] keyValues) => KeyValues = keyValues;

            [MethodImpl((MethodImplOptions)0x100)]
            public Bucket Add(KeyValue keyValue) =>
                new Bucket(KeyValues.Add(keyValue));

            [MethodImpl((MethodImplOptions)0x100)]
            public Bucket Copy() =>
                KeyValues.Length == 0 ? EmptyBucket : new Bucket(KeyValues.Copy());

            [MethodImpl((MethodImplOptions)0x200)]
            public Bucket Remove(int index)
            {
                var newLeyValues = new KeyValue[KeyValues.Length - 1];
                for (var newIndex = 0; newIndex < index; newIndex++)
                {
                    newLeyValues[newIndex] = KeyValues[newIndex];
                }

                for (var newIndex = index + 1; newIndex < KeyValues.Length; newIndex++)
                {
                    newLeyValues[newIndex - 1] = KeyValues[newIndex];
                }

                return new Bucket(newLeyValues);
            }

            public override string ToString() => $"Bucket[{KeyValues.Length}]";
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