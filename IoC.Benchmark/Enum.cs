// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable ObjectCreationAsStatement
namespace IoC.Benchmark
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;
    using Model;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class Enum : BenchmarkBase
    {
        public override TActualContainer CreateContainer<TActualContainer, TAbstractContainer>()
        {
            var abstractContainer = new TAbstractContainer();
            abstractContainer.Register(typeof(ICompositionRoot), typeof(CompositionRoot));
            abstractContainer.Register(typeof(IService1), typeof(Service1));
            abstractContainer.Register(typeof(IService2), typeof(Service2Enum));
            abstractContainer.Register(typeof(IService3), typeof(Service3));
            abstractContainer.Register(typeof(IService3), typeof(Service3v2), AbstractLifetime.Transient, "2");
            abstractContainer.Register(typeof(IService3), typeof(Service3v3), AbstractLifetime.Transient, "3");
            abstractContainer.Register(typeof(IService3), typeof(Service3v4), AbstractLifetime.Transient, "4");
            return abstractContainer.TryCreate();
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
        private static ICompositionRoot NewInstance() =>
            new CompositionRoot(new Service1(new Service2Enum(Service3Enum())), new Service2Func(Service3Factory), new Service2Enum(Service3Enum()), new Service2Enum(Service3Enum()), new Service3());

        private static IEnumerable<IService3> Service3Enum()
        {
            yield return new Service3();
            yield return new Service3v2();
            yield return new Service3v3();
            yield return new Service3v4();
        }
    }
}