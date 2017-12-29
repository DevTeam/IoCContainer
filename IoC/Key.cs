namespace IoC
{
    using System;

    [PublicAPI]
    public class Key
    {
        public static readonly object AnyTag = new AnyTagObject();

        [NotNull] internal readonly Type ContractType;
        [CanBeNull] internal readonly object Tag;
        private readonly int _hashCode;

        public Key([NotNull] Type type, [CanBeNull] object tag = null)
        {
            ContractType = type ?? throw new ArgumentNullException(nameof(type));
            Tag = tag;
            _hashCode = type.GetHashCode();
        }

        public override string ToString()
        {
            return $"{ContractType.Name} {Tag ?? "empty"}";
        }

        public override bool Equals(object obj)
        {
            // if (ReferenceEquals(null, obj)) return false;
            return obj is Key key && ContractType == key.ContractType && (Equals(key.Tag, Tag) || Equals(Tag, key.Tag));
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        private class AnyTagObject
        {
            public override int GetHashCode()
            {
                return 0;
            }

            public override bool Equals(object obj)
            {
                return !ReferenceEquals(obj, null);
            }

            public override string ToString()
            {
                return "any";
            }
        }
    }
}
