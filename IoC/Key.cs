namespace IoC
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

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

        private readonly int _hashCode;

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
                _hashCode = (tag?.GetHashCode() * 397 ?? 0) ^ type.GetHashCode();
            }
        }

        /// <inheritdoc />
        public override string ToString() => $"[Type = {Type.FullName}, Tag = {Tag ?? "empty"}, HashCode = {GetHashCode()}]";

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        // ReSharper disable once PossibleNullReferenceException
        public override bool Equals(object obj) => Equals((Key)obj);

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public bool Equals(Key other) => ReferenceEquals(Type, other.Type) && (ReferenceEquals(Tag, other.Tag) || Equals(Tag, other.Tag));

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public override int GetHashCode() => _hashCode;

        private struct AnyTagObject
        {
            public override string ToString() => "any";
        }
    }
}
