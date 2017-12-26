namespace IoC.Internal.Lifetimes
{
    using System;

    internal sealed class SingletoneLifetime : ILifetime
    {
        public static readonly ILifetime Shared = new SingletoneLifetime();

        public object GetOrCreate(Context context, IFactory factory)
        {
            IInstanceKey key;
            if (context.IsConstructedGenericTargetContractType)
            {
                key = new SingletoneGenericInstanceKey<long>(context.RegistrationId, context.TargetContractType.GenericTypeArguments());
            }
            else
            {
                key = new SingletoneInstanceKey<long>(context.RegistrationId);
            }

            var store = context.RegistrationContainer as IInstanceStore ?? throw new NotSupportedException($"The lifetime \"{GetType().Name}\" is not supported for specified container");
            return store.GetOrAdd(key, context, factory);
        }
    }
}
