﻿namespace IoC.Benchmark.Containers
{
    using System;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class IoCContainerByCompositionRoot<TContract> : IAbstractContainer<Func<TContract>>
    {
        private readonly IoCContainer _abstractContainer = new IoCContainer();

        public Func<TContract> ActualContainer => _abstractContainer.ActualContainer.Resolve<Func<TContract>>();

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime = AbstractLifetime.Transient) =>
            _abstractContainer.Register(contractType, implementationType, lifetime);

        public void Dispose() => _abstractContainer.Dispose();
    }
}