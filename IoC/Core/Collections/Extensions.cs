namespace IoC.Core.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class Extensions
    {
        [MethodImpl((MethodImplOptions)256)]
        public static bool SequenceEqual<T>([NotNull] T[] array1, [NotNull] T[] array2)
        {
            return ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }


        [MethodImpl((MethodImplOptions)256)]
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

        [MethodImpl((MethodImplOptions)256)]
        public static TValue GetByType<TValue>(this Table<Type, TValue> table, int hashCode, Type type)
        {
            var tree = table.Buckets[hashCode & (table.Divisor - 1)];
            var height = tree.Height;
            var treeEntry = tree.Current;
            while (height != 0 && treeEntry.HashCode != hashCode)
            {
                tree = hashCode < treeEntry.HashCode ? tree.Left : tree.Right;
                height = tree.Height;
                treeEntry = tree.Current;
            }

            if (height != 0)
            {
                if (type == treeEntry.Key)
                {
                    return treeEntry.Value;
                }

                var entryDuplicates = treeEntry.Duplicates;
                if (entryDuplicates != null)
                {
                    for (var i = entryDuplicates.Length - 1; i >= 0; --i)
                    {
                        if (entryDuplicates[i].Key != type)
                        {
                            continue;
                        }

                        return entryDuplicates[i].Value;
                    }
                }
            }

            return default(TValue);
        }
    }
}
