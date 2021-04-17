// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMethodReturnValue.Local
// ReSharper disable ObjectCreationAsStatement
namespace IoC.Benchmark
{
    using System;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Order;
    using Microsoft.Extensions.DependencyInjection;
    using Model;

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class AspDotnet: BenchmarkBase
    {
        public override TActualContainer CreateContainer<TActualContainer, TAbstractContainer>()
        {
            var abstractContainer = new TAbstractContainer();
            var services = new ServiceCollection();
            services.AddTransient<ICompositionRoot, AspCompositionRoot>();
            services.AddTransient<ISession, Session>();
            services.AddSingleton<IService1, Service1>();
            services.AddScoped<IService2, Service2>();
            services.AddTransient<IService3, Service3>();
            services.AddTransient<IDisposable, Disposable>();
            abstractContainer.Register(services);
            return abstractContainer.TryCreate();
        }

        private sealed class AspCompositionRoot: ICompositionRoot
        {
            private readonly IServiceScopeFactory _serviceScopeFactory;

            public AspCompositionRoot(IServiceScopeFactory serviceScopeFactory) => 
                _serviceScopeFactory = serviceScopeFactory;

            public void DoSomething()
            {
                using var scope = _serviceScopeFactory.CreateScope();
                scope.ServiceProvider.GetService<ISession>();
            }

            public bool Verify()
            {
                Session session1;
                Session session2;
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    session1 = (Session)scope.ServiceProvider.GetService<ISession>();
                    session2 = (Session)scope.ServiceProvider.GetService<ISession>();
                    if (session1 == null || session2 == null)
                    {
                        return false;
                    }

                    if (session1 == session2)
                    {
                        return false;
                    }

                    if (session1.Disposable == session2.Disposable)
                    {
                        return false;
                    }

                    if (session1.Service1 != session2.Service1)
                    {
                        return false;
                    }

                    if (session1.Service21 != session2.Service21)
                    {
                        return false;
                    }

                    if (session1.Service22 != session2.Service22)
                    {
                        return false;
                    }

                    if (session1.Service23 != session2.Service23)
                    {
                        return false;
                    }

                    if (session1.Service3 == session2.Service3)
                    {
                        return false;
                    }
                }

                if (!((Disposable)session1.Disposable).IsDisposed)
                {
                    return false;
                }

                return true;
            }
        }
    }
}