namespace IoC
{
    using System;

    /// <summary>
    /// Represents the key of binding.
    /// </summary>
    [PublicAPI]
    public struct Key
    {
        /// <summary>
        /// The marker object for any tag.
        /// </summary>
        [NotNull] public static readonly object AnyTag = new AnyTagObject();

        /// <summary>
        /// The type.
        /// </summary>
        [NotNull] public readonly Type Type;

        /// <summary>
        /// The tag.
        /// </summary>
        [CanBeNull] public readonly object Tag;

        /// <summary>
        /// Creates the instance of Key.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tag"></param>
        public Key([NotNull] Type type, [CanBeNull] object tag = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Tag = tag;
        }

        /// <inheritdoc />
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
