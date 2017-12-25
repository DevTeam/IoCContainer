namespace IoC.Internal
{
    using System;
    using System.Collections.Concurrent;

    internal sealed class ContainerLifetime : SingletoneBaseLifetime
    {
        public static readonly ILifetime Shared = new ContainerLifetime();

        protected override ConcurrentDictionary<IInstanceKey, object> GetInstances(Context context)
        {
            return (context.ResolvingContainer as IInstanceStore ?? throw new NotSupportedException($"The lifetime \"{GetType().Name}\" is not supported for specified container")).GetInstances();
        }

        protected override IInstanceKey CeateKey(Context context)
        {
            if (context.TargetContractType.IsConstructedGenericType())
            {
                return new SingletoneGenericInstanceKey<long>(context.RegistrationId, context.TargetContractType.GenericTypeArguments());
            }

            return new SingletoneInstanceKey<long>(context.RegistrationId);
        }
    }
}
