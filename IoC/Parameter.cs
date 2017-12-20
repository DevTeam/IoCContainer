namespace IoC
{
    using System;

    [PublicAPI]
    public struct Parameter
    {
        [NotNull] public readonly Type Type;
        public readonly int Position;
        [CanBeNull] public readonly string Name;

        public Parameter([NotNull] Type type, int position, [CanBeNull] string name)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Position = position;
            Name = name;
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
                var hashCode = (Type.GetHashCode());
                hashCode = (hashCode * 397) ^ Position;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }
        private bool Equals(Parameter other)
        {
            return Type == other.Type && Position == other.Position && string.Equals(Name, other.Name);
        }
    }
}
