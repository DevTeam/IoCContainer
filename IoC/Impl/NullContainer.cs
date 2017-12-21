namespace IoC.Impl
{
    using System;
    using System.Collections.Generic;

    internal class NullContainer : IContainer
    {
        public IContainer Parent => throw new NotSupportedException();

        public IDisposable Register(IEnumerable<Key> keys, IFactory factory, ILifetime lifetime = null)
        {
            throw new NotSupportedException();
        }

        public bool TryGetResolver(Key key, out IResolver resolver)
        {
            resolver = default(IResolver);
            return false;
        }

        public void Dispose()
        {
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}