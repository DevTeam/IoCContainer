namespace IoC.Performance.Tests
{
    using System;
    using Model;
    using Xunit;
    using static Lifetime;

    public class ResolveTests
    {
        private readonly long _series;
        private readonly Container _containerComplex;
        private readonly Container _containerCore;
        private readonly Container _container;

        public ResolveTests()
        {
            if (!long.TryParse(Environment.GetEnvironmentVariable("SERIES"), out _series))
            {
                _series = 10000000;
            }

            _containerComplex = Container.Create("Core", Features.Set.Core);
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                _containerComplex.Bind(type).To(type);
            }

            _containerCore = Container.Create("Core", Features.Set.Core);
            _containerCore.Bind<IService1>().To<Service1>();
            _containerCore.Bind<IService2>().As(Singleton).To<Service2>();
            _containerCore.Bind<IService3>().To<Service3>();
            _containerCore.Bind<IService4>().To<Service4>();

            _container = Container.Create();
            _container.Bind<IService1>().To<Service1>();
            _container.Bind<IService2>().As(Singleton).To<Service2>();
            _container.Bind<IService3>().To<Service3>();
            _container.Bind<IService4>().To<Service4>();
        }

        [Fact]
        public void TestResolveCore()
        {          
            for (var i = 0; i < _series; i++)
            {
                _containerCore.Resolve<IService1>().DoSomething();
            }
        }

        [Fact]
        public void TestResolve()
        {          
            for (var i = 0; i < _series; i++)
            {
                _container.Resolve<IService1>().DoSomething();
            }
        }

        [Fact]
        public void TestResolveComplex()
        {          
            for (var i = 0; i < _series / 1000; i++)
            {
                _containerComplex.Resolve<object>(TestTypeBuilder.Default.RootType);
            } 
        }
    }
}