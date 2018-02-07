namespace IoC.Core
{
    using System;

    internal interface IResolverHolder<out T> : IDisposable
    {
        Resolver<T> Resolve { get; }
    }
}
