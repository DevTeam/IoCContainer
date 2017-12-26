namespace IoC.Internal
{
    using System.Collections.Generic;

    internal struct SingletoneInstanceKey<T> : IInstanceKey
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly T _id;
        private readonly int _hashCode;

        public SingletoneInstanceKey(T id)
        {
            _id = id;
            _hashCode = EqualityComparer<T>.Default.GetHashCode(id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SingletoneInstanceKey<T> key && Equals(key);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        private bool Equals(SingletoneInstanceKey<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_id, other._id);
        }
    }
}
