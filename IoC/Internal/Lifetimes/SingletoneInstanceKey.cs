namespace IoC.Internal.Lifetimes
{
    using System.Collections.Generic;

    internal struct SingletoneInstanceKey<T> : IInstanceKey
    {
        private static readonly EqualityComparer<T> Comparer = EqualityComparer<T>.Default;
        private readonly T _id;
        private readonly int _hashCode;

        public SingletoneInstanceKey(T id)
        {
            _id = id;
            _hashCode = EqualityComparer<T>.Default.GetHashCode(id);
        }

        public override bool Equals(object obj)
        {
            // if (ReferenceEquals(null, obj)) return false;
            return obj is SingletoneInstanceKey<T> key && Comparer.Equals(_id, key._id);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }
}
