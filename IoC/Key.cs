namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;

    [PublicAPI]
    public sealed class Key
    {
        public static readonly object AnyTag = new AnyTagObject();
        [NotNull] public readonly Type Type;
        [CanBeNull] public readonly object Tag;
        internal readonly int HashCode;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key Create<T>([CanBeNull] object tag)
        {
            return tag == null ? KeyContainer<T>.Shared : new Key(typeof(T), tag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Key Create<T>()
        {
            return KeyContainer<T>.Shared;
        }

        public Key([NotNull] Type type, [CanBeNull] object tag)
        {
#if DEBUG
            if (type == null) throw new ArgumentNullException(nameof(type));
#endif
            Type = type;
            Tag = tag;
            HashCode = type.GetHashCode();
        }

        public Key([NotNull] Type type)
        {
#if DEBUG
            if (type == null) throw new ArgumentNullException(nameof(type));
#endif
            Type = type;
            HashCode = type.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Type.FullName} {Tag ?? "empty"}";
        }

        public override bool Equals(object obj)
        {
            // if (ReferenceEquals(null, obj)) return false;
            return obj is Key key && Equals(this, key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool Equals(Key key1, Key key2)
        {
            return ReferenceEquals(key1.Type, key2.Type) 
                   && (
                       ReferenceEquals(key1.Tag, key2.Tag)
                       || Equals(key1.Tag, key2.Tag)
                       || ReferenceEquals(key1.Tag, AnyTag)
                       || ReferenceEquals(key2.Tag, AnyTag));
        }

        public override int GetHashCode()
        {
            return HashCode;
        }

        internal static class KeyContainer<T>
        {
            internal static readonly Key Shared = new Key(typeof(T));
        }

        private sealed class AnyTagObject
        {
            public override string ToString()
            {
                return "any";
            }
        }
    }

}
