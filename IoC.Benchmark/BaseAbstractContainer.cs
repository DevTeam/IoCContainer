﻿namespace IoC.Benchmark
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Model;

    internal abstract class BaseAbstractContainer<TActualContainer> : IAbstractContainer<TActualContainer>
    {
        public TActualContainer TryCreate()
        {
            try
            {
                return Resolve<ICompositionRoot>().Verify() ? CreateContainer() : default;
            }
            catch
            {
                return default;
            }
        }

        public abstract TActualContainer CreateContainer();
        
        public abstract void Register(Type contractType, Type implementationType, AbstractLifetime lifetime = AbstractLifetime.Transient, string name = null);
        
        public virtual void Register(IServiceCollection services) {}

        public abstract T Resolve<T>() where T : class;

        public abstract void Dispose();
    }
}
