namespace IoC.Core
{
    using System.Collections.Generic;
    using System.Linq;
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;

    internal static class Arrays
    {
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool SequenceEqual<T>([NotNull] T[] array1, [NotNull] T[] array2)
        {
            return ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }


#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int GetHash<T>([NotNull] this IEnumerable<T> items)
        {
            return items.Aggregate(0, (code, key) =>
            {
                unchecked
                {
                    return (code * 397) ^ key?.GetHashCode() ?? 0;
                }
            });
        }
    }
}
