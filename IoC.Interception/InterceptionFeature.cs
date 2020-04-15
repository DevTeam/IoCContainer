namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Interception;

    /// <summary>
    /// Allows to use interceptions for containers instances.
    /// </summary>
    [PublicAPI]
    public sealed class InterceptionFeature : IConfiguration
    {
        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Bind<InterceptorBuilder, IInterceptorRegistry, IBuilder>().As(Lifetime.Singleton).To();
        }
    }
}
