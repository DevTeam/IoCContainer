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
        [Benchmark(Description = "IoC.Container", OperationsPerInvoke = Configs.ComplexCount * Configs.FastK * 10)]
        public void This()
        {
            for (var i = 0; i < Configs.ComplexCount * Configs.FastK; i++)
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

        [Benchmark(Description = "IoC.Container composition root", OperationsPerInvoke = Configs.ComplexCount * Configs.FastK * 10)]
        public void ThisByCR()
        {
            for (var i = 0; i < Configs.ComplexCount * Configs.FastK; i++)
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

        [Benchmark(OperationsPerInvoke = Configs.ComplexCount * 10)]
        public void Autofac()
        {
            for (var i = 0; i < Configs.ComplexCount * 10; i++)
            {
                Configs.AutofacComplex.Resolve(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.ComplexCount)]
        public void CastleWindsor()
        {
            for (var i = 0; i < Configs.ComplexCount; i++)
            {
                Configs.CastleWindsorComplex.Resolve(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.ComplexCount * Configs.FastK * 10)]
        public void DryIoc()
        {
            for (var i = 0; i < Configs.ComplexCount * Configs.FastK; i++)
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

        [Benchmark(OperationsPerInvoke = Configs.ComplexCount * Configs.FastK * 10)]
        public void LightInject()
        {
            for (var i = 0; i < Configs.ComplexCount * Configs.FastK; i++)
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

        [Benchmark(OperationsPerInvoke = Configs.ComplexCount)]
        public void Ninject()
        {
            for (var i = 0; i < Configs.ComplexCount; i++)
            {
                Configs.NinjectComplex.Get(Configs.RootType);
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.ComplexCount)]
        public void Unity()
        {
            for (var i = 0; i < Configs.ComplexCount; i++)
            {
                Configs.UnityComplex.Resolve(Configs.RootType);
            }
        }
    }
}