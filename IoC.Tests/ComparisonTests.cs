// ReSharper disable HeuristicUnreachableCode
// ReSharper disable RedundantUsingDirective
#pragma warning disable 162
namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Ninject;
    using Unity;
    using Unity.Lifetime;
    using Xunit;

    public class ComparisonTests
    {
        private static readonly Dictionary<string, Func<int, long>> Iocs = new Dictionary<string, Func<int, long>>()
        {
            { "Castle.Windsor", CastleWindsor },
            { "Unity", Unity },
            { "Ninject", Ninject },
            { "This by Func", ThisByFunc },
            { "This", This },
            { "Autofac", Autofac },
        };

        [Fact]
        public void ComparisonTest()
        {
            const int warmupSeries = 2;
            const int series = 300000;
            const double error = .80;

            var results = new Dictionary<string, long>();
            foreach (var ioc in Iocs)
            {
                ioc.Value(warmupSeries);
                var elapsedMilliseconds = ioc.Value(series);
                Trace.WriteLine($"{ioc.Key}: {elapsedMilliseconds}");
                results.Add(ioc.Key, elapsedMilliseconds);
            }

            var resultsStr = string.Join(Environment.NewLine, results.Select(i => $"{i.Key}: {i.Value}").ToArray());
            var resultFileName = Path.Combine(GetBinDirectory(), "ComparisonTest.txt");
            File.WriteAllText(resultFileName, resultsStr);
            var actualElapsedMilliseconds = results["This"];
            foreach (var result in results.Where(i => !i.Key.StartsWith("This")))
            {
                Assert.True(actualElapsedMilliseconds * error <= result.Value, $"{result.Key} is better: {result.Value}, our result is : {actualElapsedMilliseconds}.\nResults:\n{resultsStr}");
            }
        }

        private static long This(int series)
        {
            using (var container = Container.Create())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().Lifetime(Lifetime.Singletone).To<Service2>())
            {
                var stopwatch = Stopwatch.StartNew();
                for (var i = 0; i < series; i++)
                {
                    container.Get<IService1>();
                }

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        private static long ThisByFunc(int series)
        {
            using (var container = Container.Create())
            using (container.Bind<IService1>().To<Service1>())
            using (container.Bind<IService2>().Lifetime(Lifetime.Singletone).To<Service2>())
            {
                var stopwatch = Stopwatch.StartNew();
                var func = container.Get<Func<IService1>>();
                for (var i = 0; i < series; i++)
                {
                    func();
                }

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        private static long Unity(int series)
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<IService1, Service1>();
                container.RegisterType<IService2, Service2>(new ContainerControlledLifetimeManager());
                var stopwatch = Stopwatch.StartNew();
                for (var i = 0; i < series; i++)
                {
                    container.Resolve(typeof(IService1));
                }

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        private static long Ninject(int series)
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind<IService1>().To<Service1>();
                kernel.Bind<IService2>().To<Service2>().InSingletonScope();
                var stopwatch = Stopwatch.StartNew();
                for (var i = 0; i < series; i++)
                {
                    kernel.Get<IService1>();
                }

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        private static long Autofac(int series)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Service1>().As<IService1>();
            builder.RegisterType<Service2>().As<IService2>().SingleInstance();
            using (var container = builder.Build())
            {
                var stopwatch = Stopwatch.StartNew();
                for (var i = 0; i < series; i++)
                {
                    container.Resolve<IService1>();
                }

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }

        private static long CastleWindsor(int series)
        {
            using (var container = new WindsorContainer())
            {
                container.Register(Component.For<IService1>().ImplementedBy<Service1>());
                container.Register(Component.For<IService2>().ImplementedBy<Service2>().LifestyleSingleton());
                var stopwatch = Stopwatch.StartNew();
                for (var i = 0; i < series; i++)
                {
                    container.Resolve<IService1>();
                }

                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
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
    public class Service1 : IService1
    {
        // ReSharper disable once UnusedParameter.Local
        public Service1([NotNull] IService2 service2)
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
}
