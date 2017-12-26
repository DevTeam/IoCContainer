namespace IoC
{
    using System;

    [PublicAPI]
    public struct Key
    {
        [NotNull] internal readonly Type ContractType;
        [CanBeNull] internal readonly object Tag;
        private readonly int _hashCode;

        public Key([NotNull] Type type, [CanBeNull] object tag = null)
        {
            ContractType = type ?? throw new ArgumentNullException(nameof(type));
            Tag = tag;
            unchecked
            {
                _hashCode = (type.GetHashCode() * 397) ^ (tag?.GetHashCode() ?? 0);
            }
        }

        public override string ToString()
        {
            return $"Key: [{ContractType.Name} {Tag ?? "empty"}]";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Key key && Equals(key);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        private bool Equals(Key other)
        {
            return ContractType == other.ContractType && Equals(Tag, other.Tag);
        }
    }
}
