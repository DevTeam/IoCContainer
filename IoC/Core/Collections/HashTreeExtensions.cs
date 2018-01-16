namespace IoC.Core.Collections
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class HashTreeExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValue<TKey, TValue>> InOrder<TKey, TValue>(this HashTree<TKey, TValue> hashTree)
        {
            if (!hashTree.IsEmpty)
            {
                foreach (var left in InOrder(hashTree.Left))
                {
                    yield return new KeyValue<TKey, TValue>(left.Key, left.Value);
                }

                yield return new KeyValue<TKey, TValue>(hashTree.Key, hashTree.Value);

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < hashTree.Duplicates.Items.Length; i++)
                {
                    yield return hashTree.Duplicates.Items[i];
                }

                foreach (var right in InOrder(hashTree.Right))
                {
                    yield return new KeyValue<TKey, TValue>(right.Key, right.Value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static HashTree<TKey, TValue> AddToLeftBranch<TKey, TValue>(HashTree<TKey, TValue> tree, TKey key, TValue value)
        {
            return new HashTree<TKey, TValue>(tree.Key, tree.Value, tree.Left.Add(key, value), tree.Right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static HashTree<TKey, TValue> AddToRightBranch<TKey, TValue>(HashTree<TKey, TValue> tree, TKey key, TValue value)
        {
            return new HashTree<TKey, TValue>(tree.Key, tree.Value, tree.Left, tree.Right.Add(key, value));
        }
    }
}