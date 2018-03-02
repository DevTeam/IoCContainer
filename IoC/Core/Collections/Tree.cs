namespace IoC.Core.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class Tree<TKey, TValue>: IEnumerable<Tree<TKey, TValue>.KeyValue>
    {
        public static readonly Tree<TKey, TValue> Empty = new Tree<TKey, TValue>();
        public readonly int Height;
        public readonly Tree<TKey, TValue> Left;
        public readonly Tree<TKey, TValue> Right;
        public readonly Entry Current;

        private Tree()
        {
            Current = Entry.Empty;
        }

        private Tree(Entry entry)
        {
            Current = entry;
            Left = Empty;
            Right = Empty;
            Height = 1;
        }

        private Tree(Entry entry, Tree<TKey, TValue> left, Tree<TKey, TValue> right)
        {
            Current = entry;
            Left = left;
            Right = right;
            Height = (left.Height > right.Height ? left.Height : right.Height) + 1;
        }

        private Tree(Entry entry, Tree<TKey, TValue> left, Tree<TKey, TValue> right, int height)
        {
            Current = entry;
            Left = left;
            Right = right;
            Height = height;
        }

        public Tree<TKey, TValue> Set(int hashCode, TKey key, TValue value)
        {
            if (Height == 0)
            {
                return new Tree<TKey, TValue>(new Entry(hashCode, key, value));
            }

            if (hashCode == Current.HashCode)
            {
                var entryDuplicates = Current.Duplicates;
                var entryKey = Current.Key;
                if (ReferenceEquals(entryKey, key) || entryKey.Equals(key))
                {
                    return new Tree<TKey, TValue>(new Entry(hashCode, key, value, entryDuplicates), Left, Right);
                }

                if (entryDuplicates == null)
                {
                    return new Tree<TKey, TValue>(new Entry(Current.HashCode, entryKey, Current.Value, new[] {new KeyValue(hashCode, key, value)}), Left, Right);
                }

                var duplicateIndex = entryDuplicates.Length - 1;
                while (duplicateIndex >= 0 && !Equals(entryDuplicates[duplicateIndex].Key, entryKey))
                {
                    --duplicateIndex;
                }

                // A duplicate was not found
                if (duplicateIndex == -1)
                {
                    var newDuplicates = new KeyValue[entryDuplicates.Length + 1];
                    Array.Copy(entryDuplicates, 0, newDuplicates, 0, entryDuplicates.Length);
                    newDuplicates[entryDuplicates.Length] = new KeyValue(hashCode, key, value);
                    return new Tree<TKey, TValue>(new Entry(Current.HashCode, entryKey, Current.Value, newDuplicates), Left, Right);
                }

                // Update the duplicate
                var duplicates = new KeyValue[entryDuplicates.Length];
                Array.Copy(entryDuplicates, 0, duplicates, 0, entryDuplicates.Length);
                duplicates[duplicateIndex] = new KeyValue(hashCode, key, value);
                return new Tree<TKey, TValue>(new Entry(Current.HashCode, entryKey, Current.Value, duplicates), Left, Right);
            }

            // To left
            if (hashCode < Current.HashCode)
            {
                if (Height == 1)
                {
                    return new Tree<TKey, TValue>(Current, new Tree<TKey, TValue>(new Entry(hashCode, key, value)), Right, 2);
                }

                return new Tree<TKey, TValue>(Current, Left.Set(hashCode, key, value), Right).RebalanceTree();
            }

            // To right
            if (Height == 1)
            {
                return new Tree<TKey, TValue>(Current, Left, new Tree<TKey, TValue>(new Entry(hashCode, key, value)), 2);
            }
            
            // Too big depth
            return new Tree<TKey, TValue>(Current, Left, Right.Set(hashCode, key, value)).RebalanceTree();
        }

        [MethodImpl((MethodImplOptions)256)]
        public bool TryGet(int hashCode, TKey key, out TValue value)
        {
            var tree = this;
            while (tree.Height != 0 && tree.Current.HashCode != hashCode)
            {
                if (hashCode < tree.Current.HashCode)
                {
                    tree = tree.Left;
                }
                else
                {
                    tree = tree.Right;
                }
            }

            var treeEntry = tree.Current;
            if (tree.Height != 0 && (ReferenceEquals(key, treeEntry.Key) || key.Equals(treeEntry.Key)))
            {
                value = treeEntry.Value;
                return true;
            }

            var entryDuplicates = treeEntry.Duplicates;
            if (tree.Height != 0 && entryDuplicates != null)
            {
                for (var i = entryDuplicates.Length - 1; i >= 0; --i)
                {
                    if (!Equals(entryDuplicates[i].Key, key))
                    {
                        continue;
                    }

                    value = entryDuplicates[i].Value;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        public Tree<TKey, TValue> Remove(int hashCode, TKey key, bool ignoreKey = false)
        {
            if (Height == 0)
            {
                return this;
            }

            Tree<TKey, TValue> result;
            if (hashCode == Current.HashCode)
            {
                if (!ignoreKey && !Equals(Current.Key, key))
                {
                    if (Current.Duplicates == null)
                    {
                        return this;
                    }

                    var index = Current.Duplicates.Length - 1;
                    while (index >= 0 && !Equals(Current.Duplicates[index].Key, key))
                    {
                        --index;
                    }

                    if (index == -1)
                    {
                        return this;
                    }

                    if (Current.Duplicates.Length == 1)
                    {
                        return new Tree<TKey, TValue>(new Entry(Current.HashCode, Current.Key, Current.Value), Left, Right);
                    }

                    var duplicates = new KeyValue[Current.Duplicates.Length - 1];
                    var newIndex = 0;
                    for (var i = 0; i < Current.Duplicates.Length; ++i)
                    {
                        if (i == index)
                        {
                            continue;
                        }

                        duplicates[newIndex++] = Current.Duplicates[i];
                    }

                    return new Tree<TKey, TValue>(new Entry(Current.HashCode, Current.Key, Current.Value, duplicates), Left, Right);

                }

                if (!ignoreKey && Current.Duplicates != null)
                {
                    if (Current.Duplicates.Length == 1)
                    {
                        return new Tree<TKey, TValue>(new Entry(Current.HashCode, Current.Duplicates[0].Key, Current.Duplicates[0].Value), Left, Right);
                    }

                    var duplicates = new KeyValue[Current.Duplicates.Length - 1];
                    Array.Copy(Current.Duplicates, 1, duplicates, 0, duplicates.Length);
                    return new Tree<TKey, TValue>(new Entry(Current.HashCode, Current.Duplicates[0].Key, Current.Duplicates[0].Value, duplicates), Left, Right);
                }

                if (Height == 1)
                {
                    return Empty;
                }

                if (Right.Height == 0)
                {
                    result = Left;
                }
                else if (Left.Height == 0)
                {
                    result = Right;
                }
                else
                {
                    var inheritor = Right;
                    while (inheritor.Left.Height != 0)
                    {
                        inheritor = inheritor.Left;
                    }

                    result = new Tree<TKey, TValue>(inheritor.Current, Left, Right.Remove(inheritor.Current.HashCode, default(TKey), true));
                }
            }
            else
            if (hashCode < Current.HashCode)
            {
                result = new Tree<TKey, TValue>(Current, Left.Remove(hashCode, key, ignoreKey), Right);
            }
            else
            {
                result = new Tree<TKey, TValue>(Current, Left, Right.Remove(hashCode, key, ignoreKey));
            }

            if (result.Height == 1)
            {
                return result;
            }

            return result.RebalanceTree();
        }

        public IEnumerator<KeyValue> GetEnumerator()
        {
            return Enumerate().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl((MethodImplOptions)256)]
        private IEnumerable<KeyValue> Enumerate()
        {
            if (Height == 0)
            {
                yield break;
            }

            var parentCount = -1;
            var node = this;
            var parents = new Tree<TKey, TValue>[Height];
            while (node.Height != 0 || parentCount != -1)
            {
                if (node.Height != 0)
                {
                    parents[++parentCount] = node;
                    node = node.Left;
                }
                else
                {
                    node = parents[parentCount--];
                    yield return new KeyValue(node.Current.HashCode, node.Current.Key, node.Current.Value);

                    var entryDuplicates = node.Current.Duplicates;
                    if (entryDuplicates != null)
                    {
                        for (var i = 0; i < entryDuplicates.Length; i++)
                        {
                            yield return entryDuplicates[i];
                        }
                    }

                    node = node.Right;
                }
            }
        }

        private Tree<TKey, TValue> RebalanceTree()
        {
            var delta = Left.Height - Right.Height;
            if (delta >= 2)
            {
                var ll = Left.Left;
                var lr = Left.Right;
                if (lr.Height - ll.Height == 1)
                {
                    return new Tree<TKey, TValue>(
                        lr.Current,
                        new Tree<TKey, TValue>(Left.Current, ll, lr.Left),
                        new Tree<TKey, TValue>(Current, lr.Right, Right));
                }

                return new Tree<TKey, TValue>(Left.Current, ll, new Tree<TKey, TValue>(Current, lr, Right));
            }

            if (delta > -2)
            {
                return this;
            }

            var rl = Right.Left;
            var rr = Right.Right;
            if (rl.Height - rr.Height == 1)
            {
                return new Tree<TKey, TValue>(
                    rl.Current,
                    new Tree<TKey, TValue>(Current, Left, rl.Left),
                    new Tree<TKey, TValue>(Right.Current, rl.Right, rr));
            }

            return new Tree<TKey, TValue>( Right.Current, new Tree<TKey, TValue>(Current, Left, rl), rr);
        }

        public sealed class KeyValue
        {
            public readonly int HashCode;
            public readonly TKey Key;
            public readonly TValue Value;

            public KeyValue(
                int hashCode,
                TKey key,
                TValue value)
            {
                HashCode = hashCode;
                Key = key;
                Value = value;
            }

            public override string ToString()
            {
                return $"{Key} = {Value}";
            }
        }

        internal sealed class Entry
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            [NotNull] public static readonly Entry Empty = new Entry();

            public readonly int HashCode;
            public readonly TKey Key;
            public readonly TValue Value;
            [CanBeNull] public readonly KeyValue[] Duplicates;

            public Entry(int hashCode, TKey key, TValue value, KeyValue[] duplicates = null)
            {
                HashCode = hashCode;
                Key = key;
                Value = value;
                Duplicates = duplicates;
            }

            private Entry()
            {
            }
        }
    }
}