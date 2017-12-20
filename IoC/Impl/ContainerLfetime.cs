namespace IoC.Impl
{
    using System;
    using System.Collections.Generic;

    internal class ContainerLifetime: SingletoneBaseLifetime
    {
        public static readonly ILifetime Shared = new ContainerLifetime();

        protected override IDictionary<IInstanceKey, object> GetInstances(Context context)
        {
            return (context.ResolvingContainer as IInstanceStore ?? throw new NotSupportedException($"The lifetime \"{GetType().Name}\" is not supported for specified container")).GetInstances();
        }

        protected override IInstanceKey CeateKey(Context context)
        {
            if (context.ContractType.IsConstructedGenericType)
            {
                return new SingletoneGenericInstanceKey(context.RegistrationId, context.ContractType.GenericTypeArguments);
            }

            return new SingletoneInstanceKey(context.RegistrationId);
        }
    }
}
