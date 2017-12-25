﻿namespace IoC.Internal
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics.CodeAnalysis;

    internal class ResolveLifetime: SingletoneBaseLifetime
    {
        private readonly long _id;

        public ResolveLifetime(long id)
        {
            _id = id;
        }

        protected override ConcurrentDictionary<IInstanceKey, object> GetInstances(Context context)
        {
            return (context.ResolvingContainer as IInstanceStore ?? throw new NotSupportedException($"The lifetime \"{GetType().Name}\" is not supported for specified container")).GetInstances();
        }

        protected override IInstanceKey CeateKey(Context context)
        {
            if (context.TargetContractType.IsConstructedGenericType())
            {
                return new SingletoneGenericInstanceKey<Id>(new Id(_id, context.RegistrationId), context.TargetContractType.GenericTypeArguments());
            }

            return new SingletoneInstanceKey<Id>(new Id(_id, context.RegistrationId));
        }

        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        private struct Id
        {
            private readonly long _id;
            private readonly long _registrationId;

            public Id(long id, long registrationId)
            {
                _id = id;
                _registrationId = registrationId;
            }
        }
    }
}
