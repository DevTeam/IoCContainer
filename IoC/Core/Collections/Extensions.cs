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
        [CanBeNull]
        public static Resolver<T> TryGetResolver<T>(Table<Type, Delegate> resolversByType, int hashCode, Type type, IContainer container)
        {
            var tree = resolversByType.Buckets[hashCode & (resolversByType.Divisor - 1)];
            while (tree.Height != 0 && tree.Current.HashCode != hashCode)
            {
                tree = hashCode < tree.Current.HashCode ? tree.Left : tree.Right;
            }

            var treeEntry = tree.Current;
            if (tree.Height != 0)
            {
                if (ReferenceEquals(type, treeEntry.Key))
                {
                    return treeEntry.Value as Resolver<T>;
                }

                var entryDuplicates = treeEntry.Duplicates;
                if (entryDuplicates != null)
                {
                    for (var i = entryDuplicates.Length - 1; i >= 0; --i)
                    {
                        if (!ReferenceEquals(entryDuplicates[i].Key, type))
                        {
                            continue;
                        }

                        return entryDuplicates[i].Value as Resolver<T>;
                    }
                }
            }

            return null;
        }

        [MethodImpl((MethodImplOptions)256)]
        [CanBeNull]
        public static Resolver<T> TryGetResolver<T>(Table<Type, Delegate> resolversByType, Table<Key, Delegate> resolvers, Type type, object tag, IContainer container)
        {
            if (tag == null)
            {
                return TryGetResolver<T>(resolversByType, type.GetHashCode(), type, container);
            }

            var key = new Key(type, tag);
            var hashCode = key.GetHashCode();
            var tree = resolvers.Buckets[hashCode & (resolvers.Divisor - 1)];
            while (tree.Height != 0 && tree.Current.HashCode != hashCode)
            {
                tree = hashCode < tree.Current.HashCode ? tree.Left : tree.Right;
            }

            var treeEntry = tree.Current;
            if (tree.Height != 0)
            {
                if (key.Equals(treeEntry.Key))
                {
                    return treeEntry.Value as Resolver<T>;
                }

                var entryDuplicates = treeEntry.Duplicates;
                if (entryDuplicates != null)
                {
                    for (var i = entryDuplicates.Length - 1; i >= 0; --i)
                    {
                        if (!Equals(entryDuplicates[i].Key, key))
                        {
                            continue;
                        }

                        return entryDuplicates[i].Value as Resolver<T>;
                    }
                }
            }

            return null;
        }
    }
}
