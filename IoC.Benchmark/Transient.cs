// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable ObjectCreationAsStatement
namespace IoC.Benchmark
{
    using System.Runtime.CompilerServices;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;
    using Model;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class Transient: BenchmarkBase
    {
        public override TActualContainer CreateContainer<TActualContainer, TAbstractContainer>()
        {
            var abstractContainer = new TAbstractContainer();
            abstractContainer.Register(typeof(IServiceRoot), typeof(ServiceRoot));
            abstractContainer.Register(typeof(IService1), typeof(Service1));
            abstractContainer.Register(typeof(IService2), typeof(Service2));
            abstractContainer.Register(typeof(IService3), typeof(Service3));
            return abstractContainer.ActualContainer;
        }

        [Benchmark(Description = "new", OperationsPerInvoke = 1000000)]
        public void New()
        {
            for (var i = 0; i < 100000; i++)
            {
                NewTransient().DoSomething();
                NewTransient().DoSomething();
                NewTransient().DoSomething();
                NewTransient().DoSomething();
                NewTransient().DoSomething();
                NewTransient().DoSomething();
                NewTransient().DoSomething();
                NewTransient().DoSomething();
                NewTransient().DoSomething();
                NewTransient().DoSomething();
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        private static IServiceRoot NewTransient() => 
            new ServiceRoot(new Service1(new Service2(new Service3(), new Service3(), new Service3(), new Service3(), new Service3())), new Service2(new Service3(), new Service3(), new Service3(), new Service3(), new Service3()), new Service2(new Service3(), new Service3(), new Service3(), new Service3(), new Service3()), new Service2(new Service3(), new Service3(), new Service3(), new Service3(), new Service3()), new Service3());
    }
}