// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable ObjectCreationAsStatement
namespace IoC.Benchmark
{
    using System.Runtime.CompilerServices;
    using BenchmarkDotNet.Attributes;
    using global::Autofac;
    using global::DryIoc;
    using global::LightInject;
    using global::Ninject;
    using global::Unity;
    using Model;

    [BenchmarkCategory("transient", " 27 instances")]
    public sealed class Transient
    {
        [Benchmark(Description = "new", OperationsPerInvoke = Configs.Series * 1000)]
        public void New()
        {
            for (var i = 0; i < Configs.Series * 100; i++)
            {
                NewTransient();
                NewTransient();
                NewTransient();
                NewTransient();
                NewTransient();
                NewTransient();
                NewTransient();
                NewTransient();
                NewTransient();
                NewTransient();
            }
        }

        [Benchmark(Description = "IoC.Container", OperationsPerInvoke = Configs.Series * 1000)]
        public void This()
        {
            for (var i = 0; i < Configs.Series * 100; i++)
            {
                Configs.IoCContainerTransient.Resolve<IService1>();
                Configs.IoCContainerTransient.Resolve<IService1>();
                Configs.IoCContainerTransient.Resolve<IService1>();
                Configs.IoCContainerTransient.Resolve<IService1>();
                Configs.IoCContainerTransient.Resolve<IService1>();
                Configs.IoCContainerTransient.Resolve<IService1>();
                Configs.IoCContainerTransient.Resolve<IService1>();
                Configs.IoCContainerTransient.Resolve<IService1>();
                Configs.IoCContainerTransient.Resolve<IService1>();
                Configs.IoCContainerTransient.Resolve<IService1>();
            }
        }

        [Benchmark(Description = "IoC.Container composition root", OperationsPerInvoke = Configs.Series * 1000)]
        public void ThisByCR()
        {
            for (var i = 0; i < Configs.Series * 100; i++)
            {
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs);
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs);
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs);
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs);
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs);
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs);
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs);
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs);
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs);
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs);
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Series * 10)]
        public void Autofac()
        {
            for (var i = 0; i < Configs.Series * 10; i++)
            {
                Configs.AutofacTransient.Resolve<IService1>();
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Series)]
        public void CastleWindsor()
        {
            for (var i = 0; i < Configs.Series; i++)
            {
                Configs.CastleWindsorTransient.Resolve<IService1>();
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Series * 1000)]
        public void DryIoc()
        {
            for (var i = 0; i < Configs.Series * 100; i++)
            {
                Configs.DryIocTransient.Resolve<IService1>();
                Configs.DryIocTransient.Resolve<IService1>();
                Configs.DryIocTransient.Resolve<IService1>();
                Configs.DryIocTransient.Resolve<IService1>();
                Configs.DryIocTransient.Resolve<IService1>();
                Configs.DryIocTransient.Resolve<IService1>();
                Configs.DryIocTransient.Resolve<IService1>();
                Configs.DryIocTransient.Resolve<IService1>();
                Configs.DryIocTransient.Resolve<IService1>();
                Configs.DryIocTransient.Resolve<IService1>();
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Series * 1000)]
        public void LightInject()
        {
            for (var i = 0; i < Configs.Series * 100; i++)
            {
                Configs.LightInjectTransient.GetInstance<IService1>();
                Configs.LightInjectTransient.GetInstance<IService1>();
                Configs.LightInjectTransient.GetInstance<IService1>();
                Configs.LightInjectTransient.GetInstance<IService1>();
                Configs.LightInjectTransient.GetInstance<IService1>();
                Configs.LightInjectTransient.GetInstance<IService1>();
                Configs.LightInjectTransient.GetInstance<IService1>();
                Configs.LightInjectTransient.GetInstance<IService1>();
                Configs.LightInjectTransient.GetInstance<IService1>();
                Configs.LightInjectTransient.GetInstance<IService1>();
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Series)]
        public void Ninject()
        {
            for (var i = 0; i < Configs.Series; i++)
            {
                Configs.NinjectTransient.Get<IService1>();
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Series)]
        public void Unity()
        {
            for (var i = 0; i < Configs.Series; i++)
            {
                Configs.UnityTransient.Resolve<IService1>();
            }
        }


        [MethodImpl((MethodImplOptions)256)]
        private static IService1 NewTransient() => 
            new Service1(new Service2(new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4())), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service4());
    }
}