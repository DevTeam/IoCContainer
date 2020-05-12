namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Issues;

    internal class ResolveUnregisteredFeature: IConfiguration, ICannotGetResolver, ICannotResolveDependency, IDisposable
    {
        private readonly IList<IDisposable> _tokens = new List<IDisposable>();

        public ResolveUnregisteredFeature() { }

        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return container.Bind<ICannotResolveDependency>().Bind<ICannotGetResolver>().To(ctx => this);
        }

        public Resolver<T> Resolve<T>(IContainer container, Key key, Exception error)
        {
            if (container is IMutableContainer mutableContainer && mutableContainer.TryRegisterDependency(new[] { key }, new FullAutowiringDependency(key.Type), null, out var token))
            {
                _tokens.Add(token);
                return container.GetResolver<T>(key.Type, key.Tag.AsTag());
            }

            return (container.Parent ?? throw new InvalidOperationException($"Parent container should not be null.")).Resolve<ICannotGetResolver>().Resolve<T>(container, key, error);
        }

        public DependencyDescription Resolve(IBuildContext buildContext) => 
            new DependencyDescription(new FullAutowiringDependency(buildContext.Key.Type), null);

        public void Dispose()
        {
            foreach (var token in _tokens)
            {
                token.Dispose();
            }
        }
    }
}
