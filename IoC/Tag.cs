namespace IoC
{
    [PublicAPI]
    public struct Tag
    {
        public static readonly Tag Default = new Tag(null);
        [CanBeNull] internal readonly object Value;

        public Tag([CanBeNull] object value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"Tag: {Value ?? "empty"}";
        }
    }
}
