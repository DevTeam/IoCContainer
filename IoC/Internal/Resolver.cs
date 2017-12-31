namespace IoC.Internal
{
    using System;

    internal class Resolver: IDisposable, IResolver, IFactory
    {
        private readonly RegistrationContext _registrationContext;
        [NotNull] private readonly IDisposable _registrationToken;
        [NotNull] private readonly IFactory _factory;
        private readonly IFactory _baseFactory;
        [CanBeNull] private readonly ILifetime _lifetime;
        private Type _prevTargetContractType;
        private bool _prevIsConstructedGenericTargetContractType;

        public Resolver(
            object lockObject,
            int registrationId,
            Key registrationKey,
            [NotNull] IContainer registrationContainer,
            [NotNull] IDisposable registrationToken,
            [NotNull] IFactory factory,
            [CanBeNull] ILifetime lifetime = null)
        {
            _registrationContext = new RegistrationContext(registrationId, registrationKey, registrationContainer);
            _registrationToken = registrationToken;
            _lifetime = lifetime;
            _baseFactory = factory;
            _factory = lifetime != null ? this : factory;
        }

        public void Dispose()
        {
            _registrationToken.Dispose();
        }

        public object Resolve(
            Key resolvingKey,
            IContainer resolvingContainer,
            int argsIndexOffset = 0,
            params object[] args)
        {
            lock (_registrationContext)
            {
                if (_prevTargetContractType != resolvingKey.ContractType)
                {
                    _prevTargetContractType = resolvingKey.ContractType;
                    _prevIsConstructedGenericTargetContractType = resolvingKey.ContractType.IsConstructedGenericType();
                }

                if (argsIndexOffset > 0)
                {
                    var newLength = args.Length - argsIndexOffset;
                    var newArgs = new object[newLength];
                    Array.Copy(args, argsIndexOffset, newArgs, 0, newLength);
                    args = newArgs;
                }

                var context = new ResolvingContext(_registrationContext, resolvingKey, resolvingContainer, args, _prevIsConstructedGenericTargetContractType);
                return _factory.Create(context);
            }
        }

        public object Create(ResolvingContext context)
        {
            // ReSharper disable once PossibleNullReferenceException
            return _lifetime.GetOrCreate(context, _baseFactory);
        }
    }
}
