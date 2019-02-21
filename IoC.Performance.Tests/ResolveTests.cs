namespace IoC.Performance.Tests
{
    using System;
    using Model;
    using NUnit.Framework;
    using static Lifetime;

    public class ResolveTests
    {
        private readonly long _series;
        private Container _containerComplex;
        private Container _containerCore;
        private Container _container;

        public ResolveTests()
        {
            if (!long.TryParse(Environment.GetEnvironmentVariable("SERIES"), out _series))
            {
                _series = 10000000;
            }       
        }

        [SetUp]
        public void SetUp()
        {
            _containerComplex = Container.CreateCore();
            foreach (var type in TestTypeBuilder.Default.Types)
            {
                _containerComplex.Bind(type).To(type).ToSelf();
            }

            _containerCore = Container.CreateCore();
            _containerCore.Bind<IService1>().To<Service1>().ToSelf();
            _containerCore.Bind<IService2>().As(Singleton).To<Service2>().ToSelf();
            _containerCore.Bind<IService3>().To<Service3>().ToSelf();
            _containerCore.Bind<IService4>().To<Service4>().ToSelf();

            _container = Container.Create();
            _container.Bind<IService1>().To<Service1>().ToSelf();
            _container.Bind<IService2>().As(Singleton).To<Service2>().ToSelf();
            _container.Bind<IService3>().To<Service3>().ToSelf();
            _container.Bind<IService4>().To<Service4>().ToSelf();           
        }

        [TearDown]
        public void TearDown()
        {
            _containerComplex.Dispose();
            _containerCore.Dispose();
            _container.Dispose();
        }

        [Test]
        public void ResolveCore()
        {          
            for (var i = 0; i < _series; i++)
            {
                _containerCore.Resolve<IService1>().DoSomething();
            }
        }

        [Test]
        public void Resolve()
        {          
            for (var i = 0; i < _series; i++)
            {
                _container.Resolve<IService1>().DoSomething();
            }
        }

        [Test]
        public void ResolveComplex()
        {          
            for (var i = 0; i < _series / 1000; i++)
            {
                _containerComplex.Resolve<object>(TestTypeBuilder.Default.RootType);
            } 
        }
    }
}