namespace IoC.Core
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class Arrays
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool SequenceEqual<T>([NotNull] T[] array1, [NotNull] T[] array2)
        {
            return ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
