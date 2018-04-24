namespace IoC.Core.Collections
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal sealed class Table<TKey, TValue>: IEnumerable<KeyValue<TKey, TValue>>
    {
        public static readonly Table<TKey, TValue> Empty = new Table<TKey, TValue>(ResizableArray<Bucket>.Create(4, Bucket.EmptyBucket), 4, 0);
        public readonly int Divisor;
        public readonly ResizableArray<Bucket> Buckets;
        public readonly int Count;

        [MethodImpl((MethodImplOptions)256)]
        private Table(ResizableArray<Bucket> buckets, int divisor, int count)
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
            if (origin.Count >= origin.Divisor)
            {
                Divisor = origin.Divisor << 1;
                Buckets = ResizableArray<Bucket>.Create(Divisor, Bucket.EmptyBucket);
                var originBuckets = origin.Buckets.Items;
                for (var originBucketIndex = 0; originBucketIndex < originBuckets.Length; originBucketIndex++)
                {
                    var originKeyValues = originBuckets[originBucketIndex].KeyValues.Items;
                    for (var index = 0; index < originKeyValues.Length; index++)
                    {
                        var keyValue = originKeyValues[index];
                        newBucketIndex = keyValue.HashCode & (Divisor - 1);
                        Buckets.Items[newBucketIndex] = Buckets.Items[newBucketIndex].Add(keyValue);
                    }
                }
            }
            else
            {
                Divisor = origin.Divisor;
                Buckets = origin.Buckets.Copy();
            }

            newBucketIndex = hashCode & (Divisor - 1);
            Buckets.Items[newBucketIndex] = Buckets.Items[newBucketIndex].Add(new KeyValue<TKey, TValue>(hashCode, key, value));
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            for (var bucketIndex = 0; bucketIndex < Buckets.Items.Length; bucketIndex++)
            {
                var keyValues = Buckets.Items[bucketIndex].KeyValues.Items;
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

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public TValue Get(int hashCode, TKey key)
        {
            var keyValues = Buckets.Items[hashCode & (Divisor - 1)].KeyValues.Items;
            for (var index = 0; index < keyValues.Length; index++)
            {
                var keyValue = keyValues[index];
                if (ReferenceEquals(keyValue.Key, key) || Equals(keyValue.Key, key))
                {
                    return keyValue.Value;
                }
            }

            return default(TValue);
        }

        [Pure]
        public Table<TKey, TValue> Remove(int hashCode, TKey key, out bool removed)
        {
            removed = false;
            var newBuckets = ResizableArray<Bucket>.Create(Divisor, Bucket.EmptyBucket);
            var newBucketsArray = newBuckets.Items;
            var bucketIndex = hashCode & (Divisor - 1);
            for (var curBucketInex = 0; curBucketInex < Buckets.Items.Length; curBucketInex++)
            {
                if (curBucketInex != bucketIndex)
                {
                    newBucketsArray[curBucketInex] = Buckets.Items[curBucketInex].Copy();
                    continue;
                }

                // Bucket to remove an element
                var bucket = Buckets.Items[bucketIndex];
                var keyValues = bucket.KeyValues.Items;
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

            return new Table<TKey, TValue>(newBuckets, Divisor, removed ? Count - 1: Count);
        }

        internal struct Bucket
        {
            public static readonly Bucket EmptyBucket = new Bucket(0);
            public readonly ResizableArray<KeyValue<TKey, TValue>> KeyValues;

            [MethodImpl((MethodImplOptions)256)]
            private Bucket(ResizableArray<KeyValue<TKey, TValue>> keyValues)
            {
                KeyValues = keyValues.Items.Length > 0 ? keyValues.Copy() : ResizableArray<KeyValue<TKey, TValue>>.Empty;
            }

            [MethodImpl((MethodImplOptions)256)]
            private Bucket(int count)
            {
                KeyValues = ResizableArray<KeyValue<TKey, TValue>>.Create(count);
            }

            [MethodImpl((MethodImplOptions)256)]
            public Bucket Copy()
            {
                return new Bucket(KeyValues);
            }

            [Pure]
            [MethodImpl((MethodImplOptions)256)]
            public Bucket Add(KeyValue<TKey, TValue> keyValue)
            {
                return new Bucket(KeyValues.Add(keyValue));
            }

            [Pure]
            [MethodImpl((MethodImplOptions)256)]
            public Bucket Remove(int index)
            {
                var count = KeyValues.Items.Length;
                var newBucket = new Bucket(count - 1);
                var newKeyValues = newBucket.KeyValues.Items;
                var keyValues = KeyValues.Items;
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