namespace IoC
{
    using System;

    [PublicAPI]
    public struct Key
    {
        [NotNull] public static readonly object AnyTag = new AnyTagObject();
        [NotNull] public readonly Type Type;
        [CanBeNull] public readonly object Tag;

        public Key([NotNull] Type type, [CanBeNull] object tag = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Tag = tag;
        }

        public override string ToString()
        {
            return $"{Type.FullName} {Tag ?? "empty"}";
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
