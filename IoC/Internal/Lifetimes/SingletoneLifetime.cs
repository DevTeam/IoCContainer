namespace IoC.Internal.Lifetimes
{
    using System;

    internal sealed class SingletoneLifetime : ILifetime
    {
        public static readonly ILifetime Shared = new SingletoneLifetime();

        public object GetOrCreate(ResolvingContext context, IFactory factory)
        {
            var registrationContext = context.RegistrationContext;
            var store = registrationContext.RegistrationContainer as IInstanceStore ?? throw new NotSupportedException($"The lifetime \"{GetType().Name}\" is not supported for specified container");
            if (context.IsConstructedGenericResolvingContractType)
            {
                var key = new SingletoneGenericInstanceKey<int>(registrationContext.RegistrationId, context.ResolvingKey.ContractType.GenericTypeArguments());
                return store.GetOrAdd(key, context, factory);
            }

            return store.GetOrAdd(registrationContext.RegistrationId, context, factory);
        }
    }
}
