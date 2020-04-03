namespace EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;

    internal struct Id: IComparable<Id>
    {
        private readonly int _value;

        public Id(Id id) => _value = id._value + 1;

        public override bool Equals(object obj) => obj is Id other && other._value == _value;

        public override int GetHashCode() => _value;

        public override string ToString() => $"[{_value}]";

        public int CompareTo(Id other) => Comparer<int>.Default.Compare(_value, other._value);
    }
}
