// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable ObjectCreationAsStatement
namespace IoC.Benchmark
{
    using System.Runtime.CompilerServices;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;
    using global::Autofac;
    using global::DryIoc;
    using global::LightInject;
    using global::Ninject;
    using global::Unity;
    using Microsoft.Extensions.DependencyInjection;
    using Model;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [BenchmarkCategory("singleton", " 20 instances and 1 singleton")]
    public class Singleton
    {
        [Benchmark(Description = "new", OperationsPerInvoke = 10000000)]
        public void New()
        {
            for (var i = 0; i < 1000000; i++)
            {
                NewSingleton().DoSomething();
                NewSingleton().DoSomething();
                NewSingleton().DoSomething();
                NewSingleton().DoSomething();
                NewSingleton().DoSomething();
                NewSingleton().DoSomething();
                NewSingleton().DoSomething();
                NewSingleton().DoSomething();
                NewSingleton().DoSomething();
                NewSingleton().DoSomething();
            }
        }

        [Benchmark(Description = "IoC.Container", OperationsPerInvoke = 10000000)]
        public void This()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Configs.IoCContainerSingleton.Resolve<IService1>().DoSomething();
                Configs.IoCContainerSingleton.Resolve<IService1>().DoSomething();
                Configs.IoCContainerSingleton.Resolve<IService1>().DoSomething();
                Configs.IoCContainerSingleton.Resolve<IService1>().DoSomething();
                Configs.IoCContainerSingleton.Resolve<IService1>().DoSomething();
                Configs.IoCContainerSingleton.Resolve<IService1>().DoSomething();
                Configs.IoCContainerSingleton.Resolve<IService1>().DoSomething();
                Configs.IoCContainerSingleton.Resolve<IService1>().DoSomething();
                Configs.IoCContainerSingleton.Resolve<IService1>().DoSomething();
                Configs.IoCContainerSingleton.Resolve<IService1>().DoSomething();
            }
        }

        [Benchmark(Description = "IoC.Container composition root", OperationsPerInvoke = 10000000)]
        public void ThisByCR()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs).DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 10000)]
        public void Autofac()
        {
            for (var i = 0; i < 10000; i++)
            {
                Configs.AutofacSingleton.Resolve<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 1000)]
        public void CastleWindsor()
        {
            for (var i = 0; i < 1000; i++)
            {
                Configs.CastleWindsorSingleton.Resolve<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 10000000)]
        public void DryIoc()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Configs.DryIocSingleton.Resolve<IService1>().DoSomething();
                Configs.DryIocSingleton.Resolve<IService1>().DoSomething();
                Configs.DryIocSingleton.Resolve<IService1>().DoSomething();
                Configs.DryIocSingleton.Resolve<IService1>().DoSomething();
                Configs.DryIocSingleton.Resolve<IService1>().DoSomething();
                Configs.DryIocSingleton.Resolve<IService1>().DoSomething();
                Configs.DryIocSingleton.Resolve<IService1>().DoSomething();
                Configs.DryIocSingleton.Resolve<IService1>().DoSomething();
                Configs.DryIocSingleton.Resolve<IService1>().DoSomething();
                Configs.DryIocSingleton.Resolve<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 10000000)]
        public void LightInject()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Configs.LightInjectSingleton.GetInstance<IService1>().DoSomething();
                Configs.LightInjectSingleton.GetInstance<IService1>().DoSomething();
                Configs.LightInjectSingleton.GetInstance<IService1>().DoSomething();
                Configs.LightInjectSingleton.GetInstance<IService1>().DoSomething();
                Configs.LightInjectSingleton.GetInstance<IService1>().DoSomething();
                Configs.LightInjectSingleton.GetInstance<IService1>().DoSomething();
                Configs.LightInjectSingleton.GetInstance<IService1>().DoSomething();
                Configs.LightInjectSingleton.GetInstance<IService1>().DoSomething();
                Configs.LightInjectSingleton.GetInstance<IService1>().DoSomething();
                Configs.LightInjectSingleton.GetInstance<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 1000)]
        public void Ninject()
        {
            for (var i = 0; i < 1000; i++)
            {
                Configs.NinjectSingleton.Get<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 10000)]
        public void Unity()
        {
            for (var i = 0; i < 10000; i++)
            {
                Configs.UnitySingleton.Resolve<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 10000000)]
        public void MicrosoftDependencyInjection()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Configs.MicrosoftDependencyInjectionSingleton.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionSingleton.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionSingleton.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionSingleton.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionSingleton.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionSingleton.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionSingleton.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionSingleton.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionSingleton.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionSingleton.GetService<IService1>().DoSomething();
            }
        }

        private readonly object LockObject = new object();
        private  volatile Service2 _service2;

        [MethodImpl((MethodImplOptions)256)]
        private IService1 NewSingleton()
        {
            if (_service2 == null)
            {
                lock (LockObject)
                {
                    if (_service2 == null)
                    {
                        _service2 = new Service2(new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()));
                    }
                }
            }

            return new Service1(_service2, new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service4());
        }
    }
}