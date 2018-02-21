namespace IoC.Core.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class Tree<TKey, TValue>: IEnumerable<Tree<TKey, TValue>.KeyValue>
    {
        public static readonly Tree<TKey, TValue> Empty = new Tree<TKey, TValue>();
        private readonly int _height;
        private readonly Tree<TKey, TValue> _left;
        private readonly Tree<TKey, TValue> _right;
        private readonly Entry _entry;

        private Tree()
        {
            _entry = Entry.Empty;
        }

        private Tree(Entry entry)
        {
            _entry = entry;
            _left = Empty;
            _right = Empty;
            _height = 1;
        }

        private Tree(Entry entry, Tree<TKey, TValue> left, Tree<TKey, TValue> right)
        {
            _entry = entry;
            _left = left;
            _right = right;
            _height = (left._height > right._height ? left._height : right._height) + 1;
        }

        private Tree(Entry entry, Tree<TKey, TValue> left, Tree<TKey, TValue> right, int height)
        {
            _entry = entry;
            _left = left;
            _right = right;
            _height = height;
        }

        public Tree<TKey, TValue> Set(int hashCode, TKey key, TValue value)
        {
            if (_height == 0)
            {
                return new Tree<TKey, TValue>(new Entry(hashCode, key, value));
            }

            if (hashCode == _entry.HashCode)
            {
                var entryDuplicates = _entry.Duplicates;
                var entryKey = _entry.Key;
                if (ReferenceEquals(entryKey, key) || entryKey.Equals(key))
                {
                    return new Tree<TKey, TValue>(new Entry(hashCode, key, value, entryDuplicates), _left, _right);
                }

                if (entryDuplicates == null)
                {
                    return new Tree<TKey, TValue>(new Entry(_entry.HashCode, entryKey, _entry.Value, new[] {new KeyValue(hashCode, key, value)}), _left, _right);
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
                    return new Tree<TKey, TValue>(new Entry(_entry.HashCode, entryKey, _entry.Value, newDuplicates), _left, _right);
                }

                // Update the duplicate
                var duplicates = new KeyValue[entryDuplicates.Length];
                Array.Copy(entryDuplicates, 0, duplicates, 0, entryDuplicates.Length);
                duplicates[duplicateIndex] = new KeyValue(hashCode, key, value);
                return new Tree<TKey, TValue>(new Entry(_entry.HashCode, entryKey, _entry.Value, duplicates), _left, _right);
            }

            // To left
            if (hashCode < _entry.HashCode)
            {
                if (_height == 1)
                {
                    return new Tree<TKey, TValue>(_entry, new Tree<TKey, TValue>(new Entry(hashCode, key, value)), _right, 2);
                }

                return new Tree<TKey, TValue>(_entry, _left.Set(hashCode, key, value), _right).RebalanceTree();
            }

            // To right
            if (_height == 1)
            {
                return new Tree<TKey, TValue>(_entry, _left, new Tree<TKey, TValue>(new Entry(hashCode, key, value)), 2);
            }
            
            // Too big depth
            return new Tree<TKey, TValue>(_entry, _left, _right.Set(hashCode, key, value)).RebalanceTree();
        }

        [MethodImpl((MethodImplOptions)256)]
        public bool TryGet(int hashCode, TKey key, out TValue value)
        {
            var tree = this;
            while (tree._height != 0 && tree._entry.HashCode != hashCode)
            {
                if (hashCode < tree._entry.HashCode)
                {
                    tree = tree._left;
                }
                else
                {
                    tree = tree._right;
                }
            }

            var treeEntry = tree._entry;
            if (tree._height != 0 && (ReferenceEquals(key, treeEntry.Key) || key.Equals(treeEntry.Key)))
            {
                value = treeEntry.Value;
                return true;
            }

            var entryDuplicates = treeEntry.Duplicates;
            if (tree._height != 0 && entryDuplicates != null)
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
            if (_height == 0)
            {
                return this;
            }

            Tree<TKey, TValue> result;
            if (hashCode == _entry.HashCode)
            {
                if (!ignoreKey && !Equals(_entry.Key, key))
                {
                    if (_entry.Duplicates == null)
                    {
                        return this;
                    }

                    var index = _entry.Duplicates.Length - 1;
                    while (index >= 0 && !Equals(_entry.Duplicates[index].Key, key))
                    {
                        --index;
                    }

                    if (index == -1)
                    {
                        return this;
                    }

                    if (_entry.Duplicates.Length == 1)
                    {
                        return new Tree<TKey, TValue>(new Entry(_entry.HashCode, _entry.Key, _entry.Value), _left, _right);
                    }

                    var duplicates = new KeyValue[_entry.Duplicates.Length - 1];
                    var newIndex = 0;
                    for (var i = 0; i < _entry.Duplicates.Length; ++i)
                    {
                        if (i == index)
                        {
                            continue;
                        }

                        duplicates[newIndex++] = _entry.Duplicates[i];
                    }

                    return new Tree<TKey, TValue>(new Entry(_entry.HashCode, _entry.Key, _entry.Value, duplicates), _left, _right);

                }

                if (!ignoreKey && _entry.Duplicates != null)
                {
                    if (_entry.Duplicates.Length == 1)
                    {
                        return new Tree<TKey, TValue>(new Entry(_entry.HashCode, _entry.Duplicates[0].Key, _entry.Duplicates[0].Value), _left, _right);
                    }

                    var duplicates = new KeyValue[_entry.Duplicates.Length - 1];
                    Array.Copy(_entry.Duplicates, 1, duplicates, 0, duplicates.Length);
                    return new Tree<TKey, TValue>(new Entry(_entry.HashCode, _entry.Duplicates[0].Key, _entry.Duplicates[0].Value, duplicates), _left, _right);
                }

                if (_height == 1)
                {
                    return Empty;
                }

                if (_right._height == 0)
                {
                    result = _left;
                }
                else if (_left._height == 0)
                {
                    result = _right;
                }
                else
                {
                    var inheritor = _right;
                    while (inheritor._left._height != 0)
                    {
                        inheritor = inheritor._left;
                    }

                    result = new Tree<TKey, TValue>(inheritor._entry, _left, _right.Remove(inheritor._entry.HashCode, default(TKey), true));
                }
            }
            else
            if (hashCode < _entry.HashCode)
            {
                result = new Tree<TKey, TValue>(_entry, _left.Remove(hashCode, key, ignoreKey), _right);
            }
            else
            {
                result = new Tree<TKey, TValue>(_entry, _left, _right.Remove(hashCode, key, ignoreKey));
            }

            if (result._height == 1)
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
            if (_height == 0)
            {
                yield break;
            }

            var parentCount = -1;
            var node = this;
            var parents = new Tree<TKey, TValue>[_height];
            while (node._height != 0 || parentCount != -1)
            {
                if (node._height != 0)
                {
                    parents[++parentCount] = node;
                    node = node._left;
                }
                else
                {
                    node = parents[parentCount--];
                    yield return new KeyValue(node._entry.HashCode, node._entry.Key, node._entry.Value);

                    var entryDuplicates = node._entry.Duplicates;
                    if (entryDuplicates != null)
                    {
                        for (var i = 0; i < entryDuplicates.Length; i++)
                        {
                            yield return entryDuplicates[i];
                        }
                    }

                    node = node._right;
                }
            }
        }

        private Tree<TKey, TValue> RebalanceTree()
        {
            var delta = _left._height - _right._height;
            if (delta >= 2)
            {
                var ll = _left._left;
                var lr = _left._right;
                if (lr._height - ll._height == 1)
                {
                    return new Tree<TKey, TValue>(
                        lr._entry,
                        new Tree<TKey, TValue>(_left._entry, ll, lr._left),
                        new Tree<TKey, TValue>(_entry, lr._right, _right));
                }

                return new Tree<TKey, TValue>(_left._entry, ll, new Tree<TKey, TValue>(_entry, lr, _right));
            }

            if (delta > -2)
            {
                return this;
            }

            var rl = _right._left;
            var rr = _right._right;
            if (rl._height - rr._height == 1)
            {
                return new Tree<TKey, TValue>(
                    rl._entry,
                    new Tree<TKey, TValue>(_entry, _left, rl._left),
                    new Tree<TKey, TValue>(_right._entry, rl._right, rr));
            }

            return new Tree<TKey, TValue>( _right._entry, new Tree<TKey, TValue>(_entry, _left, rl), rr);
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

        private sealed class Entry
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