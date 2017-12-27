namespace IoC.Internal.Lifetimes
{
    using System;

    internal class ResolveLifetime: ILifetime
    {
        private readonly long _id;

        public ResolveLifetime(long id)
        {
            _id = id;
        }

        public object GetOrCreate(Context context, IFactory factory)
        {
            var store = context.ResolvingContainer as IInstanceStore ?? throw new NotSupportedException($"The lifetime \"{GetType().Name}\" is not supported for specified container");
            object key;
            if (context.IsConstructedGenericTargetContractType)
            {
                key = new SingletoneGenericInstanceKey<ResolveId>(new ResolveId(_id, context.RegistrationId), context.TargetContractType.GenericTypeArguments());
            }
            else
            {
                key = new SingletoneInstanceKey<ResolveId>(new ResolveId(_id, context.RegistrationId));
            }

            return store.GetOrAdd(key, context, factory);
        }
    }
}
