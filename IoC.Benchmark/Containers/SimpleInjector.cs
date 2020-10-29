﻿namespace IoC.Benchmark.Containers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::SimpleInjector;


    // ReSharper disable once ClassNeverInstantiated.Global
    internal class SimpleInjector: IAbstractContainer<Container>
    {
        private readonly Container _container = new Container();
        private readonly Lazy<Container> _containerProvider;
        private readonly Dictionary<Type, List<Type>> _collections = new Dictionary<Type, List<Type>>();

        public SimpleInjector()
        {
            _container.Options.EnableAutoVerification = false;
            _container.Options.UseStrictLifestyleMismatchBehavior = false;
            _container.Options.SuppressLifestyleMismatchVerification = true;
            
            _containerProvider = new Lazy<Container>(() =>
            {
                foreach (var (contractType, implementations) in _collections.Where(i => i.Value.Count > 1))
                {
                    _container.Collection.Register(contractType, implementations);
                }

                return _container;
            });
        }

        public Container CreateActualContainer() => _containerProvider.Value;

        public void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    if (!_collections.TryGetValue(contractType, out var implementations))
                    {
                        implementations = new List<Type> { implementationType };
                        _collections.Add(contractType, implementations);
                        _container.Register(contractType, implementationType);
                    }
                    else
                    {
                        implementations.Add(contractType);
                    }

                    break;

                case AbstractLifetime.Singleton:
                    _container.RegisterSingleton(contractType, implementationType);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        public void Dispose() => _container.Dispose();
    }
}