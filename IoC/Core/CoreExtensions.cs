namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class CoreExtensions
    {
        [MethodImpl((MethodImplOptions)256)]
        public static bool SequenceEqual<T>([NotNull] this T[] array1, [NotNull] T[] array2)
        {
            return ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }

        [MethodImpl((MethodImplOptions)256)]
        public static int GetHash<T>([NotNull][ItemNotNull] this T[] items)
        {
            unchecked
            {
                var code = 0;
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < items.Length; i++)
                {
                    code = (code * 397) ^ items[i].GetHashCode();
                }

                return code;
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        public static T[] EmptyArray<T>()
        {
            return Empty<T>.Array;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static T[] CreateArray<T>(int size = 0, T value = default(T))
        {
            if (size == 0)
            {
                return EmptyArray<T>();
            }

            var array = new T[size];
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
            Array.Fill(array, value);
#else
            for (var i = 0; i < size; i++)
            {
                array[i] = value;
            }
#endif
            return array;
        }

        [MethodImpl((MethodImplOptions) 256)]
        [Pure]
        [NotNull]
        public static T[] Add<T>([NotNull] this T[] previous, [CanBeNull] T value)
        {
            var length = previous.Length;
            var result = new T[length + 1];
            Array.Copy(previous, result, length);
            result[length] = value;
            return result;
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
            where TKey: struct
        {
            var items = table.Buckets[hashCode & table.Divisor].KeyValues;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (item.Key.Equals(key))
                {
                    return item.Value;
                }
            }

            return default(TValue);
        }

        [MethodImpl((MethodImplOptions)256)]
        [Pure]
        public static TValue GetByRef<TKey, TValue>(this Table<TKey, TValue> table, int hashCode, TKey key)
            where TKey: class
        {
            var items = table.Buckets[hashCode & table.Divisor].KeyValues;
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < items.Length; index++)
            {
                var item = items[index];
                if (item.Key == key)
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
