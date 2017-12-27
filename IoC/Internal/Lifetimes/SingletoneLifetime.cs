namespace IoC.Internal.Lifetimes
{
    using System;

    internal sealed class SingletoneLifetime : ILifetime
    {
        public static readonly ILifetime Shared = new SingletoneLifetime();

        public object GetOrCreate(Context context, IFactory factory)
        {
            var store = context.RegistrationContainer as IInstanceStore ?? throw new NotSupportedException($"The lifetime \"{GetType().Name}\" is not supported for specified container");
            if (context.IsConstructedGenericTargetContractType)
            {
                var key = new SingletoneGenericInstanceKey<int>(context.RegistrationId, context.TargetContractType.GenericTypeArguments());
                return store.GetOrAdd(key, context, factory);
            }

            return store.GetOrAdd(context.RegistrationId, context, factory);
        }
    }
}
