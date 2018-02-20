namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Lifetimes;
    using Extensibility;

    /// <summary>
    /// Adds the set of core features like lifetimes and default containers.
    /// </summary>
    [PublicAPI]
    public sealed class CoreFeature : IConfiguration
    {
        /// The shared instance.
        public static readonly IConfiguration Shared = new CoreFeature();

        private CoreFeature()
        {
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container
                .Bind<IIssueResolver>()
                .To(ctx => IssueResolver.Shared);

            yield return container
                .Bind<Resolver<TT>>()
                .AnyTag()
                .To(ctx => ctx.Container.GetResolver<TT>(typeof(TT), ctx.Key.Tag, ctx.Container));

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Singleton)
                .To(ctx => new SingletonLifetime());

            yield return container
                .Bind<Func<ILifetime>>()
                .Tag(Lifetime.Singleton)
                .To(ctx => (() => new SingletonLifetime()));

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.ContainerSingleton)
                .To(ctx => new ContainerSingletonLifetime(ctx.Container.Inject<Func<ILifetime>>(Lifetime.Singleton)));

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.ScopeSingleton)
                .To(ctx => new ScopeSingletonLifetime(ctx.Container.Inject<Func<ILifetime>>(Lifetime.Singleton)));

#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_5 && !NETSTANDARD1_6
            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.ThreadSingleton)
                .To(ctx => new ThreadSingletonLifetime(ctx.Container.Inject<Func<ILifetime>>(Lifetime.Singleton)));
#endif

            yield return container
                .Bind<IContainer>()
                .Tag()
                .Tag(WellknownContainers.Current)
                .To(ctx => ctx.Container);

            yield return container
                .Bind<IContainer>()
                .Tag(WellknownContainers.Child)
                .To(ctx => new Container(ctx.Args.Length == 1 ? Container.CreateContainerName(ctx.Args[0] as string) : Container.CreateContainerName(string.Empty), ctx.Container, false));

            yield return container
                .Bind<IContainer>()
                .Tag(WellknownContainers.Parent)
                .To(ctx => ctx.Container.Parent);

            yield return container
                .Bind<IResourceStore>()
                .To(ctx => (IResourceStore)ctx.Container);
        }
    }
}
