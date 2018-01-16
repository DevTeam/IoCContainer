// ReSharper disable HeuristicUnreachableCode
#pragma warning disable 162
namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using JetBrains.dotMemoryUnit;
    using JetBrains.dotMemoryUnit.Kernel;
    using LightInject;
    using Ninject;
    using Unity;
    using Unity.Lifetime;
    using Xunit;
    using Xunit.Abstractions;

    public class ComparisonTests
    {
        private const string ThisIocName = "this";
        private const string ReportFileName = "REPORT.html";

        private static readonly Dictionary<string, Action<int, IPerformanceCounter>> IocsGraphOf3ObjectsWithSingletone = new Dictionary<string, Action<int, IPerformanceCounter>>()
        {
            { "Without IoC", CtorGraphOf5ObjectsWithSingletone },
#if NETCOREAPP2_0
            { "LightInject", LightInjectGraphOf5ObjectsWithSingletone },
#endif
            { $"{ThisIocName} for actual scenario", ThisByFuncGraphOf5ObjectsWithSingletone },
            { ThisIocName, ThisGraphOf5ObjectsWithSingletone },
            { "Castle.Windsor", CastleWindsorGraphOf5ObjectsWithSingletone },
            { "Unity", UnityGraphOf5ObjectsWithSingletone },
            { "Ninject", NinjectGraphOf5ObjectsWithSingletone },
            { "Autofac", AutofacGraphOf5ObjectsWithSingletone },
        };

        private static readonly Dictionary<string, Action<int, IPerformanceCounter>> IocsGraphOf3TransientObjects = new Dictionary<string, Action<int, IPerformanceCounter>>()
        {
            { "Without IoC", CtorGraphOf6TransientObjects },
#if NETCOREAPP2_0
            { "LightInject", LightInjectGraphOf6TransientObjects },
#endif
            { $"{ThisIocName} for actual scenario", ThisByFuncGraphOf6TransientObjects },
            { ThisIocName, ThisGraphOf6TransientObjects },
            { "Castle.Windsor", CastleWindsorGraphOf6TransientObjects },
            { "Unity", UnityGraphOf6TransientObjects },
            { "Ninject", NinjectGraphOf6TransientObjects },
            { "Autofac", AutofacGraphOf6TransientObjects },
        };

        public ComparisonTests(ITestOutputHelper output)
        {
            DotMemoryUnitTestOutput.SetOutputMethod(output.WriteLine);
        }

        [Fact]
        [Trait("Category", "Performance")]
        public void PerformanceTest()
        {
            var isUnderTeamCity = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEAMCITY_VERSION") ?? string.Empty);
            var series = isUnderTeamCity ? 1000000 : 10000;

            var results = new List<TestResult>();
            foreach (var ioc in IocsGraphOf3ObjectsWithSingletone)
            {
                // Warmup
                GC.Collect();
                ioc.Value(2, new TotalTimePerformanceCounter());

                var performanceCounter = new TotalTimePerformanceCounter();
                ioc.Value(series, performanceCounter);

                var result = new TestResult(ioc.Key, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Speed: 5 objects and 1 singletone {series} times");
            results.Clear();

            foreach (var ioc in IocsGraphOf3TransientObjects)
            {
                // Warmup
                GC.Collect();
                ioc.Value(2, new TotalTimePerformanceCounter());

                var performanceCounter = new TotalTimePerformanceCounter();
                ioc.Value(series, performanceCounter);

                var result = new TestResult(ioc.Key, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Speed: 6 objects {series} times");
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
                GC.Collect();
                var performanceCounter = new MemoryPerformanceCounter();
                ioc.Value(series, performanceCounter);

                var result = new TestResult(ioc.Key, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Memory usage: 5 objects and 1 singletone {series} times");
            results.Clear();

            foreach (var ioc in IocsGraphOf3TransientObjects)
            {
                GC.Collect();
                var performanceCounter = new MemoryPerformanceCounter();
                ioc.Value(series, performanceCounter);

                var result = new TestResult(ioc.Key, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Memory usage: 6 objects {series} times");
            results.Clear();
        }

        private static void ThisGraphOf5ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = Container.Create())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().Lifetime(Lifetime.Singletone).To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    container.Get<IService1>().DoSomething();
                }
            }
        }

        private static void ThisGraphOf6TransientObjects(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = Container.Create())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    container.Get<IService1>().DoSomething();
                }
            }
        }

        private static void ThisByFuncGraphOf5ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = Container.Create())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().Lifetime(Lifetime.Singletone).To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            {
                container.TryGetResolver<IService1>(out var factory);
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        factory(container).DoSomething();
                    }
                }
            }
        }

        private static void ThisByFuncGraphOf6TransientObjects(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = Container.Create())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            {
                container.TryGetResolver<IService1>(out var factory);
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        factory(container).DoSomething();
                    }
                }
            }
        }

        private static void UnityGraphOf5ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
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
                        container.Resolve<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void UnityGraphOf6TransientObjects(int series, IPerformanceCounter performanceCounter)
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
                        container.Resolve<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void NinjectGraphOf5ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
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
                        kernel.Get<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void NinjectGraphOf6TransientObjects(int series, IPerformanceCounter performanceCounter)
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
                        kernel.Get<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void AutofacGraphOf5ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
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
                    container.Resolve<IService1>().DoSomething();
                }
            }
        }

        private static void AutofacGraphOf6TransientObjects(int series, IPerformanceCounter performanceCounter)
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
                    container.Resolve<IService1>().DoSomething();
                }
            }
        }

        private static void CastleWindsorGraphOf5ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
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
                        container.Resolve<IService1>().DoSomething(); ;
                    }
                }
            }
        }

        private static void CastleWindsorGraphOf6TransientObjects(int series, IPerformanceCounter performanceCounter)
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
                        container.Resolve<IService1>().DoSomething(); ;
                    }
                }
            }
        }

        private static void LightInjectGraphOf5ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = new ServiceContainer())
            {
                container.Register<IService1, Service1>();
                container.Register<IService2, Service2>(new PerContainerLifetime());
                container.Register<IService3, Service3>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.GetInstance<IService1>().DoSomething(); ;
                    }
                }
            }
        }

        private static void LightInjectGraphOf6TransientObjects(int series, IPerformanceCounter performanceCounter)
        {
            using (var container = new ServiceContainer())
            {
                container.Register<IService1, Service1>();
                container.Register<IService2, Service2>();
                container.Register<IService3, Service3>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.GetInstance<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void CtorGraphOf5ObjectsWithSingletone(int series, IPerformanceCounter performanceCounter)
        {
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    var service2 = new Service2(new Service3());
                    IService1 service1 = new Service1(service2, new Service3(), new Service3(), new Service3());
                    service1.DoSomething();
                }
            }
        }

        private static void CtorGraphOf6TransientObjects(int series, IPerformanceCounter performanceCounter)
        {
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    IService1 service1 = new Service1(new Service2(new Service3()), new Service3(), new Service3(), new Service3());
                    service1.DoSomething();
                }
            }
        }

        private static string GetBinDirectory()
        {
            return Environment.CurrentDirectory;
        }

        private static string GetFramework()
        {
            return Path.GetFileName(GetBinDirectory());
        }

        private static string GetReportFilePath()
        {
            return Path.Combine(Path.Combine(GetBinDirectory(), ".."), ReportFileName);
        }

        private static void SaveResults(IEnumerable<TestResult> results, string name)
        {
            var body = new StringBuilder();
            body.AppendLine($"<h2>{name}</h2>");
            body.AppendLine($"<h3>{GetFramework()}</h3>");
            body.AppendLine("<table border='1'>");
            body.AppendLine("<tr>");
            body.AppendLine("<th>#</th>");
            body.AppendLine("<th>IoC</th>");
            body.AppendLine("<th>result</th>");
            body.AppendLine("</tr>");
            foreach (var line in results.OrderBy(i => i).Select((item, index) => $"<td>{index + 1:00}</td>{item}"))
            {
                body.AppendLine("<tr>");
                body.AppendLine(line);
                body.AppendLine("</tr>");
            }
            body.AppendLine("</table>");
            File.AppendAllText(GetReportFilePath(), body.ToString());
        }
    }

    public interface IService1
    {
        void DoSomething();
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class Service1 : IService1
    {
        public Service1(IService2 service2, IService3 service31, IService3 service32, IService3 service33)
        {
        }

        public void DoSomething()
        {
        }
    }

    public interface IService2
    {
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Service2 : IService2
    {
        // ReSharper disable once UnusedParameter.Local
        public Service2(IService3 service3)
        {
        }
    }

    public interface IService3
    {
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Service3 : IService3
    {
    }
}
