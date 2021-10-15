namespace IoC.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Model;

    [BenchmarkCategory("operations")]
    public class Operations
    {
        private readonly IMutableContainer _container = Container.Create(Features.CoreFeature.Set);

        [Benchmark(Description = "Create and dispose root container")]
        public void CreateAndDispose() => Container.Create(Features.CoreFeature.Set).Dispose();

        [Benchmark(Description = "Create and dispose child container")]
        public void CreateAndDisposeChild() => _container.Create().Dispose();

        [Benchmark(Description = "Bind and unbind")]
        public void BindAndUnbind() => _container.Bind<IService3>().To<Service3>().Dispose();
    }
}
