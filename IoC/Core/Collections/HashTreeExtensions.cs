namespace IoC.Core.Collections
{
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;

    internal static class HashTreeExtensions
    {
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static HashTree<TKey, TValue> Add<TKey, TValue>(this HashTree<TKey, TValue> tree, TKey key, TValue value)
        {
            if (tree.IsEmpty)
            {
                return new HashTree<TKey, TValue>(key, value, tree, tree);
            }

            var hashCode = key.GetHashCode();

            if (hashCode > tree.HashCode)
            {
                return AddToRightBranch(tree, key, value);
            }

            if (hashCode < tree.HashCode)
            {
                return AddToLeftBranch(tree, key, value);
            }

            return new HashTree<TKey, TValue>(key, value, tree);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static IEnumerable<KeyValue<TKey, TValue>> Enumerate<TKey, TValue>(this HashTree<TKey, TValue> hashTree)
        {
            if (!hashTree.IsEmpty)
            {
                foreach (var left in Enumerate(hashTree.Left))
                {
                    yield return new KeyValue<TKey, TValue>(left.Key, left.Value);
                }

                yield return new KeyValue<TKey, TValue>(hashTree.Key, hashTree.Value);

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < hashTree.Duplicates.Items.Length; i++)
                {
                    yield return hashTree.Duplicates.Items[i];
                }

                foreach (var right in Enumerate(hashTree.Right))
                {
                    yield return new KeyValue<TKey, TValue>(right.Key, right.Value);
                }
            }
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static HashTree<TKey, TValue> AddToLeftBranch<TKey, TValue>(HashTree<TKey, TValue> tree, TKey key, TValue value)
        {
            return new HashTree<TKey, TValue>(tree.Key, tree.Value, tree.Left.Add(key, value), tree.Right);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static HashTree<TKey, TValue> AddToRightBranch<TKey, TValue>(HashTree<TKey, TValue> tree, TKey key, TValue value)
        {
            return new HashTree<TKey, TValue>(tree.Key, tree.Value, tree.Left, tree.Right.Add(key, value));
        }
    }
}