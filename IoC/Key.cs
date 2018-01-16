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
        public static Key Create<T>([CanBeNull] object tag = null)
        {
            return tag == null ? KeyContainer<T>.Shared : new Key(typeof(T), tag);
        }

        public Key([NotNull] Type type, [CanBeNull] object tag = null)
        {
#if DEBUG
            if (type == null) throw new ArgumentNullException(nameof(type));
#endif
            Type = type;
            Tag = tag;
            HashCode = type.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Type.Name} {Tag ?? "empty"}";
        }

        public override bool Equals(object obj)
        {
            // if (ReferenceEquals(null, obj)) return false;
            return obj is Key key && Equals(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Key key)
        {
            return ReferenceEquals(Type, key.Type) && (ReferenceEquals(key.Tag, Tag) || ReferenceEquals(Tag, key.Tag) || Equals(key.Tag, Tag) || Equals(Tag, key.Tag));
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
            public override int GetHashCode()
            {
                return 0;
            }

            public override bool Equals(object obj)
            {
                return !(obj is null);
            }

            public override string ToString()
            {
                return "any";
            }
        }
    }

}
