// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable ObjectCreationAsStatement
namespace IoC.Benchmark
{
    using System;
    using System.Runtime.CompilerServices;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;
    using Model;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class Array: BenchmarkBase
    {
        public override TActualContainer CreateContainer<TActualContainer, TAbstractContainer>()
        {
            var abstractContainer = new TAbstractContainer();
            abstractContainer.Register(typeof(IServiceRoot), typeof(ServiceRoot));
            abstractContainer.Register(typeof(IService1), typeof(Service1));
            abstractContainer.Register(typeof(IService2), typeof(Service2Array));
            abstractContainer.Register(typeof(IService3), typeof(Service3));
            abstractContainer.Register(typeof(IService3), typeof(Service3v2), AbstractLifetime.Transient, "2");
            abstractContainer.Register(typeof(IService3), typeof(Service3v3), AbstractLifetime.Transient, "3");
            abstractContainer.Register(typeof(IService3), typeof(Service3v4), AbstractLifetime.Transient, "4");
            return abstractContainer.CreateContainer();
        }

        [Benchmark(Description = "new", OperationsPerInvoke = 1000000)]
        public void New()
        {
            for (var i = 0; i < 100000; i++)
            {
                NewInstance().DoSomething();
                NewInstance().DoSomething();
                NewInstance().DoSomething();
                NewInstance().DoSomething();
                NewInstance().DoSomething();
                NewInstance().DoSomething();
                NewInstance().DoSomething();
                NewInstance().DoSomething();
                NewInstance().DoSomething();
                NewInstance().DoSomething();
            }
        }

        private static readonly Func<IService3> Service3Factory = () => new Service3();

        [MethodImpl((MethodImplOptions)0x100)]
        private static IServiceRoot NewInstance() =>
            new ServiceRoot(new Service1(new Service2Array(new IService3[] { new Service3(), new Service3v2(), new Service3v3(), new Service3v4() })), new Service2Func(Service3Factory), new Service2Array(new IService3[] { new Service3(), new Service3v2(), new Service3v3(), new Service3v4() }), new Service2Array(new IService3[] { new Service3(), new Service3v2(), new Service3v3(), new Service3v4() }), new Service3());
    }
}