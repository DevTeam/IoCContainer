namespace IoC.Benchmark.Containers
{
    using System;
    using global::DryIoc;
    using global::DryIoc.Microsoft.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    internal class DryIoc: BaseAbstractContainer<Container>
    {
        private Container _container = new Container();

        public override Container CreateContainer() => _container;

        public override void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            IReuse reuse;
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    reuse = Reuse.Transient;
                    break;

                case AbstractLifetime.Singleton:
                    reuse = Reuse.Singleton;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

            _container.Register(new ReflectionFactory(implementationType, reuse), contractType, name, null, true);
        }

        public override void Register(IServiceCollection services)
        {
            _container = (Container) _container.WithDependencyInjectionAdapter(services);
        }

        public override T Resolve<T>() where T : class => _container.Resolve<T>();

        public override void Dispose() => _container.Dispose();
    }
}