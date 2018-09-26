namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Lifetimes;

    /// <inheritdoc cref="IConfiguration" />
    [PublicAPI]
    public class InterceptionFeature: IConfiguration
    {
        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register<InterceptorBuilder, IInterceptorRegistry, IBuilder>(ctx => new InterceptorBuilder(), new SingletonLifetime());
        }
    }
}
