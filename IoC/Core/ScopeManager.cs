namespace IoC.Core
{
    using System;

    internal class ScopeManager : IScopeManager
    {
        [NotNull] private IScope _current;
        [CanBeNull] private IScope _prev;

        public ScopeManager([NotNull] ILockObject lockObject, IContainer container) =>
            _current = new Scope(this, lockObject, container, true);

        public IScope Current => _current;

        public IDisposable Activate(IScope scope)
        {
            if (ReferenceEquals(scope, Current))
            {
                return Disposable.Empty;
            }

            _prev = Current;
            _current = scope;
            return Disposable.Create(() => { _current = _prev; });
        }
    }
}