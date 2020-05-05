namespace IoC.Benchmark.Model
{
    using System.Diagnostics.CodeAnalysis;

    public sealed class Service2 : IService2
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        public Service2(IService3 service41, IService3 service42, IService3 service43, IService3 service44, IService3 service45)
        {
        }
    }
}