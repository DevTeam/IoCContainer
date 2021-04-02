﻿namespace IoC.Benchmark.Containers
{
    using System;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal class CastleWindsor: BaseAbstractContainer<WindsorContainer>
    {
        private readonly WindsorContainer _container = new WindsorContainer();

        public override WindsorContainer CreateContainer() => _container;

        public override void Register(Type contractType, Type implementationType, AbstractLifetime lifetime, string name)
        {
            var registration = Component.For(contractType).ImplementedBy(implementationType);
            switch (lifetime)
            {
                case AbstractLifetime.Transient:
                    registration.LifestyleTransient();
                    break;

                case AbstractLifetime.Singleton:
                    registration.LifestyleSingleton();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }

            if (name != null)
            {
                registration.Named(name);
            }

            _container.Register(registration);
        }

        public override T Resolve<T>() where T : class => _container.Resolve<T>();

        public override void Dispose() => _container.Dispose();
    }
}