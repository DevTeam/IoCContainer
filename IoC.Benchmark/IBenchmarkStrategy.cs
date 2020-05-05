namespace IoC.Benchmark
{
    public interface IBenchmarkStrategy
    {
        TActualContainer CreateContainer<TActualContainer, TAbstractContainer>()
            where TAbstractContainer : IAbstractContainer<TActualContainer>, new();
    }
}
