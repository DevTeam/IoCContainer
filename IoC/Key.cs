namespace IoC
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents the container key.
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
        public override string ToString() => $"[Type = {Type.FullName}, Tag = {Tag ?? "empty"}, HashCode = {GetHashCode()}]";

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public override bool Equals(object obj)
        {
            // ReSharper disable once PossibleNullReferenceException
            return Equals((Key)obj);
        }

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public bool Equals(Key other)
        {
            return ReferenceEquals(Type, other.Type) && (ReferenceEquals(Tag, other.Tag) || Equals(Tag, other.Tag));
        }

        /// <inheritdoc />
        [MethodImpl((MethodImplOptions)256)]
        public override int GetHashCode()
        {
            unchecked
            {
                return (Tag != null ? Tag.GetHashCode() * 397 : 0) ^ Type.GetHashCode();
            }
        }

        private struct AnyTagObject
        {
            public override string ToString() => "any";
        }
    }
}
