namespace IoC.Benchmark.Containers
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class IoCContainerByCompositionRoot<TContract> : BaseAbstractContainer<Func<TContract>>
    {
        private readonly IoCContainer _container = new IoCContainer();

        public override Func<TContract> CreateContainer() => _container.CreateContainer().Resolve<Func<TContract>>();

        public override void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name) =>
            _container.Register(contractType, implementationType, lifetime, name);

        public override void Register(IServiceCollection services) => _container.Register(services);

        public override T Resolve<T>() where T : class => _container.Resolve<T>();

        public override void Dispose() => _container.Dispose();
    }
}