﻿namespace IoC.Benchmark.Containers
{
    using System;
    using Features;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class IoCContainer: IAbstractContainer<Container>
    {
        public IoCContainer()
            :this(CoreFeature.Set)
        { }

        public IoCContainer(params IConfiguration[] configurations) => ActualContainer = Container.Create();

        public Container ActualContainer { get; }

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime = AbstractLifetime.Transient)
        { 
            var bind = ActualContainer.Bind(contractType);
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    break;

                case AbstractLifetime.Singleton:
                    bind.As(Lifetime.Singleton);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

            bind.To(implementationType);
        }

        public void Dispose() => ActualContainer.Dispose();
    }
}