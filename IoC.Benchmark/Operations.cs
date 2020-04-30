namespace IoC.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using Model;

    [BenchmarkCategory("operations")]
    public class Operations
    {
        private readonly IMutableContainer _container = Container.Create(Features.CoreFeature.Set);

        [Benchmark(Description = "Create and dispose root container", OperationsPerInvoke = 1000)]
        public void CreateAndDispose()
        {
            for (var i = 0; i < 1000; i++)
            {
                Container.Create(Features.CoreFeature.Set).Dispose();
            }
        }

        [Benchmark(Description = "Create and dispose child container", OperationsPerInvoke = 1000)]
        public void CreateAndDisposeChild()
        {
            for (var i = 0; i < 1000; i++)
            {
                _container.Create().Dispose();
            }
        }

        [Benchmark(Description = "Bind and unbind", OperationsPerInvoke = 1000)]
        public void BindAndUnbind()
        {
            for (var i = 0; i < 1000; i++)
            {
                _container.Bind<IService4>().To<Service4>().Dispose();
            }
        }
    }
}
