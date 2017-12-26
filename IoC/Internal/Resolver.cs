namespace IoC.Internal
{
    using System;

    internal class Resolver: IDisposable, IResolver, IFactory
    {
        private readonly object _lockObject;
        private readonly long _registrationId;
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
            long registrationId,
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

        public object Resolve(IContainer resolvingContainer, Type targetContractType, params object[] args)
        {
            lock (_lockObject)
            {
                if (_prevTargetContractType != targetContractType)
                {
                    _prevTargetContractType = targetContractType;
                    _prevIsConstructedGenericTargetContractType = targetContractType.IsConstructedGenericType();
                }

                var context = new Context(_registrationId, _key, _registrationContainer, resolvingContainer, targetContractType, args, _prevIsConstructedGenericTargetContractType);
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
