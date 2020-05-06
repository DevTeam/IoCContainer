namespace IoC.Benchmark.Model
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class ServiceRoot : IServiceRoot
    {
        public ServiceRoot(IService1 service1, IService2 service21, IService2 service22, IService2 service23, IService3 service3)
        {
        }

        public void DoSomething()
        {
        }
    }
}