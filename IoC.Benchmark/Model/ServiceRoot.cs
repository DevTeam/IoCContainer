namespace IoC.Benchmark.Model
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class ServiceRoot : IServiceRoot
    {
        public ServiceRoot(IService1 service1, IService2 service31, IService2 service32, IService2 service33, IService3 service3)
        {
        }

        public void DoSomething()
        {
        }
    }
}