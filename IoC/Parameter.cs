namespace IoC
{
    using System;

    [PublicAPI]
    public struct Parameter
    {
        [NotNull] public readonly string Name;
        [CanBeNull] public readonly Type Type;

        public Parameter([NotNull] string name, [CanBeNull] Type type = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            Type = type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Parameter argument && Equals(argument);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"Parameter: [{Type?.Name ?? "AnyType"} {Name}]";
        }

        private bool Equals(Parameter other)
        {
            return Type == other.Type && string.Equals(Name, other.Name);
        }
    }
}
