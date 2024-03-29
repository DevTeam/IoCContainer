﻿// ReSharper disable InconsistentNaming
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
            abstractContainer.Register(typeof(ICompositionRoot), typeof(CompositionRoot));
            abstractContainer.Register(typeof(IService1), typeof(Service1));
            abstractContainer.Register(typeof(IService2), typeof(Service2Array));
            abstractContainer.Register(typeof(IService3), typeof(Service3));
            abstractContainer.Register(typeof(IService3), typeof(Service3v2), AbstractLifetime.Transient, "2");
            abstractContainer.Register(typeof(IService3), typeof(Service3v3), AbstractLifetime.Transient, "3");
            abstractContainer.Register(typeof(IService3), typeof(Service3v4), AbstractLifetime.Transient, "4");
            return abstractContainer.TryCreate();
        }

        [Benchmark(Description = "new", OperationsPerInvoke = 10)]
        public void New()
        {
            NewInstance();
            NewInstance();
            NewInstance();
            NewInstance();
            NewInstance();
            NewInstance();
            NewInstance();
            NewInstance();
            NewInstance();
            NewInstance();
        }

        private static readonly Func<IService3> Service3Factory = () => new Service3();

        [MethodImpl((MethodImplOptions)0x100)]
        private static ICompositionRoot NewInstance() =>
            new CompositionRoot(new Service1(new Service2Array(new IService3[] { new Service3(), new Service3v2(), new Service3v3(), new Service3v4() })), new Service2Func(Service3Factory), new Service2Array(new IService3[] { new Service3(), new Service3v2(), new Service3v3(), new Service3v4() }), new Service2Array(new IService3[] { new Service3(), new Service3v2(), new Service3v3(), new Service3v4() }), new Service3());
    }
}