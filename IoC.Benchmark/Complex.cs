// ReSharper disable InconsistentNaming
namespace IoC.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;
    using global::Autofac;
    using global::DryIoc;
    using global::Ninject;
    using global::Unity;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [BenchmarkCategory("complex", " 364 instances of unique type")]
    public class Complex
    {
        [Benchmark(Description = "IoC.Container", OperationsPerInvoke = 1000000)]
        public void This()
        {
            for (var i = 0; i < 100000; i++)
            {
                Configs.IoCContainerComplex.Resolve<object>(Configs.RootType);
                Configs.IoCContainerComplex.Resolve<object>(Configs.RootType);
                Configs.IoCContainerComplex.Resolve<object>(Configs.RootType);
                Configs.IoCContainerComplex.Resolve<object>(Configs.RootType);
                Configs.IoCContainerComplex.Resolve<object>(Configs.RootType);
                Configs.IoCContainerComplex.Resolve<object>(Configs.RootType);
                Configs.IoCContainerComplex.Resolve<object>(Configs.RootType);
                Configs.IoCContainerComplex.Resolve<object>(Configs.RootType);
                Configs.IoCContainerComplex.Resolve<object>(Configs.RootType);
                Configs.IoCContainerComplex.Resolve<object>(Configs.RootType);
            }
        }

        [Benchmark(Description = "IoC.Container composition root", OperationsPerInvoke = 1000000)]
        public void ThisByCR()
        {
            for (var i = 0; i < 100000; i++)
            {
                Configs.IoCContainerComplexResolve(Configs.IoCContainerComplex, Configs.EmptyArgs);
                Configs.IoCContainerComplexResolve(Configs.IoCContainerComplex, Configs.EmptyArgs);
                Configs.IoCContainerComplexResolve(Configs.IoCContainerComplex, Configs.EmptyArgs);
                Configs.IoCContainerComplexResolve(Configs.IoCContainerComplex, Configs.EmptyArgs);
                Configs.IoCContainerComplexResolve(Configs.IoCContainerComplex, Configs.EmptyArgs);
                Configs.IoCContainerComplexResolve(Configs.IoCContainerComplex, Configs.EmptyArgs);
                Configs.IoCContainerComplexResolve(Configs.IoCContainerComplex, Configs.EmptyArgs);
                Configs.IoCContainerComplexResolve(Configs.IoCContainerComplex, Configs.EmptyArgs);
                Configs.IoCContainerComplexResolve(Configs.IoCContainerComplex, Configs.EmptyArgs);
                Configs.IoCContainerComplexResolve(Configs.IoCContainerComplex, Configs.EmptyArgs);
            }
        }

        [Benchmark(OperationsPerInvoke = 1000)]
        public void Autofac()
        {
            for (var i = 0; i < 1000; i++)
            {
                Configs.AutofacComplex.Resolve(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = 100)]
        public void CastleWindsor()
        {
            for (var i = 0; i < 100; i++)
            {
                Configs.CastleWindsorComplex.Resolve(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = 1000000)]
        public void DryIoc()
        {
            for (var i = 0; i < 100000; i++)
            {
                Configs.DryIocComplex.Resolve(Configs.RootType);
                Configs.DryIocComplex.Resolve(Configs.RootType);
                Configs.DryIocComplex.Resolve(Configs.RootType);
                Configs.DryIocComplex.Resolve(Configs.RootType);
                Configs.DryIocComplex.Resolve(Configs.RootType);
                Configs.DryIocComplex.Resolve(Configs.RootType);
                Configs.DryIocComplex.Resolve(Configs.RootType);
                Configs.DryIocComplex.Resolve(Configs.RootType);
                Configs.DryIocComplex.Resolve(Configs.RootType);
                Configs.DryIocComplex.Resolve(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = 1000000)]
        public void LightInject()
        {
            for (var i = 0; i < 100000; i++)
            {
                Configs.LightInjectComplex.GetInstance(Configs.RootType);
                Configs.LightInjectComplex.GetInstance(Configs.RootType);
                Configs.LightInjectComplex.GetInstance(Configs.RootType);
                Configs.LightInjectComplex.GetInstance(Configs.RootType);
                Configs.LightInjectComplex.GetInstance(Configs.RootType);
                Configs.LightInjectComplex.GetInstance(Configs.RootType);
                Configs.LightInjectComplex.GetInstance(Configs.RootType);
                Configs.LightInjectComplex.GetInstance(Configs.RootType);
                Configs.LightInjectComplex.GetInstance(Configs.RootType);
                Configs.LightInjectComplex.GetInstance(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = 100)]
        public void Ninject()
        {
            for (var i = 0; i < 100; i++)
            {
                Configs.NinjectComplex.Get(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = 1000)]
        public void Unity()
        {
            for (var i = 0; i < 1000; i++)
            {
                Configs.UnityComplex.Resolve(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = 1000000)]
        public void MicrosoftDependencyInjection()
        {
            for (var i = 0; i < 100000; i++)
            {
                Configs.MicrosoftDependencyInjectionComplex.GetService(Configs.RootType);
                Configs.MicrosoftDependencyInjectionComplex.GetService(Configs.RootType);
                Configs.MicrosoftDependencyInjectionComplex.GetService(Configs.RootType);
                Configs.MicrosoftDependencyInjectionComplex.GetService(Configs.RootType);
                Configs.MicrosoftDependencyInjectionComplex.GetService(Configs.RootType);
                Configs.MicrosoftDependencyInjectionComplex.GetService(Configs.RootType);
                Configs.MicrosoftDependencyInjectionComplex.GetService(Configs.RootType);
                Configs.MicrosoftDependencyInjectionComplex.GetService(Configs.RootType);
                Configs.MicrosoftDependencyInjectionComplex.GetService(Configs.RootType);
                Configs.MicrosoftDependencyInjectionComplex.GetService(Configs.RootType);
            }
        }
    }
}