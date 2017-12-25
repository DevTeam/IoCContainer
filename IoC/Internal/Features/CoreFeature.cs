namespace IoC.Internal.Features
{
    using System;
    using System.Collections.Generic;

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
                .To(ctx => IssueResolver.Shared);

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Transient)
                .To(ctx => null);

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Singletone)
                .To(ctx => SingletoneLifetime.Shared);

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Container)
                .To(ctx => ContainerLifetime.Shared);

            yield return container
                .Bind<IContainer>()
                .To(ctx => Normalize(ctx.ResolvingContainer));

            yield return container
                .Bind<IContainer>()
                .Tag(Scope.Current)
                .To(ctx => Normalize(ctx.ResolvingContainer));

            yield return container
                .Bind<IContainer>()
                .Tag(Scope.Child)
                .To(ctx => Normalize(ctx.ResolvingContainer.CreateChild()));

            yield return container
                .Bind<IContainer>()
                .Tag(Scope.Parent)
                .To(ctx => Normalize(ctx.ResolvingContainer.Parent));

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

        private static IContainer Normalize(IContainer container)
        {
            switch (container)
            {
                case Resolving resolving:
                    return resolving.Container;

                default:
                    return container;
            }
        }
    }
}
