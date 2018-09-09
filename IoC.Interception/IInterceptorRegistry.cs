namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Castle.DynamicProxy;

    internal interface IInterceptorRegistry
    {
        [NotNull]
        IDisposable Register<T>([NotNull] IEnumerable<Key> keys, [NotNull] [ItemNotNull] params IInterceptor[] interceptors);
    }
}