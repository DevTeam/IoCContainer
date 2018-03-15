namespace IoC
{
    /// <summary>
    /// Represents a tag holder.
    /// </summary>
    public struct Tag
    {
        internal readonly object Value;

        internal Tag([CanBeNull] object value) => Value = value;

        /// <inheritdoc />
        public override string ToString() => Value?.ToString() ?? "empty";
    }
}
