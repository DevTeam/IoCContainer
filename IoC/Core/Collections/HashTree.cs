namespace IoC.Core.Collections
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;

    internal sealed class HashTree<TKey, TValue>
    {
        public static readonly HashTree<TKey, TValue> Empty = new HashTree<TKey, TValue>();
        public readonly TKey Key;
        public readonly TValue Value;
        public readonly List<KeyValue<TKey, TValue>> Duplicates;
        public readonly int HashCode;
        public readonly HashTree<TKey, TValue> Left;
        public readonly HashTree<TKey, TValue> Right;
        public readonly int Height;
        public readonly bool IsEmpty;

        public HashTree(TKey key, TValue value, HashTree<TKey, TValue> hashTree)
        {
            Duplicates = hashTree.Duplicates.Add(new KeyValue<TKey, TValue>(key, value));
            Key = hashTree.Key;
            Value = hashTree.Value;
            Height = hashTree.Height;
            HashCode = hashTree.HashCode;
            Left = hashTree.Left;
            Right = hashTree.Right;
        }

        public HashTree(TKey key, TValue value, HashTree<TKey, TValue> left, HashTree<TKey, TValue> right)
        {
            var balance = left.Height - right.Height;

            if (balance == -2)
            {
                if (right.IsLeftHeavy())
                {
                    right = RotateRight(right);
                }

                // Rotate left
                Key = right.Key;
                Value = right.Value;
                Left = new HashTree<TKey, TValue>(key, value, left, right.Left);
                Right = right.Right;
            }
            else if (balance == 2)
            {
                if (left.IsRightHeavy())
                {
                    left = RotateLeft(left);
                }

                // Rotate right
                Key = left.Key;
                Value = left.Value;
                Right = new HashTree<TKey, TValue>(key, value, left.Right, right);
                Left = left.Left;
            }
            else
            {
                Key = key;
                Value = value;
                Left = left;
                Right = right;
            }

            Height = 1 + Math.Max(Left.Height, Right.Height);
            Duplicates = List<KeyValue<TKey, TValue>>.Empty;
            HashCode = Key.GetHashCode();
        }

        public override string ToString()
        {
            if (IsEmpty)
            {
                return "empty";
            }

            return $"{Key} = {Value}";
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private HashTree()
        {
            IsEmpty = true;
            Duplicates = List<KeyValue<TKey, TValue>>.Empty;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static HashTree<TKey, TValue> RotateLeft(HashTree<TKey, TValue> left)
        {
            return new HashTree<TKey, TValue>(
                left.Right.Key,
                left.Right.Value,
                new HashTree<TKey, TValue>(left.Key, left.Value, left.Right.Left, left.Left),
                left.Right.Right);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static HashTree<TKey, TValue> RotateRight(HashTree<TKey, TValue> right)
        {
            return new HashTree<TKey, TValue>(
                right.Left.Key,
                right.Left.Value,
                right.Left.Left,
                new HashTree<TKey, TValue>(right.Key, right.Value, right.Left.Right, right.Right));
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool IsLeftHeavy()
        {
            return Left.Height > Right.Height;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool IsRightHeavy()
        {
            return Right.Height > Left.Height;
        }
    }
}