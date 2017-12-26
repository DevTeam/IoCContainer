namespace IoC.Internal
{
    using System;

    internal class Resolver: IDisposable, IResolver
    {
        private readonly long _registrationId;
        private readonly Key _key;
        [NotNull] private readonly IContainer _registrationContainer;
        [NotNull] private readonly IDisposable _registrationToken;
        [NotNull] private readonly IFactory _factory;
        private Type _prevTargetContractType;
        private bool _prevIsConstructedGenericTargetContractType;

        public Resolver(
            long registrationId,
            Key key,
            [NotNull] IContainer registrationContainer,
            [NotNull] IDisposable registrationToken,
            [NotNull] IFactory factory,
            [CanBeNull] ILifetime lifetime = null)
        {
            _registrationId = registrationId;
            _key = key;
            _registrationContainer = registrationContainer;
            _registrationToken = registrationToken;
            _factory = factory;
            _factory = lifetime != null ? new LifetimeBaseFactory(lifetime, factory) :_factory;
        }

        public void Dispose()
        {
            _registrationToken.Dispose();
        }

        public object Resolve(IContainer resolvingContainer, Type targetContractType, params object[] args)
        {
            if (resolvingContainer == null) throw new ArgumentNullException(nameof(resolvingContainer));
            if (targetContractType == null) throw new ArgumentNullException(nameof(targetContractType));
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (_prevTargetContractType != targetContractType)
            {
                _prevTargetContractType = targetContractType;
                _prevIsConstructedGenericTargetContractType = targetContractType.IsConstructedGenericType();
            }

            var context = new Context(_registrationId, _key, _registrationContainer, resolvingContainer, targetContractType, _prevIsConstructedGenericTargetContractType, args);
            return _factory.Create(context);
        }
    }
}
