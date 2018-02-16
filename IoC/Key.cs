namespace IoC
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using System.Runtime.CompilerServices;

    [PublicAPI]
    public struct Key
    {
        public static readonly object AnyTag = new AnyTagObject();
        [NotNull] public readonly Type Type;
        [CanBeNull] public readonly object Tag;

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Key Create<T>([CanBeNull] object tag)
        {
            return tag == null ? KeyContainer<T>.Shared : new Key(typeof(T), tag);
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Key Create<T>()
        {
            return KeyContainer<T>.Shared;
        }

        public Key([NotNull] Type type, [CanBeNull] object tag = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Tag = tag;
        }

        public override string ToString()
        {
            return $"{Type.FullName} {Tag ?? "empty"}";
        }

        private static class KeyContainer<T>
        {
            internal static readonly Key Shared = new Key(typeof(T));
        }

        private struct AnyTagObject
        {
            public override string ToString()
            {
                return "any";
            }
        }
    }
}
