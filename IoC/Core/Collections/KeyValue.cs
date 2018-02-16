namespace IoC.Core.Collections
{
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;

    internal sealed class KeyValue<TKey, TValue>
    {
        private static readonly EqualityComparer<TKey> KeyEqualityComparer = EqualityComparer<TKey>.Default;
        private static readonly EqualityComparer<TValue> ValueEqualityComparer = EqualityComparer<TValue>.Default;
        public readonly TKey Key;
        public readonly TValue Value;
        public readonly int KeyHashCode;
        private readonly int _hashCode;

        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            KeyHashCode = Key.GetHashCode();
            _hashCode = GetHashCodeInternal();
        }

        public override string ToString()
        {
            return $"{Key} = {Value}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is KeyValue<TKey, TValue> value && Equals(value);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private int GetHashCodeInternal()
        {
            unchecked
            {
                var hashCode = KeyHashCode;
                hashCode = (hashCode * 397) ^ KeyEqualityComparer.GetHashCode(Key);
                hashCode = (hashCode * 397) ^ ValueEqualityComparer.GetHashCode(Value);
                return hashCode;
            }
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private bool Equals(KeyValue<TKey, TValue> other)
        {
            return KeyHashCode == other.KeyHashCode && KeyEqualityComparer.Equals(Key, other.Key) && ValueEqualityComparer.Equals(Value, other.Value);
        }
    }
}