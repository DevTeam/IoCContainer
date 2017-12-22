namespace IoC.Internal
{
    using System;

    internal struct Resolver : IDisposable, IResolver
    {
        private readonly long _registrationId;
        private readonly Key _key;
        [NotNull] private readonly IContainer _registrationContainer;
        [NotNull] private readonly IDisposable _registrationToken;
        [NotNull] private readonly IFactory _factory;
        [CanBeNull] private readonly ILifetime _lifetime;

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
            _lifetime = lifetime;
        }

        public void Dispose()
        {
            _registrationToken.Dispose();
        }

        public object Resolve(IContainer resolvingContainer, Type contractType, params object[] args)
        {
            if (resolvingContainer == null) throw new ArgumentNullException(nameof(resolvingContainer));
            if (contractType == null) throw new ArgumentNullException(nameof(contractType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            var context = new Context(_registrationId, _key, _registrationContainer, resolvingContainer, contractType, args);
            return _lifetime?.GetOrCreate(context, _factory) ?? _factory.Create(context);
        }
    }
}
