namespace IoC.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Features;

    internal class RootConfiguration: IConfiguration
    {
        public static readonly IConfiguration Shared = new RootConfiguration();

        private RootConfiguration()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            yield return container
                .Map<IIssueResolver>()
                .To(ctx => IssueResolver.Shared);

            yield return container
                .Map<ILifetime>()
                .Tag(Lifetime.Transient)
                .To(ctx => null);

            yield return container
                .Map<ILifetime>()
                .Tag(Lifetime.Singletone)
                .To(ctx => SingletoneLifetime.Shared);

            yield return container
                .Map<ILifetime>()
                .Tag(Lifetime.Container)
                .To(ctx => ContainerLifetime.Shared);

            yield return container
                .Map<IContainer>()
                .To(ctx => Normalize(ctx.ResolvingContainer));

            yield return container
                .Map<IContainer>()
                .Tag(Scope.Current)
                .To(ctx => Normalize(ctx.ResolvingContainer));

            yield return container
                .Map<IContainer>()
                .Tag(Scope.Child)
                .To(ctx => Normalize(ctx.ResolvingContainer.CreateChild()));

            yield return container
                .Map<IContainer>()
                .Tag(Scope.Parent)
                .To(ctx =>
                {
                    return Normalize(ctx.ResolvingContainer.Parent);
                });

            yield return container
                .Map<IResourceStore>()
                .To(ctx => (IResourceStore)ctx.ResolvingContainer);

            yield return container
                .Map<IFactory>()
                .To(ctx => new AutowiringFactory(
                    ctx.ResolvingContainer.Get<IIssueResolver>(),
                    (Type) ctx.Args[0],
                    (Has[]) ctx.Args[1]));

            foreach (var reg in ApplyFeatures(container).SelectMany(i => i))
            {
                yield return reg;
            }
        }

        private static IEnumerable<IEnumerable<IDisposable>> ApplyFeatures(IContainer container)
        {
            yield return EnumerableFeature.Shared.Apply(container);
            yield return FuncFeature.Shared.Apply(container);
            yield return TaskFeature.Shared.Apply(container);
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
