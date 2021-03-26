namespace IoC.Benchmark.Containers
{
    using System;
    using Features;
    using Lifetimes;
    using Microsoft.Extensions.DependencyInjection;
    using Model;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class IoCContainer: BaseAbstractContainer<Container>
    {
        private readonly Container _container = Container.Create(CollectionFeature.Set, FuncFeature.Set);

        public override Container CreateContainer() => _container;

        public override void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        { 
            var bind = _container.Bind(contractType);
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    break;

                case AbstractLifetime.Singleton:
                    bind = bind.Lifetime(new SingletonLifetime(false));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

            if (name != null)
            {
                bind = bind.Tag(name);
            }

            bind.To(implementationType);
        }

        public override void Register(IServiceCollection services) =>
            _container.Using(new AspNetCoreFeature(services));

        public override T Resolve<T>() where T : class => _container.Resolve<T>();

        public override void Dispose() => _container.Dispose();
    }
}