namespace EntityFrameworkCore
{
    internal struct Id
    {
        private readonly int _value;

        public Id(Id id) => _value = id._value + 1;

        public override bool Equals(object obj) => obj is Id other && Equals(other);

        public override int GetHashCode() => _value;

        public override string ToString() => $"[{_value}]";

        private bool Equals(Id other) => _value == other._value;
    }
}
