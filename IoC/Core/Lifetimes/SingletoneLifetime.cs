namespace IoC.Core.Lifetimes
{
    using System;

    public sealed class SingletoneLifetime : ILifetime, IDisposable
    {
        private object _lockObject = new object();
        private volatile bool _isCreated;
        private volatile object _value;

        public T GetOrCreate<T>(Key key, IContainer container, object[] args, Resolver<T> resolver)
        {
            lock (_lockObject)
            {
                if (_isCreated)
                {
                    return (T) _value;
                }

                var value = resolver(container, args);
                _isCreated = true;
                _value = value;
                return value;
            }
        }

        public void Dispose()
        {
            lock (_lockObject)
            {
                if (_isCreated && _value is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
