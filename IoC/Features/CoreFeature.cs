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

        private CoreFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => FoundCyclicDependency.Shared);
            yield return container.Register(ctx => CannotBuildExpression.Shared);
            yield return container.Register(ctx => CannotGetResolver.Shared);
            yield return container.Register(ctx => CannotParseLifetime.Shared);
            yield return container.Register(ctx => CannotParseTag.Shared);
            yield return container.Register(ctx => CannotParseType.Shared);
            yield return container.Register(ctx => CannotRegister.Shared);
            yield return container.Register(ctx => CannotResolveConstructor.Shared);
            yield return container.Register(ctx => CannotResolveDependency.Shared);
            yield return container.Register(ctx => CannotResolveType.Shared);
            yield return container.Register(ctx => CannotResolveGenericTypeArgument.Shared);

            yield return container.Register(ctx => DefaultAutowiringStrategy.Shared);
            yield return container.Register(ctx => ctx.Container.GetResolver<TT>(ctx.Key.Tag.AsTag()), null, Set.AnyTag);

            // Lifetimes
            yield return container.Register<ILifetime>(ctx => new SingletonLifetime(), null, new object[] { Lifetime.Singleton });
            yield return container.Register<ILifetime>(ctx => new ContainerSingletonLifetime(), null, new object[] { Lifetime.ContainerSingleton });
            yield return container.Register<ILifetime>(ctx => new ScopeSingletonLifetime(), null, new object[] { Lifetime.ScopeSingleton });

            // Scope
            yield return container.Register<IScope>(ctx => new Scope(ctx.Container.Inject<ILockObject>()));

            // ThreadLocal
            yield return container.Register(ctx => new ThreadLocal<TT>(() => ctx.Container.Inject<TT>(ctx.Key.Tag)), null, Set.AnyTag);

            // Containers:

            // Current
            yield return container.Register<IContainer, IResourceRegistry, IObservable<ContainerEvent>>(ctx => ctx.Container);

            // New child
            yield return container.Register<IMutableContainer, IContainer, IResourceRegistry, IObservable<ContainerEvent>>(
                ctx => new Container(
                    ctx.Args.Length == 1
                        ? Container.CreateContainerName(ctx.Args[0] as string)
                        : Container.CreateContainerName(string.Empty),
                    ctx.Container,
                    ctx.Container.Inject<ILockObject>(),
                    false),
                null,
                new object[] { WellknownContainers.NewChild });

            yield return container.Register<Func<IContainer>>(ctx => () => ctx.Container.Inject<IContainer>(WellknownContainers.NewChild));
            yield return container.Register<Func<string, IContainer>>(ctx => name => ctx.Container.Resolve<IContainer>(WellknownContainers.NewChild.AsTag(), name));
            yield return container.Register<Func<IMutableContainer>>(ctx => () => ctx.Container.Inject<IMutableContainer>(WellknownContainers.NewChild));
            yield return container.Register<Func<string, IMutableContainer>>(ctx => name => ctx.Container.Resolve<IMutableContainer>(WellknownContainers.NewChild.AsTag(), name));

            // Parent
            yield return container.Register<IContainer, IResourceRegistry, IObservable<ContainerEvent>>(ctx => ctx.Container.Parent, null, new object[] { WellknownContainers.Parent });

            yield return container.Register(ctx => ContainerEventToStringConverter.Shared);
            yield return container.Register(ctx => TypeToStringConverter.Shared);
        }
    }
}
