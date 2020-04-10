namespace IoC
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a dependency key.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("Type = {" + nameof(Type) + "}, Tag = {" + nameof(Tag) + "}")]
    public struct Key: IEquatable<Key>
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

        internal readonly int HashCode;

        /// <summary>
        /// Creates the instance of Key.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tag"></param>
        public Key([NotNull] Type type, [CanBeNull] object tag = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Tag = tag;
            unchecked
            {
                HashCode = (tag?.GetHashCode() * 397 ?? 0) ^ type.GetHashCode();
            }
        }

        /// <inheritdoc />
        public override string ToString() => $"[Type = {Type.FullName}, Tag = {Tag ?? "empty"}, HashCode = {HashCode}]";

        /// <inheritdoc />
        // ReSharper disable once PossibleNullReferenceException
        public override bool Equals(object obj)
        {
            // ReSharper disable once PossibleNullReferenceException
            var other = (Key)obj;
            return ReferenceEquals(Type, other.Type) && (ReferenceEquals(Tag, other.Tag) || Equals(Tag, other.Tag));
        }

        /// <inheritdoc />
        public bool Equals(Key other) => ReferenceEquals(Type, other.Type) && (ReferenceEquals(Tag, other.Tag) || Equals(Tag, other.Tag));

        /// <inheritdoc />
        public override int GetHashCode() => HashCode;

        private class AnyTagObject
        {
            public override string ToString() => "any";
        }
    }
}
