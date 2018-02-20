namespace IoC.Core.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class Map<TKey, TValue>: IEnumerable<Map<TKey, TValue>.KeyValue>
    {
        public static readonly Map<TKey, TValue> Empty = new Map<TKey, TValue>();
        private readonly int _height;
        private readonly Map<TKey, TValue> _left;
        private readonly Map<TKey, TValue> _right;
        private readonly Entry _entry;

        private Map()
        {
            _entry = Entry.Empty;
        }

        private Map(Entry entry)
        {
            _entry = entry;
            _left = Empty;
            _right = Empty;
            _height = 1;
        }

        private Map(Entry entry, Map<TKey, TValue> left, Map<TKey, TValue> right)
        {
            _entry = entry;
            _left = left;
            _right = right;
            _height = (left._height > right._height ? left._height : right._height) + 1;
        }

        private Map(Entry entry, Map<TKey, TValue> left, Map<TKey, TValue> right, int height)
        {
            _entry = entry;
            _left = left;
            _right = right;
            _height = height;
        }

        public Map<TKey, TValue> Set(int hashCode, TKey key, TValue value)
        {
            if (_height == 0)
            {
                return new Map<TKey, TValue>(new Entry(hashCode, key, value));
            }

            if (hashCode == _entry.HashCode)
            {
                if (ReferenceEquals(_entry.Key, key) || _entry.Key.Equals(key))
                {
                    return new Map<TKey, TValue>(new Entry(hashCode, key, value, _entry.Duplicates), _left, _right);
                }

                if (_entry.Duplicates == null)
                {
                    return new Map<TKey, TValue>(new Entry(_entry.HashCode, _entry.Key, _entry.Value, new[] {new KeyValue(hashCode, key, value)}), _left, _right);
                }

                var duplicateIndex = _entry.Duplicates.Length - 1;
                while (duplicateIndex >= 0 && !Equals(_entry.Duplicates[duplicateIndex].Key, _entry.Key))
                {
                    --duplicateIndex;
                }

                // A duplicate was not found
                if (duplicateIndex == -1)
                {
                    var newDuplicates = new KeyValue[_entry.Duplicates.Length + 1];
                    Array.Copy(_entry.Duplicates, 0, newDuplicates, 0, _entry.Duplicates.Length);
                    newDuplicates[_entry.Duplicates.Length] = new KeyValue(hashCode, key, value);
                    return new Map<TKey, TValue>(new Entry(_entry.HashCode, _entry.Key, _entry.Value, newDuplicates), _left, _right);
                }

                // Update the duplicate
                var duplicates = new KeyValue[_entry.Duplicates.Length];
                Array.Copy(_entry.Duplicates, 0, duplicates, 0, _entry.Duplicates.Length);
                duplicates[duplicateIndex] = new KeyValue(hashCode, key, value);
                return new Map<TKey, TValue>(new Entry(_entry.HashCode, _entry.Key, _entry.Value, duplicates), _left, _right);
            }

            // To left
            if (hashCode < _entry.HashCode)
            {
                if (_height == 1)
                {
                    return new Map<TKey, TValue>(_entry, new Map<TKey, TValue>(new Entry(hashCode, key, value)), _right, 2);
                }

                return new Map<TKey, TValue>(_entry, _left.Set(hashCode, key, value), _right).RebalanceTree();
            }

            // To right
            if (_height == 1)
            {
                return new Map<TKey, TValue>(_entry, _left, new Map<TKey, TValue>(new Entry(hashCode, key, value)), 2);
            }
            
            // Too big depth
            return new Map<TKey, TValue>(_entry, _left, _right.Set(hashCode, key, value)).RebalanceTree();
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

            if (tree._height != 0 && (ReferenceEquals(key, tree._entry.Key) || key.Equals(tree._entry.Key)))
            {
                value = tree._entry.Value;
                return true;
            }

            if (tree._height != 0 && tree._entry.Duplicates != null)
            {
                for (var i = tree._entry.Duplicates.Length - 1; i >= 0; --i)
                {
                    if (!Equals(tree._entry.Duplicates[i].Key, key))
                    {
                        continue;
                    }

                    value = tree._entry.Duplicates[i].Value;
                    return true;
                }
            }

            value = default(TValue);
            return false;
        }

        public Map<TKey, TValue> Remove(int hashCode, TKey key, bool ignoreKey = false)
        {
            if (_height == 0)
            {
                return this;
            }

            Map<TKey, TValue> result;
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
                        return new Map<TKey, TValue>(new Entry(_entry.HashCode, _entry.Key, _entry.Value), _left,
                            _right);
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

                    return new Map<TKey, TValue>(new Entry(_entry.HashCode, _entry.Key, _entry.Value, duplicates), _left, _right);

                }

                if (!ignoreKey && _entry.Duplicates != null)
                {
                    if (_entry.Duplicates.Length == 1)
                    {
                        return new Map<TKey, TValue>(new Entry(_entry.HashCode, _entry.Duplicates[0].Key, _entry.Duplicates[0].Value), _left, _right);
                    }

                    var duplicates = new KeyValue[_entry.Duplicates.Length - 1];
                    Array.Copy(_entry.Duplicates, 1, duplicates, 0, duplicates.Length);
                    return new Map<TKey, TValue>(new Entry(_entry.HashCode, _entry.Duplicates[0].Key, _entry.Duplicates[0].Value, duplicates), _left, _right);
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
                    var successor = _right;
                    while (successor._left._height != 0)
                    {
                        successor = successor._left;
                    }

                    result = new Map<TKey, TValue>(successor._entry, _left, _right.Remove(successor._entry.HashCode, default(TKey), true));
                }
            }
            else
            if (hashCode < _entry.HashCode)
            {
                result = new Map<TKey, TValue>(_entry, _left.Remove(hashCode, key, ignoreKey), _right);
            }
            else
            {
                result = new Map<TKey, TValue>(_entry, _left, _right.Remove(hashCode, key, ignoreKey));
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
            var parents = new Map<TKey, TValue>[_height];
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

                    if (node._entry.Duplicates != null)
                    {
                        for (var i = 0; i < node._entry.Duplicates.Length; i++)
                        {
                            yield return node._entry.Duplicates[i];
                        }
                    }

                    node = node._right;
                }
            }
        }

        private Map<TKey, TValue> RebalanceTree()
        {
            var delta = _left._height - _right._height;
            if (delta >= 2)
            {
                var ll = _left._left;
                var lr = _left._right;
                if (lr._height - ll._height == 1)
                {
                    return new Map<TKey, TValue>(
                        lr._entry,
                        new Map<TKey, TValue>(_left._entry, ll, lr._left),
                        new Map<TKey, TValue>(_entry, lr._right, _right));
                }

                return new Map<TKey, TValue>(_left._entry, ll, new Map<TKey, TValue>(_entry, lr, _right));
            }

            if (delta > -2)
            {
                return this;
            }

            var rl = _right._left;
            var rr = _right._right;
            if (rl._height - rr._height == 1)
            {
                return new Map<TKey, TValue>(
                    rl._entry,
                    new Map<TKey, TValue>(_entry, _left, rl._left),
                    new Map<TKey, TValue>(_right._entry, rl._right, rr));
            }

            return new Map<TKey, TValue>( _right._entry, new Map<TKey, TValue>(_entry, _left, rl), rr);
        }

        public class KeyValue
        {
            private readonly int _hashCode;
            public readonly TKey Key;
            public readonly TValue Value;

            public KeyValue(
                int hashCode,
                TKey key,
                TValue value)
            {
                _hashCode = hashCode;
                Key = key;
                Value = value;
            }

            public override bool Equals(object obj)
            {
                return obj is KeyValue other
                       && (ReferenceEquals(other.Key, Key) || Equals(other.Key, Key))
                       && (ReferenceEquals(other.Value, Value) || Equals(other.Value, Value));
            }

            public override int GetHashCode()
            {
                return _hashCode;
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