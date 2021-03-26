namespace IoC.Benchmark
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public interface IAbstractContainer<out TActualContainer>: IDisposable
    {
        [CanBeNull] TActualContainer TryCreate();

        void Register(Type contractType, Type implementationType, AbstractLifetime lifetime = AbstractLifetime.Transient, string name = null);

        void Register(IServiceCollection services);

        T Resolve<T>() where T: class;
    }
}
