namespace IoC.Benchmark
{
    using System;
    using BenchmarkDotNet.Attributes;
    using Castle.Windsor;
    using Containers;
    using Autofac;
    using DryIoc;
    using LightInject;
    using Ninject;
    using Unity;
    using Microsoft.Extensions.DependencyInjection;
    using Model;

    public abstract class BenchmarkBase: IBenchmarkStrategy
    {
        private const int Series = 100000;
        private IoC.Container _iocContainer;
        private Func<IServiceRoot> _iocRootResolver;
        private global::Autofac.IContainer _autofacContainer;
        private WindsorContainer _windsorContainerContainer;
        private Container _dryIocContainer;
        private ServiceContainer _lightInjectContainer;
        private StandardKernel _ninjectContainer;
        private UnityContainer _unityContainer;
        private ServiceProvider _microsoftContainer;

        public abstract TContainer CreateContainer<TContainer, TAbstractContainer>() where TAbstractContainer : IAbstractContainer<TContainer>, new();

        [GlobalSetup]
        public void Setup()
        {
            _iocContainer = CreateContainer<IoC.Container, IoCContainer>();
            _iocRootResolver = CreateContainer<Func<IServiceRoot>, IoCContainerByCompositionRoot<IServiceRoot>>();
            _autofacContainer = CreateContainer<global::Autofac.IContainer, Autofac>();
            _windsorContainerContainer = CreateContainer<WindsorContainer, CastleWindsor>();
            _dryIocContainer = CreateContainer<Container, DryIoc>();
            _lightInjectContainer = CreateContainer<ServiceContainer, LightInject>();
            _ninjectContainer = CreateContainer<StandardKernel, Ninject>();
            _unityContainer = CreateContainer<UnityContainer, Unity>();
            _microsoftContainer = CreateContainer<ServiceProvider, MicrosoftDependencyInjection>();
        }

        [Benchmark(Description = "IoC.Container", OperationsPerInvoke = Series * 10)]
        public void This()
        {
            for (var i = 0; i < Series; i++)
            {
                _iocContainer.Resolve<IServiceRoot>().DoSomething();
                _iocContainer.Resolve<IServiceRoot>().DoSomething();
                _iocContainer.Resolve<IServiceRoot>().DoSomething();
                _iocContainer.Resolve<IServiceRoot>().DoSomething();
                _iocContainer.Resolve<IServiceRoot>().DoSomething();
                _iocContainer.Resolve<IServiceRoot>().DoSomething();
                _iocContainer.Resolve<IServiceRoot>().DoSomething();
                _iocContainer.Resolve<IServiceRoot>().DoSomething();
                _iocContainer.Resolve<IServiceRoot>().DoSomething();
                _iocContainer.Resolve<IServiceRoot>().DoSomething();
            }
        }

        [Benchmark(Description = "IoC.Container composition root", OperationsPerInvoke = Series * 10)]
        // ReSharper disable once InconsistentNaming
        public void ThisByCR()
        {
            for (var i = 0; i < Series; i++)
            {
                _iocRootResolver().DoSomething();
                _iocRootResolver().DoSomething();
                _iocRootResolver().DoSomething();
                _iocRootResolver().DoSomething();
                _iocRootResolver().DoSomething();
                _iocRootResolver().DoSomething();
                _iocRootResolver().DoSomething();
                _iocRootResolver().DoSomething();
                _iocRootResolver().DoSomething();
                _iocRootResolver().DoSomething();
            }
        }

        [Benchmark]
        public void Autofac() => _autofacContainer.Resolve<IServiceRoot>().DoSomething();

        [Benchmark]
        public void CastleWindsor() => _windsorContainerContainer.Resolve<IServiceRoot>().DoSomething();

        [Benchmark(OperationsPerInvoke = Series * 10)]
        public void DryIoc()
        {
            for (var i = 0; i < Series; i++)
            {
                _dryIocContainer.Resolve<IServiceRoot>().DoSomething();
                _dryIocContainer.Resolve<IServiceRoot>().DoSomething();
                _dryIocContainer.Resolve<IServiceRoot>().DoSomething();
                _dryIocContainer.Resolve<IServiceRoot>().DoSomething();
                _dryIocContainer.Resolve<IServiceRoot>().DoSomething();
                _dryIocContainer.Resolve<IServiceRoot>().DoSomething();
                _dryIocContainer.Resolve<IServiceRoot>().DoSomething();
                _dryIocContainer.Resolve<IServiceRoot>().DoSomething();
                _dryIocContainer.Resolve<IServiceRoot>().DoSomething();
                _dryIocContainer.Resolve<IServiceRoot>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = Series * 10)]
        public void LightInject()
        {
            for (var i = 0; i < Series; i++)
            {
                _lightInjectContainer.GetInstance<IServiceRoot>().DoSomething();
                _lightInjectContainer.GetInstance<IServiceRoot>().DoSomething();
                _lightInjectContainer.GetInstance<IServiceRoot>().DoSomething();
                _lightInjectContainer.GetInstance<IServiceRoot>().DoSomething();
                _lightInjectContainer.GetInstance<IServiceRoot>().DoSomething();
                _lightInjectContainer.GetInstance<IServiceRoot>().DoSomething();
                _lightInjectContainer.GetInstance<IServiceRoot>().DoSomething();
                _lightInjectContainer.GetInstance<IServiceRoot>().DoSomething();
                _lightInjectContainer.GetInstance<IServiceRoot>().DoSomething();
                _lightInjectContainer.GetInstance<IServiceRoot>().DoSomething();
            }
        }

        [Benchmark]
        public void Ninject() => _ninjectContainer.Get<IServiceRoot>().DoSomething();

        [Benchmark]
        public void Unity() => _unityContainer.Resolve<IServiceRoot>().DoSomething();

        [Benchmark(OperationsPerInvoke = Series * 10)]
        public void MicrosoftDependencyInjection()
        {
            for (var i = 0; i < Series; i++)
            {
                _microsoftContainer.GetService<IServiceRoot>().DoSomething();
                _microsoftContainer.GetService<IServiceRoot>().DoSomething();
                _microsoftContainer.GetService<IServiceRoot>().DoSomething();
                _microsoftContainer.GetService<IServiceRoot>().DoSomething();
                _microsoftContainer.GetService<IServiceRoot>().DoSomething();
                _microsoftContainer.GetService<IServiceRoot>().DoSomething();
                _microsoftContainer.GetService<IServiceRoot>().DoSomething();
                _microsoftContainer.GetService<IServiceRoot>().DoSomething();
                _microsoftContainer.GetService<IServiceRoot>().DoSomething();
                _microsoftContainer.GetService<IServiceRoot>().DoSomething();
            }
        }
    }
}
