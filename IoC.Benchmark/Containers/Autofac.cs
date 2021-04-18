﻿namespace IoC.Benchmark.Containers
{
    using System;
    using global::Autofac;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class Autofac: BaseAbstractContainer<IContainer>
    {
        private readonly ContainerBuilder _builder = new ContainerBuilder();
        private readonly Lazy<IContainer> _container;

        public Autofac() => _container = new Lazy<IContainer>(() => _builder.Build());

        public override IContainer CreateContainer() => _container.Value;

        public override void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            var registration = _builder.RegisterType(implementationType).As(contractType);
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    break;

                case AbstractLifetime.Singleton:
                    registration.SingleInstance();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

            if (name != null)
            {
                registration.Keyed<object>(name);
            }
        }

        public override T Resolve<T>() where T : class => _container.Value.Resolve<T>();

        public override void Dispose() => _container.Value.Dispose();
    }
}