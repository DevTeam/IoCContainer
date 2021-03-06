﻿// ReSharper disable LoopCanBeConvertedToQuery
namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class CoreExtensions
    {
        [MethodImpl((MethodImplOptions)0x100)]
        public static bool SequenceEqual<T>([NotNull] this T[] array1, [NotNull] T[] array2) =>
            ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);

        [MethodImpl((MethodImplOptions)0x200)]
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

        [MethodImpl((MethodImplOptions)0x100)]
        public static T[] EmptyArray<T>() => Empty<T>.Array;

        [MethodImpl((MethodImplOptions)0x200)]
        public static T[] CreateArray<T>(int size = 0, T value = default(T))
        {
            if (size == 0)
            {
                return Empty<T>.Array;
            }

            var array = new T[size];
            if (Equals(value, default(T)))
            {
                return array;
            }

            for (var i = 0; i < size; i++)
            {
                array[i] = value;
            }

            return array;
        }

        [MethodImpl((MethodImplOptions)0x200)]
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

        [Pure]
        [MethodImpl((MethodImplOptions)0x200)]
        public static T[] Copy<T>([NotNull] this T[] previous)
        {
            var length = previous.Length;
            var result = new T[length];
            Array.Copy(previous, result, length);
            return result;
        }

        private static class Empty<T>
        {
            public static readonly T[] Array = new T[0];
        }
    }
}
