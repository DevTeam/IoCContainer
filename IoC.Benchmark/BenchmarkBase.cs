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
    using ICompositionRoot = Model.ICompositionRoot;

    public abstract class BenchmarkBase: IBenchmarkStrategy
    {
        private const int Series = 100000;
        private IoC.Container _iocContainer;
        private Func<ICompositionRoot> _iocRootResolver;
        private global::Autofac.IContainer _autofacContainer;
        private WindsorContainer _windsorContainerContainer;
        private Container _dryIocContainer;
        private ServiceContainer _lightInjectContainer;
        private StandardKernel _ninjectContainer;
        private UnityContainer _unityContainer;
        private ServiceProvider _microsoftContainer;
        private global::SimpleInjector.Container _simpleInjectorContainer;

        [CanBeNull] public abstract TContainer CreateContainer<TContainer, TAbstractContainer>() where TAbstractContainer : IAbstractContainer<TContainer>, new();

        [GlobalSetup]
        public void Setup()
        {
            _iocContainer = CreateContainer<IoC.Container, IoCContainer>();
            _iocRootResolver = CreateContainer<Func<ICompositionRoot>, IoCContainerByCompositionRoot<ICompositionRoot>>();
            _autofacContainer = CreateContainer<global::Autofac.IContainer, Autofac>();
            _windsorContainerContainer = CreateContainer<WindsorContainer, CastleWindsor>();
            _dryIocContainer = CreateContainer<Container, DryIoc>();
            _lightInjectContainer = CreateContainer<ServiceContainer, LightInject>();
            _ninjectContainer = CreateContainer<StandardKernel, Ninject>();
            _unityContainer = CreateContainer<UnityContainer, Unity>();
            _microsoftContainer = CreateContainer<ServiceProvider, MicrosoftDependencyInjection>();
            _simpleInjectorContainer = CreateContainer<global::SimpleInjector.Container, SimpleInjector>();
        }

        [Benchmark(Description = "IoC.Container", OperationsPerInvoke = Series * 10)]
        public void This()
        {
            for (var i = 0; i < Series; i++)
            {
                _iocContainer.Resolve<ICompositionRoot>().DoSomething();
                _iocContainer.Resolve<ICompositionRoot>().DoSomething();
                _iocContainer.Resolve<ICompositionRoot>().DoSomething();
                _iocContainer.Resolve<ICompositionRoot>().DoSomething();
                _iocContainer.Resolve<ICompositionRoot>().DoSomething();
                _iocContainer.Resolve<ICompositionRoot>().DoSomething();
                _iocContainer.Resolve<ICompositionRoot>().DoSomething();
                _iocContainer.Resolve<ICompositionRoot>().DoSomething();
                _iocContainer.Resolve<ICompositionRoot>().DoSomething();
                _iocContainer.Resolve<ICompositionRoot>().DoSomething();
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
        public void Autofac() => _autofacContainer.Resolve<ICompositionRoot>().DoSomething();

        [Benchmark]
        public void CastleWindsor() => _windsorContainerContainer.Resolve<ICompositionRoot>().DoSomething();

        [Benchmark(OperationsPerInvoke = Series * 10)]
        public void DryIoc()
        {
            for (var i = 0; i < Series; i++)
            {
                _dryIocContainer.Resolve<ICompositionRoot>().DoSomething();
                _dryIocContainer.Resolve<ICompositionRoot>().DoSomething();
                _dryIocContainer.Resolve<ICompositionRoot>().DoSomething();
                _dryIocContainer.Resolve<ICompositionRoot>().DoSomething();
                _dryIocContainer.Resolve<ICompositionRoot>().DoSomething();
                _dryIocContainer.Resolve<ICompositionRoot>().DoSomething();
                _dryIocContainer.Resolve<ICompositionRoot>().DoSomething();
                _dryIocContainer.Resolve<ICompositionRoot>().DoSomething();
                _dryIocContainer.Resolve<ICompositionRoot>().DoSomething();
                _dryIocContainer.Resolve<ICompositionRoot>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = Series * 10)]
        public void SimpleInjector()
        {
            for (var i = 0; i < Series; i++)
            {
                _simpleInjectorContainer.GetInstance<ICompositionRoot>().DoSomething();
                _simpleInjectorContainer.GetInstance<ICompositionRoot>().DoSomething();
                _simpleInjectorContainer.GetInstance<ICompositionRoot>().DoSomething();
                _simpleInjectorContainer.GetInstance<ICompositionRoot>().DoSomething();
                _simpleInjectorContainer.GetInstance<ICompositionRoot>().DoSomething();
                _simpleInjectorContainer.GetInstance<ICompositionRoot>().DoSomething();
                _simpleInjectorContainer.GetInstance<ICompositionRoot>().DoSomething();
                _simpleInjectorContainer.GetInstance<ICompositionRoot>().DoSomething();
                _simpleInjectorContainer.GetInstance<ICompositionRoot>().DoSomething();
                _simpleInjectorContainer.GetInstance<ICompositionRoot>().DoSomething();
            }
        }

        [Benchmark(OperationsPerInvoke = Series * 10)]
        public void LightInject()
        {
            for (var i = 0; i < Series; i++)
            {
                _lightInjectContainer.GetInstance<ICompositionRoot>().DoSomething();
                _lightInjectContainer.GetInstance<ICompositionRoot>().DoSomething();
                _lightInjectContainer.GetInstance<ICompositionRoot>().DoSomething();
                _lightInjectContainer.GetInstance<ICompositionRoot>().DoSomething();
                _lightInjectContainer.GetInstance<ICompositionRoot>().DoSomething();
                _lightInjectContainer.GetInstance<ICompositionRoot>().DoSomething();
                _lightInjectContainer.GetInstance<ICompositionRoot>().DoSomething();
                _lightInjectContainer.GetInstance<ICompositionRoot>().DoSomething();
                _lightInjectContainer.GetInstance<ICompositionRoot>().DoSomething();
                _lightInjectContainer.GetInstance<ICompositionRoot>().DoSomething();
            }
        }

        [Benchmark]
        public void Ninject() => _ninjectContainer.Get<ICompositionRoot>().DoSomething();

        [Benchmark]
        public void Unity() => _unityContainer.Resolve<ICompositionRoot>().DoSomething();

        [Benchmark(OperationsPerInvoke = Series * 10)]
        public void MicrosoftDependencyInjection()
        {
            for (var i = 0; i < Series; i++)
            {
                _microsoftContainer.GetService<ICompositionRoot>().DoSomething();
                _microsoftContainer.GetService<ICompositionRoot>().DoSomething();
                _microsoftContainer.GetService<ICompositionRoot>().DoSomething();
                _microsoftContainer.GetService<ICompositionRoot>().DoSomething();
                _microsoftContainer.GetService<ICompositionRoot>().DoSomething();
                _microsoftContainer.GetService<ICompositionRoot>().DoSomething();
                _microsoftContainer.GetService<ICompositionRoot>().DoSomething();
                _microsoftContainer.GetService<ICompositionRoot>().DoSomething();
                _microsoftContainer.GetService<ICompositionRoot>().DoSomething();
                _microsoftContainer.GetService<ICompositionRoot>().DoSomething();
            }
        }
    }
}
