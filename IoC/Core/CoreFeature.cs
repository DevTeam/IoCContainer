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
                .ToValue(IssueResolver.Shared);

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Singletone)
                .ToFactory((key, curContainer, args) => new SingletoneLifetime());

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Container)
                .ToFactory((key, curContainer, args) => new ContainerLifetime());

            yield return container
                .Bind<ILifetime>()
                .Tag(Lifetime.Scope)
                .ToFactory((key, curContainer, args) => new ScopeLifetime());

            yield return container
                .Bind<IContainer>()
                .Tag()
                .Tag(Scope.Current)
                .ToFactory((key, curContainer, args) => curContainer);

            yield return container
                .Bind<IContainer>()
                .Tag(Scope.Child)
                .ToFactory((key, curContainer, args) => new Container(args.Length == 1 ? Container.CreateContainerName(args[0] as string) : Container.CreateContainerName(), curContainer, false));

            yield return container
                .Bind<IContainer>()
                .Tag(Scope.Parent)
                .ToFactory((key, curContainer, args) => curContainer.Parent ?? throw new NotSupportedException());

            yield return container
                .Bind<IResourceStore>()
                .ToFactory((key, curContainer, args) => (IResourceStore)curContainer);
        }
    }
}
