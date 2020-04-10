﻿namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Interception;

    /// <inheritdoc cref="IConfiguration" />
    [PublicAPI]
    public sealed class InterceptionFeature : IConfiguration
    {
        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Bind<InterceptorBuilder, IInterceptorRegistry, IBuilder>().As(Lifetime.Singleton).To();
        }
    }
}
