// ReSharper disable LoopCanBeConvertedToQuery
namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class CoreExtensions
    {
        [MethodImpl((MethodImplOptions)256)]
        public static bool SequenceEqual<T>([NotNull] this T[] array1, [NotNull] T[] array2) =>
            ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);

        [MethodImpl((MethodImplOptions)256)]
        public static int GetHash<T>([NotNull][ItemNotNull] this T[] items)
        {
            unchecked
            {
                var code = 0;
                // ReSharper disable once ForCanBeConvertedToForeach
                // ReSharper disable once LoopCanBeConvertedToQuery
                for (var i = 0; i < items.Length; i++)
                {
                    code = (code * 397) ^ items[i].GetHashCode();
                }

                return code;
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        public static T[] EmptyArray<T>() =>
            Empty<T>.Array;

        [MethodImpl((MethodImplOptions) 256)]
        public static T[] CreateArray<T>(int size = 0, T value = default(T))
        {
            if (size == 0)
            {
                return EmptyArray<T>();
            }

            var array = new T[size];
            for (var i = 0; i < size; i++)
            {
                array[i] = value;
            }

            return array;
        }

        [MethodImpl((MethodImplOptions) 256)]
        [Pure]
        [NotNull]
        public static T[] Add<T>([NotNull] this T[] source, [CanBeNull] T value)
        {
            var length = source.Length;
            var destination = new T[length + 1];
            Array.Copy(
                source,
                source.GetLowerBound(0),
                destination,
                destination.GetLowerBound(0),
                length);
            destination[length] = value;
            return destination;
        }

        [MethodImpl((MethodImplOptions) 256)]
        [Pure]
        public static T[] Copy<T>([NotNull] this T[] previous)
        {
            var length = previous.Length;
            var result = new T[length];
            Array.Copy(previous, result, length);
            return result;
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public static TValue Get<TKey, TValue>(this Table<TKey, TValue> table, int hashCode, TKey key)
        {
            var items = table.Buckets[hashCode & table.Divisor].KeyValues;
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

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public static TValue GetByTypeKey<TKey, TValue>(this Table<TKey, TValue> table, int hashCode, TKey key)
            where TKey : Type
        {
            var items = table.Buckets[hashCode & table.Divisor].KeyValues;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
#if NETSTANDARD1_0 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5
                if (ReferenceEquals(key, item.Key))
#else
                // ReSharper disable once PossibleUnintendedReferenceComparison
                if (key == item.Key)
#endif
                {
                    return item.Value;
                }
            }

            return default(TValue);
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public static TValue GetByEquatableKey<TKey, TValue>(this Table<TKey, TValue> table, int hashCode, TKey key)
            where TKey : IEquatable<TKey>
        {
            var items = table.Buckets[hashCode & table.Divisor].KeyValues;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (key.Equals(item.Key))
                {
                    return item.Value;
                }
            }

            return default(TValue);
        }

        private static class Empty<T>
        {
            public static readonly T[] Array = new T[0];
        }
    }
}
