// ReSharper disable InconsistentNaming
namespace IoC.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using global::Autofac;
    using global::DryIoc;
    using global::Ninject;
    using global::Unity;

    [BenchmarkCategory("complex", " 364 instances of unique type")]
    public class Complex
    {
        [Benchmark(Description = "IoC.Container", OperationsPerInvoke = Configs.ComplexSeries * 1000)]
        public void This()
        {
            for (var i = 0; i < Configs.ComplexSeries * 100; i++)
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

        [Benchmark(Description = "IoC.Container composition root", OperationsPerInvoke = Configs.ComplexSeries * 1000)]
        public void ThisByCR()
        {
            for (var i = 0; i < Configs.ComplexSeries * 100; i++)
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

        [Benchmark(OperationsPerInvoke = Configs.ComplexSeries * 10)]
        public void Autofac()
        {
            for (var i = 0; i < Configs.ComplexSeries * 10; i++)
            {
                Configs.AutofacComplex.Resolve(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.ComplexSeries)]
        public void CastleWindsor()
        {
            for (var i = 0; i < Configs.ComplexSeries; i++)
            {
                Configs.CastleWindsorComplex.Resolve(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.ComplexSeries * 1000)]
        public void DryIoc()
        {
            for (var i = 0; i < Configs.ComplexSeries * 100; i++)
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

        [Benchmark(OperationsPerInvoke = Configs.ComplexSeries * 1000)]
        public void LightInject()
        {
            for (var i = 0; i < Configs.ComplexSeries * 100; i++)
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

        [Benchmark(OperationsPerInvoke = Configs.ComplexSeries)]
        public void Ninject()
        {
            for (var i = 0; i < Configs.ComplexSeries; i++)
            {
                Configs.NinjectComplex.Get(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.ComplexSeries)]
        public void Unity()
        {
            for (var i = 0; i < Configs.ComplexSeries; i++)
            {
                Configs.UnityComplex.Resolve(Configs.RootType);
            }
        }
    }
}