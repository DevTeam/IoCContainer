namespace IoC.Internal.Lifetimes
{
    using System;

    internal sealed class ResolveLifetime : ILifetime
    {
        private readonly long _id;

        public ResolveLifetime(long id)
        {
            _id = id;
        }

        public object GetOrCreate(ResolvingContext context, IFactory factory)
        {
            var store = context.ResolvingContainer as IInstanceStore ?? throw new NotSupportedException($"The lifetime \"{GetType().Name}\" is not supported for specified container");
            object key;
            if (context.IsGenericResolvingType)
            {
                key = new SingletoneGenericInstanceKey<ResolveId>(new ResolveId(_id, context.RegistrationContext.RegistrationId), context.ResolvingKey.ContractType.GenericTypeArguments());
            }
            else
            {
                key = new SingletoneInstanceKey<ResolveId>(new ResolveId(_id, context.RegistrationContext.RegistrationId));
            }

            return store.GetOrAdd(key, context, factory);
        }
    }
}
