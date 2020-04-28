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
    using Microsoft.Extensions.DependencyInjection;
    using Model;

    [BenchmarkCategory("transient", " 27 instances")]
    public class Transient
    {
        [Benchmark(Description = "new", OperationsPerInvoke = 10000000)]
        public void New()
        {
            for (var i = 0; i < 1000000; i++)
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

        [Benchmark(Description = "IoC.Container", OperationsPerInvoke = 10000000)]
        public void This()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Configs.IoCContainerTransient.Resolve<IService1>().DoSomething();
                Configs.IoCContainerTransient.Resolve<IService1>().DoSomething();
                Configs.IoCContainerTransient.Resolve<IService1>().DoSomething();
                Configs.IoCContainerTransient.Resolve<IService1>().DoSomething();
                Configs.IoCContainerTransient.Resolve<IService1>().DoSomething();
                Configs.IoCContainerTransient.Resolve<IService1>().DoSomething();
                Configs.IoCContainerTransient.Resolve<IService1>().DoSomething();
                Configs.IoCContainerTransient.Resolve<IService1>().DoSomething();
                Configs.IoCContainerTransient.Resolve<IService1>().DoSomething();
                Configs.IoCContainerTransient.Resolve<IService1>().DoSomething();
            }
        }

        [Benchmark(Description = "IoC.Container composition root", OperationsPerInvoke = 10000000)]
        public void ThisByCR()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs).DoSomething();
                Configs.IoCContainerTransientResolve(Configs.IoCContainerTransient, Configs.EmptyArgs).DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 10000)]
        public void Autofac()
        {
            for (var i = 0; i < 10000; i++)
            {
                Configs.AutofacTransient.Resolve<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 1000)]
        public void CastleWindsor()
        {
            for (var i = 0; i < 1000; i++)
            {
                Configs.CastleWindsorTransient.Resolve<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 10000000)]
        public void DryIoc()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Configs.DryIocTransient.Resolve<IService1>().DoSomething();
                Configs.DryIocTransient.Resolve<IService1>().DoSomething();
                Configs.DryIocTransient.Resolve<IService1>().DoSomething();
                Configs.DryIocTransient.Resolve<IService1>().DoSomething();
                Configs.DryIocTransient.Resolve<IService1>().DoSomething();
                Configs.DryIocTransient.Resolve<IService1>().DoSomething();
                Configs.DryIocTransient.Resolve<IService1>().DoSomething();
                Configs.DryIocTransient.Resolve<IService1>().DoSomething();
                Configs.DryIocTransient.Resolve<IService1>().DoSomething();
                Configs.DryIocTransient.Resolve<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 10000000)]
        public void LightInject()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Configs.LightInjectTransient.GetInstance<IService1>().DoSomething();
                Configs.LightInjectTransient.GetInstance<IService1>().DoSomething();
                Configs.LightInjectTransient.GetInstance<IService1>().DoSomething();
                Configs.LightInjectTransient.GetInstance<IService1>().DoSomething();
                Configs.LightInjectTransient.GetInstance<IService1>().DoSomething();
                Configs.LightInjectTransient.GetInstance<IService1>().DoSomething();
                Configs.LightInjectTransient.GetInstance<IService1>().DoSomething();
                Configs.LightInjectTransient.GetInstance<IService1>().DoSomething();
                Configs.LightInjectTransient.GetInstance<IService1>().DoSomething();
                Configs.LightInjectTransient.GetInstance<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 1000)]
        public void Ninject()
        {
            for (var i = 0; i < 1000; i++)
            {
                Configs.NinjectTransient.Get<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 10000)]
        public void Unity()
        {
            for (var i = 0; i < 10000; i++)
            {
                Configs.UnityTransient.Resolve<IService1>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = 10000000)]
        public void MicrosoftDependencyInjection()
        {
            for (var i = 0; i < 1000000; i++)
            {
                Configs.MicrosoftDependencyInjectionTransient.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionTransient.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionTransient.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionTransient.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionTransient.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionTransient.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionTransient.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionTransient.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionTransient.GetService<IService1>().DoSomething();
                Configs.MicrosoftDependencyInjectionTransient.GetService<IService1>().DoSomething();
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        private static IService1 NewTransient() => 
            new Service1(new Service2(new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4())), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service4());
    }
}