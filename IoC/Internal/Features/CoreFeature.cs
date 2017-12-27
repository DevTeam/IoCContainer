namespace IoC.Internal.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Factories;
    using Lifetimes;

    internal sealed class CoreFeature: IConfiguration
    {
        public static readonly IConfiguration Shared = new CoreFeature();

        private CoreFeature()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            yield return container
                .Bind<IIssueResolver>()
                .To(() => IssueResolver.Shared);

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Transient)
                .To(() => null);

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Singletone)
                .To(() => SingletoneLifetime.Shared);

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Container)
                .To(() => ContainerLifetime.Shared);

            long resolveLifetimeId = 0;
            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Resolve)
                .Lifetime(Lifetime.Singletone)
                .To(() => new ResolveLifetime(Interlocked.Increment(ref resolveLifetimeId)));

            yield return container
                .Bind<IContainer>()
                .To(ctx => ctx.ResolvingContainer);

            yield return container
                .Bind<IContainer>()
                .Tag(Scope.Current)
                .To(ctx => ctx.ResolvingContainer);

            yield return container
                .Bind<IContainer>()
                .Tag(Scope.Child)
                .To(ctx => ctx.ResolvingContainer.CreateChild());

            yield return container
                .Bind<IContainer>()
                .Tag(Scope.Parent)
                .To(ctx => ctx.ResolvingContainer.Parent);

            yield return container
                .Bind<IResourceStore>()
                .To(ctx => (IResourceStore)ctx.ResolvingContainer);

            yield return container
                .Bind<IFactory>()
                .To(ctx => new AutowiringFactory(
                    ctx.ResolvingContainer.Get<IIssueResolver>(),
                    (Type)ctx.Args[0],
                    (Has[])ctx.Args[1]));
        }
    }
}
