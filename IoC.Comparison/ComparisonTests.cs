// ReSharper disable HeuristicUnreachableCode
// ReSharper disable RedundantUsingDirective
#pragma warning disable 162
namespace IoC.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using Autofac;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using DryIoc;
    using LightInject;
    using Model;
    using JetBrains.dotMemoryUnit;
    using JetBrains.dotMemoryUnit.Kernel;
    using Ninject;
    using Unity;
    using Unity.Lifetime;
    using Xunit;
#if NET5 || NETCOREAPP3_1
    using Xunit.Abstractions;
#endif

    using ThisContainer = Container;
    using static Lifetime;

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "EmptyConstructor")]
    public class ComparisonTests
    {
        private const string ThisIocName = "IoC.Container";
        private const string ReportFileName = "REPORT.html";

        private static readonly TestTypeBuilder TestTypeBuilder = new TestTypeBuilder(5, 4);

        private static readonly List<TestInfo> IoCGraphOf3ObjectsWithSingleton = new List<TestInfo>
        {
            new TestInfo($"{ThisIocName} by the Composition Root", ThisByCompositionRootSingleton),
            new TestInfo(ThisIocName, ThisSingleton),
            new TestInfo("operators 'new'", CtorSingleton),
            new TestInfo("LightInject", LightInjectSingleton),
            new TestInfo("DryIoc", DryIocSingleton),
            new TestInfo("Castle Windsor", CastleWindsorSingleton) { PerformanceRate = 800 },
            new TestInfo("Unity", UnitySingleton) { PerformanceRate = 800 },
            new TestInfo("Ninject", NinjectSingleton) { PerformanceRate = 1000 },
            new TestInfo("Autofac", AutofacSingleton) { PerformanceRate = 100 },
        };

        private static readonly List<TestInfo> IoCGraphOf3Transient = new List<TestInfo>
        {
            new TestInfo($"{ThisIocName} by the Composition Root", ThisByCompositionRootTransient),
            new TestInfo(ThisIocName, ThisTransient),
            new TestInfo("operators 'new'", CtorTransient),
            new TestInfo("LightInject", LightInjectTransient),
            new TestInfo("DryIoc", DryIocTransient),
            new TestInfo("Castle Windsor", CastleWindsorTransient) { PerformanceRate = 800 },
            new TestInfo("Unity", UnityTransient) { PerformanceRate = 800 },
            new TestInfo("Ninject", NinjectTransient) { PerformanceRate = 1000 },
            new TestInfo("Autofac", AutofacTransient) { PerformanceRate = 100 },
        };

        private static readonly List<TestInfo> IoCComplexGraphOf3Transient = new List<TestInfo>
        {
            new TestInfo($"{ThisIocName} by the Composition Root", ThisByCompositionRootComplex),
            new TestInfo(ThisIocName, ThisComplex),
            new TestInfo("LightInject", LightInjectComplex),
            new TestInfo("DryIoc", DryIocComplex),
            new TestInfo("Castle Windsor", CastleWindsorComplex) { PerformanceRate = 800 },
            new TestInfo("Unity", UnityComplex) { PerformanceRate = 800 },
            new TestInfo("Ninject", NinjectComplex) { PerformanceRate = 1000 },
            new TestInfo("Autofac", AutofacComplex) { PerformanceRate = 100 },
        };

#if NET5 || NETCOREAPP3_1
        public ComparisonTests(ITestOutputHelper output)
        {
            DotMemoryUnitTestOutput.SetOutputMethod(output.WriteLine);
        }
#else
        public ComparisonTests()
        {
        }
#endif

        [Fact]
        public void ConfigureContainerTest()
        {
            var performanceCounter = new TotalTimePerformanceCounter();
            for (var i = 0; i < 10000; i++)
            {
                ThisSingleton(0, performanceCounter);
            }
        }

        [Fact]
        [Trait("Category", "Performance")]
        public void PerformanceTest()
        {
            if (!long.TryParse(Environment.GetEnvironmentVariable("SERIES"), out var series))
            {
                series = 10000000;
            }

            var results = new List<TestResult>();
            foreach (var ioc in IoCGraphOf3ObjectsWithSingleton)
            {
                // Warm up
                ioc.Test(2, new TotalTimePerformanceCounter());
                GC.Collect();
                GC.WaitForFullGCComplete();

                var performanceCounter = new TotalTimePerformanceCounter(ioc.PerformanceRate);
                ioc.Test((int)(series/ioc.PerformanceRate), performanceCounter);

                var result = new TestResult(ioc.Name, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"20 instances and 1 singleton {series.ToShortString()} times");
            results.Clear();

            foreach (var ioc in IoCGraphOf3Transient)
            {
                // Warm up
                ioc.Test(2, new TotalTimePerformanceCounter());
                GC.Collect();
                GC.WaitForFullGCComplete();

                var performanceCounter = new TotalTimePerformanceCounter(ioc.PerformanceRate);
                ioc.Test((int)(series / ioc.PerformanceRate), performanceCounter);

                var result = new TestResult(ioc.Name, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"27 instances {series.ToShortString()} times");
            results.Clear();

            series /= 1000;
            foreach (var ioc in IoCComplexGraphOf3Transient)
            {
                // Warm up
                ioc.Test(2, new TotalTimePerformanceCounter());
                GC.Collect();
                GC.WaitForFullGCComplete();

                var performanceCounter = new TotalTimePerformanceCounter(ioc.PerformanceRate);
                ioc.Test((int)(series / ioc.PerformanceRate), performanceCounter);

                var result = new TestResult(ioc.Name, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"{TestTypeBuilder.Count} unique instances {series.ToShortString()} times");
            results.Clear();
        }

        [Fact]
        [Trait("Category", "Performance")]
        public void ComplexGraphPerformanceTest()
        {
            using (var container = ThisContainer.CreateCore())
            {
                foreach (var type in TestTypeBuilder.Types)
                {
                    container.Bind(type).To(type);
                }

                container.Resolve<object>(TestTypeBuilder.RootType);
            }
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

            const long series = 10;

            var results = new List<TestResult>();
            foreach (var ioc in IoCGraphOf3ObjectsWithSingleton)
            {
                GC.Collect();
                GC.WaitForFullGCComplete();
                var performanceCounter = new MemoryPerformanceCounter();
                ioc.Test(series, performanceCounter);

                var result = new TestResult(ioc.Name, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Memory usage 5 instances and 1 singleton {series} times");
            results.Clear();

            foreach (var ioc in IoCGraphOf3Transient)
            {
                GC.Collect();
                GC.WaitForFullGCComplete();
                var performanceCounter = new MemoryPerformanceCounter();
                ioc.Test(series, performanceCounter);

                var result = new TestResult(ioc.Name, performanceCounter.Result);
                results.Add(result);
            }

            SaveResults(results, $"Memory usage 6 instances {series} times");
            results.Clear();
        }

        private static void ThisSingleton(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = ThisContainer.CreateCore())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().As(Singleton).To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            using (container.Bind<IService4>().To<Service4>())
            {
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void ThisTransient(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = ThisContainer.CreateCore())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            using (container.Bind<IService4>().To<Service4>())
            {
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void ThisComplex(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = ThisContainer.CreateCore())
            {
                foreach (var type in TestTypeBuilder.Types)
                {
                    container.Bind(type).To(type);
                }
                
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<object>(TestTypeBuilder.RootType);
                    }
                }
            }
        }

        private static void ThisByCompositionRootSingleton(long series, IPerformanceCounter performanceCounter)
        {
            var emptyArgs = new object[0];
            using (var container = ThisContainer.CreateCore())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().As(Singleton).To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            using (container.Bind<IService4>().To<Service4>())
            {
                using (performanceCounter.Run())
                {
                    container.TryGetResolver<IService1>(typeof(IService1), null, out var resolver, out _);
                    for (var i = 0; i < series; i++)
                    {
                        resolver(container, emptyArgs).DoSomething();
                    }
                }
            }
        }

        private static void ThisByCompositionRootTransient(long series, IPerformanceCounter performanceCounter)
        {
            var emptyArgs = new object[0];
            using (var container = ThisContainer.CreateCore())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().To<Service2>())
            using (container.Bind<IService3>().To<Service3>())
            using (container.Bind<IService4>().To<Service4>())
            {
                using (performanceCounter.Run())
                {
                    container.TryGetResolver<IService1>(typeof(IService1), null, out var resolver, out _);
                    for (var i = 0; i < series; i++)
                    {
                        resolver(container, emptyArgs).DoSomething();
                    }
                }
            }
        }

        private static void ThisByCompositionRootComplex(long series, IPerformanceCounter performanceCounter)
        {
            var emptyArgs = new object[0];
            using (var container = ThisContainer.CreateCore())
            {
                foreach (var type in TestTypeBuilder.Types)
                {
                    container.Bind(type).To(type);
                }
                
                using (performanceCounter.Run())
                {
                    container.TryGetResolver<object>(TestTypeBuilder.RootType, null, out var resolver, out _);
                    for (var i = 0; i < series; i++)
                    {
                        resolver(container, emptyArgs);
                    }
                }
            }
        }

        private static void UnitySingleton(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<IService1, Service1>();
                container.RegisterType<IService2, Service2>(new ContainerControlledLifetimeManager());
                container.RegisterType<IService3, Service3>();
                container.RegisterType<IService4, Service4>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void UnityTransient(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<IService1, Service1>();
                container.RegisterType<IService2, Service2>(new ContainerControlledLifetimeManager());
                container.RegisterType<IService3, Service3>();
                container.RegisterType<IService4, Service4>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void UnityComplex(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new UnityContainer())
            {
                foreach (var type in TestTypeBuilder.Types)
                {
                    container.RegisterType(type, type);
                }
                
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve(TestTypeBuilder.RootType);
                    }
                }
            }
        }

        private static void NinjectSingleton(long series, IPerformanceCounter performanceCounter)
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind<IService1>().To<Service1>();
                kernel.Bind<IService2>().To<Service2>().InSingletonScope();
                kernel.Bind<IService3>().To<Service3>();
                kernel.Bind<IService4>().To<Service4>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        kernel.Get<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void NinjectTransient(long series, IPerformanceCounter performanceCounter)
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind<IService1>().To<Service1>();
                kernel.Bind<IService2>().To<Service2>();
                kernel.Bind<IService3>().To<Service3>();
                kernel.Bind<IService4>().To<Service4>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        kernel.Get<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void NinjectComplex(long series, IPerformanceCounter performanceCounter)
        {
            using (var kernel = new StandardKernel())
            {
                foreach (var type in TestTypeBuilder.Types)
                {
                    kernel.Bind(type).To(type);
                }

                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        kernel.Get(TestTypeBuilder.RootType);
                    }
                }
            }
        }

        private static void AutofacSingleton(long series, IPerformanceCounter performanceCounter)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Service1>().As<IService1>();
            builder.RegisterType<Service2>().As<IService2>().SingleInstance();
            builder.RegisterType<Service3>().As<IService3>();
            builder.RegisterType<Service4>().As<IService4>();
            using (var container = builder.Build())
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    container.Resolve<IService1>().DoSomething();
                }
            }
        }

        private static void AutofacTransient(long series, IPerformanceCounter performanceCounter)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Service1>().As<IService1>();
            builder.RegisterType<Service2>().As<IService2>();
            builder.RegisterType<Service3>().As<IService3>();
            builder.RegisterType<Service4>().As<IService4>();
            using (var container = builder.Build())
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    container.Resolve<IService1>().DoSomething();
                }
            }
        }

        private static void AutofacComplex(long series, IPerformanceCounter performanceCounter)
        {
            var builder = new ContainerBuilder();
            foreach (var type in TestTypeBuilder.Types)
            {
                builder.RegisterType(type).As(type);
            }
            
            using (var container = builder.Build())
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    container.Resolve(TestTypeBuilder.RootType);
                }
            }
        }

        private static void CastleWindsorSingleton(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new WindsorContainer())
            {
                container.Register(Component.For<IService1>().ImplementedBy<Service1>().LifestyleTransient());
                container.Register(Component.For<IService2>().ImplementedBy<Service2>().LifestyleSingleton());
                container.Register(Component.For<IService3>().ImplementedBy<Service3>().LifestyleTransient());
                container.Register(Component.For<IService4>().ImplementedBy<Service4>().LifestyleTransient());
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void CastleWindsorTransient(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new WindsorContainer())
            {
                container.Register(Component.For<IService1>().ImplementedBy<Service1>().LifestyleTransient());
                container.Register(Component.For<IService2>().ImplementedBy<Service2>().LifestyleTransient());
                container.Register(Component.For<IService3>().ImplementedBy<Service3>().LifestyleTransient());
                container.Register(Component.For<IService4>().ImplementedBy<Service4>().LifestyleTransient());
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void CastleWindsorComplex(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new WindsorContainer())
            {
                foreach (var type in TestTypeBuilder.Types)
                {
                    container.Register(Component.For(type).ImplementedBy(type).LifestyleTransient());
                }
                
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve(TestTypeBuilder.RootType);
                    }
                }
            }
        }

        private static void LightInjectSingleton(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new ServiceContainer())
            {
                container.Register<IService1, Service1>();
                container.Register<IService2, Service2>(new PerContainerLifetime());
                container.Register<IService3, Service3>();
                container.Register<IService4, Service4>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.GetInstance<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void LightInjectTransient(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new ServiceContainer())
            {
                container.Register<IService1, Service1>();
                container.Register<IService2, Service2>();
                container.Register<IService3, Service3>();
                container.Register<IService4, Service4>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.GetInstance<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void LightInjectComplex(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new ServiceContainer())
            {
                foreach (var type in TestTypeBuilder.Types)
                {
                    container.Register(type);
                }
                
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.GetInstance(TestTypeBuilder.RootType);
                    }
                }
            }
        }

        private static void DryIocSingleton(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new Container())
            {
                container.Register<IService1, Service1>();
                container.Register<IService2, Service2>(Reuse.Singleton);
                container.Register<IService3, Service3>();
                container.Register<IService4, Service4>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void DryIocTransient(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new Container())
            {
                container.Register<IService1, Service1>();
                container.Register<IService2, Service2>();
                container.Register<IService3, Service3>();
                container.Register<IService4, Service4>();
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve<IService1>().DoSomething();
                    }
                }
            }
        }

        private static void DryIocComplex(long series, IPerformanceCounter performanceCounter)
        {
            using (var container = new Container())
            {
                foreach (var type in TestTypeBuilder.Types)
                {
                    container.Register(type);
                }
                
                using (performanceCounter.Run())
                {
                    for (var i = 0; i < series; i++)
                    {
                        container.Resolve(TestTypeBuilder.RootType);
                    }
                }
            }
        }

        private static readonly object LockObject = new object();
        private static volatile Service2 _service2;

        private static void CtorSingleton(long series, IPerformanceCounter performanceCounter)
        {
            lock (LockObject)
            {
                _service2 = null;
            }

            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    CreateSingletonService().DoSomething();
                }
            }
        }

        private static Service1 CreateSingletonService()
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

        private static void CtorTransient(long series, IPerformanceCounter performanceCounter)
        {
            using (performanceCounter.Run())
            {
                for (var i = 0; i < series; i++)
                {
                    CreateTransientService().DoSomething();
                }
            }
        }

        private static Service1 CreateTransientService() =>
            new Service1(new Service2(new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4())), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service3(new Service4(), new Service4(), new Service4(), new Service4(), new Service4()), new Service4());

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
            body.AppendLine($"{GetFramework()}");
            body.AppendLine("<style type='text/css'>TABLE {width: 600px; border-collapse: collapse;} TD, TH {padding: 3px; border: 1px solid black;} TH { background: #A0A0A0; } </style> ");
            body.AppendLine("<table border='1px' border-style:solid>");
            body.AppendLine("<tr>");
            body.AppendLine("<th>#</th>");
            body.AppendLine("<th>IoC</th>");
            body.AppendLine("<th>result</th>");
            body.AppendLine("</tr>");
            foreach (var line in results.OrderBy(i => i).Select((item, index) => $"<td>{index + 1:00}</td>{item}"))
            {
                body.AppendLine("<tr>");
                body.AppendLine(line.Replace("\n", "<br/>"));
                body.AppendLine("</tr>");
            }
            body.AppendLine("</table>");
            File.AppendAllText(GetReportFilePath(), body.ToString());
        }
    }
}
