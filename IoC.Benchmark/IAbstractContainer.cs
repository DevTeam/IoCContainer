namespace IoC.Benchmark
{
    using System;

    public interface IAbstractContainer<out TActualContainer>: IDisposable
    {
        TActualContainer CreateContainer();

        void Register(Type contractType, Type implementationType, AbstractLifetime lifetime = AbstractLifetime.Transient, string name = null);
    }
}
