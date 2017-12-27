namespace IoC
{
    using System;

    [PublicAPI]
    public class Key
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
            // if (ReferenceEquals(null, obj)) return false;
            return obj is Key key && ContractType == key.ContractType && Equals(Tag, key.Tag);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }
}
