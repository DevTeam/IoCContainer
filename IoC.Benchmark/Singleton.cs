﻿// ReSharper disable InconsistentNaming
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

    [BenchmarkCategory("singleton", " 20 instances and 1 singleton")]
    public class Singleton
    {
        [Benchmark(Description = "new", OperationsPerInvoke = Configs.Count * Configs.FastK * 10)]
        public void New()
        {
            for (var i = 0; i < Configs.Count * Configs.FastK; i++)
            {
                NewSingleton();
                NewSingleton();
                NewSingleton();
                NewSingleton();
                NewSingleton();
                NewSingleton();
                NewSingleton();
                NewSingleton();
                NewSingleton();
                NewSingleton();
            }
        }

        [Benchmark(Description = "IoC.Container", OperationsPerInvoke = Configs.Count * Configs.FastK * 10)]
        public void This()
        {
            for (var i = 0; i < Configs.Count * Configs.FastK; i++)
            {
                Configs.IoCContainerSingleton.Resolve<IService1>();
                Configs.IoCContainerSingleton.Resolve<IService1>();
                Configs.IoCContainerSingleton.Resolve<IService1>();
                Configs.IoCContainerSingleton.Resolve<IService1>();
                Configs.IoCContainerSingleton.Resolve<IService1>();
                Configs.IoCContainerSingleton.Resolve<IService1>();
                Configs.IoCContainerSingleton.Resolve<IService1>();
                Configs.IoCContainerSingleton.Resolve<IService1>();
                Configs.IoCContainerSingleton.Resolve<IService1>();
                Configs.IoCContainerSingleton.Resolve<IService1>();
            }
        }

        [Benchmark(Description = "IoC.Container composition root", OperationsPerInvoke = Configs.Count * Configs.FastK * 10)]
        public void ThisByCR()
        {
            for (var i = 0; i < Configs.Count * Configs.FastK; i++)
            {
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs);
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs);
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs);
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs);
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs);
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs);
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs);
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs);
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs);
                Configs.IoCContainerSingletonResolve(Configs.IoCContainerSingleton, Configs.EmptyArgs);
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Count * 10)]
        public void Autofac()
        {
            for (var i = 0; i < Configs.Count * 10; i++)
            {
                Configs.AutofacSingleton.Resolve<IService1>();
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Count)]
        public void CastleWindsor()
        {
            for (var i = 0; i < Configs.Count; i++)
            {
                Configs.CastleWindsorSingleton.Resolve<IService1>();
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Count * Configs.FastK * 10)]
        public void DryIoc()
        {
            for (var i = 0; i < Configs.Count * Configs.FastK; i++)
            {
                Configs.DryIocSingleton.Resolve<IService1>();
                Configs.DryIocSingleton.Resolve<IService1>();
                Configs.DryIocSingleton.Resolve<IService1>();
                Configs.DryIocSingleton.Resolve<IService1>();
                Configs.DryIocSingleton.Resolve<IService1>();
                Configs.DryIocSingleton.Resolve<IService1>();
                Configs.DryIocSingleton.Resolve<IService1>();
                Configs.DryIocSingleton.Resolve<IService1>();
                Configs.DryIocSingleton.Resolve<IService1>();
                Configs.DryIocSingleton.Resolve<IService1>();
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Count * Configs.FastK * 10)]
        public void LightInject()
        {
            for (var i = 0; i < Configs.Count * Configs.FastK; i++)
            {
                Configs.LightInjectSingleton.GetInstance<IService1>();
                Configs.LightInjectSingleton.GetInstance<IService1>();
                Configs.LightInjectSingleton.GetInstance<IService1>();
                Configs.LightInjectSingleton.GetInstance<IService1>();
                Configs.LightInjectSingleton.GetInstance<IService1>();
                Configs.LightInjectSingleton.GetInstance<IService1>();
                Configs.LightInjectSingleton.GetInstance<IService1>();
                Configs.LightInjectSingleton.GetInstance<IService1>();
                Configs.LightInjectSingleton.GetInstance<IService1>();
                Configs.LightInjectSingleton.GetInstance<IService1>();
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Count)]
        public void Ninject()
        {
            for (var i = 0; i < Configs.Count; i++)
            {
                Configs.NinjectSingleton.Get<IService1>();
            }
        }

        [Benchmark(OperationsPerInvoke = Configs.Count)]
        public void Unity()
        {
            for (var i = 0; i < Configs.Count; i++)
            {
                Configs.UnitySingleton.Resolve<IService1>();
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