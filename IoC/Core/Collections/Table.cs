namespace IoC.Core.Collections
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    internal sealed class Table<TKey, TValue>: IEnumerable<KeyValue<TKey, TValue>>
    {
        public static readonly Table<TKey, TValue> Empty = new Table<TKey, TValue>();
        public readonly int Divisor;
        public readonly ResizableArray<Bucket> Buckets;
        private readonly int _count;

        [MethodImpl((MethodImplOptions)256)]
        private Table()
        {
            Divisor = 2;
            Buckets = ResizableArray<Bucket>.Create(Divisor, Bucket.EmptyBucket);
        }

        [MethodImpl((MethodImplOptions)256)]
        private Table(Table<TKey, TValue> origin, int hashCode, TKey key, TValue value)
        {
            int newBucketIndex;
            _count = origin._count + 1;
            if (origin._count >= origin.Divisor)
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
            var bucket = Buckets.Items[hashCode & (Divisor - 1)];
            var keyValues = bucket.KeyValues.Items;
            for (var index = 0; index < bucket.Count; index++)
            {
                var keyValue = keyValues[index];
                if (keyValue.HashCode == hashCode && (ReferenceEquals(keyValue.Key, key) || Equals(keyValue.Key, key)))
                {
                    return keyValue.Value;
                }
            }

            return default(TValue);
        }

        [Pure]
        public Table<TKey, TValue> Remove(int hashCode, TKey key)
        {
            var newTable = new Table<TKey, TValue>();
            foreach (var keyValue in this)
            {
                if (keyValue.HashCode == hashCode && (ReferenceEquals(keyValue.Key, key) || Equals(keyValue.Key, key)))
                {
                    continue;
                }

                newTable = newTable.Set(keyValue.HashCode, keyValue.Key, keyValue.Value);
            }
            
            return newTable;
        }

        internal struct Bucket
        {
            public static readonly Bucket EmptyBucket = new Bucket(ResizableArray<KeyValue<TKey, TValue>>.Empty);
            public ResizableArray<KeyValue<TKey, TValue>> KeyValues;
            public readonly int Count;

            [MethodImpl((MethodImplOptions)256)]
            private Bucket(ResizableArray<KeyValue<TKey, TValue>> keyValues)
            {
                KeyValues = keyValues;
                Count = keyValues.Items.Length;
            }

            [Pure]
            [MethodImpl((MethodImplOptions)256)]
            public Bucket Add(KeyValue<TKey, TValue> keyValue)
            {
                return new Bucket(KeyValues.Add(keyValue));
            }
        }
    }
}