// ReSharper disable HeuristicUnreachableCode
#pragma warning disable 162
namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Autofac;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using JetBrains.dotMemoryUnit;
    using JetBrains.dotMemoryUnit.Kernel;
    using Ninject;
    using Unity;
    using Unity.Lifetime;
    using Xunit;
    using Xunit.Abstractions;

    public class ComparisonTests
    {
        private const string ThisIocName = "This";

        private static readonly Dictionary<string, Action<int, IPerformanceCounter>> IocsGraphOf3ObjectsWithSingletone = new Dictionary<string, Action<int, IPerformanceCounter>>()
        {
            { "Castle.Windsor", CastleWindsorGraphOf3ObjectsWithSingletone },
            { "Unity", UnityGraphOf3ObjectsWithSingletone },
            { "Ninject", NinjectGraphOf3ObjectsWithSingletone },
            { $"{ThisIocName} by Func", ThisByFuncGraphOf3ObjectsWithSingletone },
            { ThisIocName, ThisGraphOf3ObjectsWithSingletone },
            { "Autofac", AutofacGraphOf3ObjectsWithSingletone },
        };

        private static readonly Dictionary<string, Action<int, IPerformanceCounter>> IocsGraphOf3TransientObjects = new Dictionary<string, Action<int, IPerformanceCounter>>()
        {
            { "Castle.Windsor", CastleWindsorGraphOf3TransientObjects },
            { "Unity", UnityGraphOf3TransientObjects },
            { "Ninject", NinjectGraphOf3TransientObjects },
            { $"{ThisIocName} by Func", ThisByFuncGraphOf3TransientObjects },
            { ThisIocName, ThisGraphOf3TransientObjects },
            { "Autofac", AutofacGraphOf3TransientObjects },
        };

        public ComparisonTests(ITestOutputHelper output)
        {
            DotMemoryUnitTestOutput.SetOutputMethod(output.WriteLine);
        }

        [Fact]
        [Trait("Category", "Performance")]
        public void PerformanceTest()
        {
            const int series = 100000;

            var results = new List<TestResult>();
            foreach (var ioc in IocsGraphOf3ObjectsWithSingletone)
            {
                // Warmup
                ioc.Value(2, new TotalTimePerformanceCounter());

                var performanceCounter = new TotalTimePerformanceCounter();
                ioc.Value(series, performanceCounter);

                var result = new TestResult(ioc.Key, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Performance_GraphOf3ObjectsWithSingletone_{series}Times");
            results.Clear();

            foreach (var ioc in IocsGraphOf3TransientObjects)
            {
                // Warmup
                ioc.Value(2, new TotalTimePerformanceCounter());

                var performanceCounter = new TotalTimePerformanceCounter();
                ioc.Value(series, performanceCounter);

                var result = new TestResult(ioc.Key, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Performance_GraphOf3TransientObjects_{series}Times");
            results.Clear();
        }

        [Fact]
        [Trait("Category", "Memory")]
        [DotMemoryUnit(CollectAllocations = true)]
        public void MemoryTest()
        {
            if (!dotMemoryApi.IsEnabled)
            {
                return;
            }

            const int series = 10;

            var results = new List<TestResult>();
            foreach (var ioc in IocsGraphOf3ObjectsWithSingletone)
            {
                var performanceCounter = new MemoryPerformanceCounter();
                ioc.Value(series, performanceCounter);

                var result = new TestResult(ioc.Key, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Memory_GraphOf3ObjectsWithSingletone_{series}Times");
            results.Clear();

            foreach (var ioc in IocsGraphOf3TransientObjects)
            {
                var performanceCounter = new MemoryPerformanceCounter();
                ioc.Value(series, performanceCounter);

                var result = new TestResult(ioc.Key, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Memory_GraphOf3TransientObjects_{series}Times");
            results.Clear();
        }

        private static void ThisGraphOf3ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = Container.Create())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().Lifetime(Lifetime.Singletone).To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    container.Get<IService1>();
                }
            }
        }

        private static void ThisGraphOf3TransientObjects(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = Container.Create())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    container.Get<IService1>();
                }
            }
        }

        private static void ThisByFuncGraphOf3ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = Container.Create())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().Lifetime(Lifetime.Singletone).To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            using (performanceCounter.Run())
            {
                var func = container.Get<Func<IService1>>();
                for (var i = 0; i < series; i++)
                {
                    func();
                }
            }
        }

        private static void ThisByFuncGraphOf3TransientObjects(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = Container.Create())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            using (performanceCounter.Run())
            {
                var func = container.Get<Func<IService1>>();
                for (var i = 0; i < series; i++)
                {
                    func();
                }
            }
        }

        private static void UnityGraphOf3ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<IService1, Service1>();
                container.RegisterType<IService2, Service2>(new ContainerControlledLifetimeManager());
                container.RegisterType<IService3, Service3>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve(typeof(IService1));
                    }
                }
            }
        }

        private static void UnityGraphOf3TransientObjects(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<IService1, Service1>();
                container.RegisterType<IService2, Service2>(new ContainerControlledLifetimeManager());
                container.RegisterType<IService3, Service3>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve(typeof(IService1));
                    }
                }
            }
        }

        private static void NinjectGraphOf3ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind<IService1>().To<Service1>();
                kernel.Bind<IService2>().To<Service2>().InSingletonScope();
                kernel.Bind<IService3>().To<Service3>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        kernel.Get<IService1>();
                    }
                }
            }
        }

        private static void NinjectGraphOf3TransientObjects(int series, IPerformanceCounter performanceCounter)
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind<IService1>().To<Service1>();
                kernel.Bind<IService2>().To<Service2>();
                kernel.Bind<IService3>().To<Service3>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        kernel.Get<IService1>();
                    }
                }
            }
        }

        private static void AutofacGraphOf3ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Service1>().As<IService1>();
            builder.RegisterType<Service2>().As<IService2>().SingleInstance();
            builder.RegisterType<Service3>().As<IService3>();
            using (var container = builder.Build())
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    container.Resolve<IService1>();
                }
            }
        }

        private static void AutofacGraphOf3TransientObjects(int series, IPerformanceCounter performanceCounter)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Service1>().As<IService1>();
            builder.RegisterType<Service2>().As<IService2>();
            builder.RegisterType<Service3>().As<IService3>();
            using (var container = builder.Build())
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    container.Resolve<IService1>();
                }
            }
        }

        private static void CastleWindsorGraphOf3ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = new WindsorContainer())
            {
                container.Register(Component.For<IService1>().ImplementedBy<Service1>());
                container.Register(Component.For<IService2>().ImplementedBy<Service2>().LifestyleSingleton());
                container.Register(Component.For<IService3>().ImplementedBy<Service3>());
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<IService1>();
                    }
                }
            }
        }

        private static void CastleWindsorGraphOf3TransientObjects(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = new WindsorContainer())
            {
                container.Register(Component.For<IService1>().ImplementedBy<Service1>());
                container.Register(Component.For<IService2>().ImplementedBy<Service2>());
                container.Register(Component.For<IService3>().ImplementedBy<Service3>());
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<IService1>();
                    }
                }
            }
        }

        private static string GetBinDirectory()
        {
            return Environment.CurrentDirectory;
        }

        private static void SaveResults(IEnumerable<TestResult> results, string name)
        {
            var resultsStr = string.Join(Environment.NewLine, results.OrderBy(i => i).Select((item, index) => $"{index + 1:00} {item}"));
            var resultFileName = Path.Combine(GetBinDirectory(), $"{name}_Report.txt");
            File.WriteAllText(resultFileName, resultsStr);
        }
    }

    public interface IService1
    {
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public class Service1 : IService1
    {
        public Service1(IService2 service2, IService3 service3)
        {
        }
    }

    public interface IService2
    {
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class Service2 : IService2
    {
    }

    public interface IService3
    {
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class Service3 : IService3
    {
    }
}
