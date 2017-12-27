// ReSharper disable HeuristicUnreachableCode
// ReSharper disable RedundantUsingDirective
#pragma warning disable 162
namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
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
        private static readonly Dictionary<string, Action<int, IPerformanceCounter>> Iocs = new Dictionary<string, Action<int, IPerformanceCounter>>()
        {
            { "Castle.Windsor", CastleWindsor },
            { "Unity", Unity },
            { "Ninject", Ninject },
            { "This by Func", ThisByFunc },
            { "This", This },
            { "Autofac", Autofac },
        };

        public ComparisonTests(ITestOutputHelper output)
        {
            DotMemoryUnitTestOutput.SetOutputMethod(output.WriteLine);
        }

        [Fact]
        public void PerformanceTest()
        {
            const int series = 100000;
            var results = new List<TestResult>();
            foreach (var ioc in Iocs)
            {
                // Warmup
                ioc.Value(2, new TotalTimePerformanceCounter());

                var performanceCounter = new TotalTimePerformanceCounter();
                ioc.Value(series, performanceCounter);

                var result = new TestResult(ioc.Key, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Performance{series}");
        }

#if !NET45
        [Fact]
        [DotMemoryUnit(CollectAllocations = true)]
        public void MemoryTest()
        {
            if (!dotMemoryApi.IsEnabled)
            {
                return;
            }

            const int series = 10;
            var results = new List<TestResult>();
            foreach (var ioc in Iocs)
            {
                var performanceCounter = new MemoryPerformanceCounter();
                ioc.Value(series, performanceCounter);

                var result = new TestResult(ioc.Key, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Memory{series}");
        }
#endif

        private static void SaveResults(IEnumerable<TestResult> results, string name)
        {
            var resultsStr = string.Join(Environment.NewLine, results.OrderBy(i => i).Select((item, index) => $"{index + 1:00} {item}"));
            var resultFileName = Path.Combine(GetBinDirectory(), $"{name}Report.txt");
            File.WriteAllText(resultFileName, resultsStr);
        }

        private static void This(int series, IPerformanceCounter performanceCounter)
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

        private static void ThisByFunc(int series, IPerformanceCounter performanceCounter)
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

        private static void Unity(int series, IPerformanceCounter performanceCounter)
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

        private static void Ninject(int series, IPerformanceCounter performanceCounter)
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

        private static void Autofac(int series, IPerformanceCounter performanceCounter)
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

        private static void CastleWindsor(int series, IPerformanceCounter performanceCounter)
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

        private static string GetBinDirectory()
        {
            return Path.GetDirectoryName(typeof(ComparisonTests).GetTypeInfo().Assembly.Location);
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
