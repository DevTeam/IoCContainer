namespace IoC.Features
{
    using System;
    using Castle.DynamicProxy;

    internal interface IInterceptorRegistry
    {
        [NotNull]
        IDisposable Register([NotNull] Predicate<IBuildContext> filter, [NotNull] [ItemNotNull] params IInterceptor[] interceptors);
    }
}