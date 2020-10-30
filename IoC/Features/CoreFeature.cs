namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Lifetimes;

    /// <summary>
    /// Adds the set of core features like lifetimes and containers.
    /// </summary>
    [PublicAPI]
    public sealed class CoreFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new CoreFeature();

        private CoreFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => FoundCyclicDependency.Shared);
            yield return container.Register(ctx => CannotBuildExpression.Shared);
            yield return container.Register(ctx => CannotGetResolver.Shared);
            yield return container.Register(ctx => CannotRegister.Shared);
            yield return container.Register(ctx => CannotResolveConstructor.Shared);
            yield return container.Register(ctx => CannotResolveDependency.Shared);
            yield return container.Register(ctx => CannotResolveType.Shared);
            yield return container.Register(ctx => CannotResolveGenericTypeArgument.Shared);

            yield return container.Register(ctx => DefaultAutowiringStrategy.Shared);

            // Lifetimes
            yield return container.Register<ILifetime>(ctx => new SingletonLifetime(true), null, new object[] { Lifetime.Singleton });
            yield return container.Register<ILifetime>(ctx => new ContainerSingletonLifetime(), null, new object[] { Lifetime.ContainerSingleton });
            yield return container.Register<ILifetime>(ctx => new ScopeSingletonLifetime(), null, new object[] { Lifetime.ScopeSingleton });
            yield return container.Register<ILifetime>(ctx => new ScopeRootLifetime(), null, new object[] { Lifetime.ScopeRoot });

            // Scope
            yield return container.Register<IScopeToken>(ctx => Scope.Current);
            yield return container.Register<IScope>(ctx => new Scope(ctx.Container.Inject<ILockObject>()));

            // Current container
            yield return container.Register<IContainer, IObservable<ContainerEvent>>(ctx => ctx.Container);

            // New child container
            yield return container.Register<IMutableContainer>(
                ctx => new Container(
                    ctx.Args.Length == 1 ? Container.CreateContainerName(ctx.Args[0] as string) : Container.CreateContainerName(string.Empty),
                    ctx.Container,
                    ctx.Container.Inject<ILockObject>()));

            yield return container.Register<Func<IMutableContainer>>(ctx => () => ctx.Container.Inject<IMutableContainer>());
            yield return container.Register<Func<string, IMutableContainer>>(ctx => name => ctx.Container.Resolve<IMutableContainer>(name));

            // Metadata
            yield return container.Register(ctx => ctx.Container.Inject<IBuildContext>().Parent.Key);
            yield return container.Register(ctx => ctx.Container.Inject<IBuildContext>().Parent.Key.Tag.AsTag());

            yield return container.Register(ctx => ContainerEventToStringConverter.Shared);
            yield return container.Register(ctx => TypeToStringConverter.Shared);
        }
    }
}
