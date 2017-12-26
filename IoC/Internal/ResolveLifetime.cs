namespace IoC.Internal
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    internal class ResolveLifetime: ILifetime
    {
        private readonly long _id;

        public ResolveLifetime(long id)
        {
            _id = id;
        }

        public object GetOrCreate(Context context, IFactory factory)
        {
            IInstanceKey key;
            if (context.IsConstructedGenericTargetContractType)
            {
                key = new SingletoneGenericInstanceKey<Id>(new Id(_id, context.RegistrationId), context.TargetContractType.GenericTypeArguments());
            }
            else
            {
                key = new SingletoneInstanceKey<Id>(new Id(_id, context.RegistrationId));
            }

            var store = context.ResolvingContainer as IInstanceStore ?? throw new NotSupportedException($"The lifetime \"{GetType().Name}\" is not supported for specified container");
            return store.GetOrAdd(key, context, factory);
        }

        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private struct Id
        {
            private readonly long _id;
            private readonly long _registrationId;
            private readonly int _hashCode;

            public Id(long id, long registrationId)
            {
                _id = id;
                _registrationId = registrationId;
                unchecked
                {
                    _hashCode = (id.GetHashCode() * 397) ^ registrationId.GetHashCode();
                }
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is Id id && Equals(id);
            }

            public override int GetHashCode()
            {
                return _hashCode;
            }
            private bool Equals(Id other)
            {
                return _id == other._id && _registrationId == other._registrationId;
            }
        }
    }
}
