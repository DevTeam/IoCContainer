namespace IoC.Internal.Lifetimes
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    internal struct ResolveId
    {
        private readonly long _id;
        private readonly int _registrationId;
        private readonly int _hashCode;

        public ResolveId(long id, int registrationId)
        {
            _id = id;
            _registrationId = registrationId;
            unchecked
            {
                _hashCode = (id.GetHashCode() * 397) ^ registrationId;
            }
        }

        public override bool Equals(object obj)
        {
            // if (ReferenceEquals(null, obj)) return false;
            return obj is ResolveId id && _id == id._id && _registrationId == id._registrationId;
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }
}
