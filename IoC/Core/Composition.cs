namespace IoC.Core
{
    using System;

    internal sealed class Composition<T> : IComposition<T>
    {
        [NotNull] private readonly IContainer _container;
        [NotNull] private readonly IToken _token;
        [NotNull] private readonly Resolver<T> _resolver;
        [NotNull] [ItemCanBeNull] private readonly object[] _args;
        private readonly ILockObject _lockObject = new LockObject();
        private  T _root;
        private volatile bool _resolved;

        public Composition([NotNull] IContainer container, [NotNull] IToken token, [NotNull] Resolver<T> resolver, [NotNull, ItemCanBeNull] object[] args)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _token = token ?? throw new ArgumentNullException(nameof(token));
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _args = args ?? throw new ArgumentNullException(nameof(args));
        }

        public T Root
        {
            get
            {
                lock (_lockObject)
                {
                    if (!_resolved)
                    {
                        _root = _resolver(_container, _args);
                        _resolved = true;
                    }

                    return _root;
                }
            }
        }

        public void Dispose()
        {
            using (_token.Container)
            using (_token)
            {
                lock (_lockObject)
                {
                    if (_resolved && _root is IDisposable disposable)
                    {
                        disposable.Dispose();
                        _root = default(T);
                    }
                }
            }
        }
    }
}