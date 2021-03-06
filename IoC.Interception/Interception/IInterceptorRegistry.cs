﻿namespace IoC.Features.Interception
{
    using System;
    using Castle.DynamicProxy;

    internal interface IInterceptorRegistry
    {
        [NotNull]
        IToken Register([NotNull] IMutableContainer container, [NotNull] Predicate<Key> filter, [NotNull] [ItemNotNull] params IInterceptor[] interceptors);
    }
}