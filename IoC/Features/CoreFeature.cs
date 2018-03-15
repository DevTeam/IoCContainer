namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Core;
    using Lifetimes;

    /// <summary>
    /// Adds the set of core features like lifetimes and default containers.
    /// </summary>
    [PublicAPI]
    public sealed class CoreFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new CoreFeature();

        private CoreFeature()
        {
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => IssueResolver.Shared);
            yield return container.Register(ctx => ctx.Container.GetResolver<TT>(ctx.Key.Tag.AsTag()), null, Feature.AnyTag);

            // Lifetimes
            yield return container.Register<ILifetime>(ctx => new SingletonLifetime(), null, new object[] { Lifetime.Singleton });
            yield return container.Register<ILifetime>(ctx => new ContainerSingletonLifetime(), null, new object[] { Lifetime.ContainerSingleton });
            yield return container.Register<ILifetime>(ctx => new ScopeSingletonLifetime(), null, new object[] { Lifetime.ScopeSingleton });
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_5 && !NETSTANDARD1_6
            yield return container.Register<ILifetime>(ctx => new ThreadSingletonLifetime(), null, new object[] { Lifetime.ThreadSingleton });
#endif
            // Scope
            long scopeId = 0;
            Func<long> createScopeId = () => Interlocked.Increment(ref scopeId);
            yield return container.Register(ctx => new Scope(createScopeId()));

            // Containers
            yield return container.Register(ctx => ctx.Container, null, new object[] { null, WellknownContainers.Current } );
            yield return container.Register<IContainer>(ctx => new Container(ctx.Args.Length == 1 ? Container.CreateContainerName(ctx.Args[0] as string) : Container.CreateContainerName(string.Empty), ctx.Container, false), null, new object[] { WellknownContainers.Child });
            yield return container.Register(ctx => ctx.Container.Parent, null, new object[] { WellknownContainers.Parent });

            yield return container.Register(ctx => (IResourceStore)ctx.Container);
            yield return container.Register(ctx => Validator.Shared);
        }
    }
}
