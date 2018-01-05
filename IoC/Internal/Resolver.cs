namespace IoC.Internal
{
    using System;

    internal sealed class Resolver: IResolver, IFactory
    {
        [NotNull] private readonly object _lockObject;
        [NotNull] private readonly IDisposable _registrationToken;
        [NotNull] private readonly IFactory _factory;
        [NotNull] private readonly IFactory _baseFactory;
        [CanBeNull] private readonly ILifetime _lifetime;
        [NotNull] private readonly ResolvingContext _context;
        private Key _prevRresolvingKey;
        private bool _isDisposed;

        public Resolver(
            object lockObject,
            int registrationId,
            [NotNull] IContainer registrationContainer,
            [NotNull] IDisposable registrationToken,
            [NotNull] IFactory factory,
            [CanBeNull] ILifetime lifetime = null)
        {
#if DEBUG
            if (registrationContainer == null) throw new ArgumentNullException(nameof(registrationContainer));
            if (registrationToken == null) throw new ArgumentNullException(nameof(registrationToken));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
#endif
            _lockObject = lockObject;
            _registrationToken = registrationToken;
            _lifetime = lifetime;
            _baseFactory = factory;
            _factory = lifetime != null ? this : factory;
            _context = new ResolvingContext(new RegistrationContext(registrationId, registrationContainer));
        }

        public void Dispose()
        {
            lock (_lockObject)
            {
                _isDisposed = true;
                _registrationToken.Dispose();
            }
        }

        public object Resolve(
            Key resolvingKey,
            IContainer resolvingContainer,
            int argsIndexOffset = 0,
            params object[] args)
        {
            lock (_lockObject)
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException($"This instance of \"{nameof(Resolver)}\" was disposed.");
                }

                if (_prevRresolvingKey.GetHashCode() != resolvingKey.GetHashCode() && !Equals(_prevRresolvingKey, resolvingKey))
                {
                    _prevRresolvingKey = resolvingKey;
                    _context.ResolvingKey = _prevRresolvingKey;
                    _context.IsGenericResolvingType = _prevRresolvingKey.ContractType.IsConstructedGenericType();
                }

                if (argsIndexOffset > 0)
                {
                    var newLength = args.Length - argsIndexOffset;
                    var newArgs = new object[newLength];
                    Array.Copy(args, argsIndexOffset, newArgs, 0, newLength);
                    args = newArgs;
                }

                _context.ResolvingContainer = resolvingContainer;
                _context.Args = args;
                return _factory.Create(_context);
            }
        }

        public object Create(ResolvingContext context)
        {
            // ReSharper disable once PossibleNullReferenceException
            return _lifetime.GetOrCreate(context, _baseFactory);
        }
    }
}
