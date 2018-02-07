namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using Lifetimes;

    internal sealed class CoreFeature : IConfiguration
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
                .Tag(Lifetime.Singletone)
                .To(ctx => new SingletoneLifetime());

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Container)
                .To(ctx => new ContainerLifetime());

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Scope)
                .To(ctx => new ScopeLifetime());

            yield return container
                .Bind<IContainer>()
                .Tag()
                .Tag(ContainerReference.Current)
                .To(ctx => ctx.Container);

            yield return container
                .Bind<IContainer>()
                .Tag(ContainerReference.Child)
                .To(ctx => new Container(ctx.Args.Length == 1 ? Container.CreateContainerName(ctx.Args[0] as string) : Container.CreateContainerName(string.Empty), ctx.Container, false));

            yield return container
                .Bind<IContainer>()
                .Tag(ContainerReference.Parent)
                .To(ctx => ctx.Container.Parent);

            yield return container
                .Bind<IResourceStore>()
                .To(ctx => (IResourceStore)ctx.Container);
        }
    }
}
