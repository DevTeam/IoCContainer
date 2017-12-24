namespace IoC.Internal
{
    using System;
    using System.Collections.Generic;

    internal sealed class NullContainer : IContainer
    {
        public IContainer Parent => throw new NotSupportedException();

        public bool TryRegister(IEnumerable<Key> keys, IFactory factory, ILifetime lifetime, out IDisposable registrationToken)
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