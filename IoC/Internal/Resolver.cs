namespace IoC.Internal
{
    using System;

    internal class Resolver: IDisposable, IResolver, IFactory
    {
        private readonly object _lockObject;
        private readonly int _registrationId;
        private readonly Key _key;
        [NotNull] private readonly IContainer _registrationContainer;
        [NotNull] private readonly IDisposable _registrationToken;
        [NotNull] private readonly IFactory _factory;
        private readonly IFactory _baseFactory;
        [CanBeNull] private readonly ILifetime _lifetime;
        private Type _prevTargetContractType;
        private bool _prevIsConstructedGenericTargetContractType;

        public Resolver(
            object lockObject,
            int registrationId,
            Key key,
            [NotNull] IContainer registrationContainer,
            [NotNull] IDisposable registrationToken,
            [NotNull] IFactory factory,
            [CanBeNull] ILifetime lifetime = null)
        {
            _lockObject = lockObject;
            _registrationId = registrationId;
            _key = key;
            _registrationContainer = registrationContainer;
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
            lock (_lockObject)
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

                var context = new Context(_registrationId, _key, _registrationContainer, resolvingKey, resolvingContainer, args, _prevIsConstructedGenericTargetContractType);
                return _factory.Create(context);
            }
        }

        public object Create(Context context)
        {
            // ReSharper disable once PossibleNullReferenceException
            return _lifetime.GetOrCreate(context, _baseFactory);
        }
    }
}
