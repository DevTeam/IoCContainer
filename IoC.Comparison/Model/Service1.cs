namespace IoC.Comparison.Model
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class Service1 : IService1
    {
        public Service1(IService2 service2, IService3 service31, IService3 service32, IService3 service33, IService4 service4)
        {
        }

        public void DoSomething()
        {
        }
    }
}