#if NETCOREAPP2_0
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class AspNetCore
    {
        [Fact]
        // $visible=true
        // $group=08
        // $priority=00
        // $description=ASP Net Core Support
        // {
        public void Run()
        {
            var mocks = new List<Mock<IMyController>>();
            // Create a container
            using (var container = Container.Create().Using<AspNetCoreFeature>())
            using (container.Bind<IMyController>().To(ctx => CreateController(mocks)))
            {
                var factory = container.Resolve<IServiceScopeFactory>();
                using (var scope = factory.CreateScope())
                {
                    var controller1 = (IMyController)scope.ServiceProvider.GetService(typeof(IMyController));
                }

                using (var scope = factory.CreateScope())
                {
                    var controller2 = (IMyController)scope.ServiceProvider.GetService(typeof(IMyController));
                }

                mocks.Count.ShouldBe(2);
                foreach (var mock in mocks)
                {
                    mock.Verify(i => i.Dispose(), Times.Once);
                }
            }
        }

        public IMyController CreateController(ICollection<Mock<IMyController>> mocks)
        {
            var newMock = new Mock<IMyController>();
            mocks.Add(newMock);
            return newMock.Object;
        }

        public class AspNetCoreFeature: IConfiguration
        {
            public IEnumerable<IDisposable> Apply(IContainer container)
            {
                yield return container.Bind<IServiceScopeFactory>().As(Lifetime.Singleton).To<ServiceScopeFactory>();
                yield return container.Bind<IServiceScope>().To<ServiceScope>(ctx => new ServiceScope(ctx.Container));
            }
        }

        public sealed class ServiceScopeFactory: IServiceScopeFactory
        {
            private readonly Func<IServiceScope> _serviceScopeFactory;

            public ServiceScopeFactory(Func<IServiceScope> serviceScopeFactory) => _serviceScopeFactory = serviceScopeFactory;

            public IServiceScope CreateScope() => _serviceScopeFactory();
        }

        public sealed class ServiceScope: IServiceScope, IServiceProvider
        {
            private readonly IContainer _container;
            private readonly HashSet<IDisposable> _disposables = new HashSet<IDisposable>();

            public ServiceScope(IContainer container)
            {
                _container = container;
            }

            public IServiceProvider ServiceProvider => this;

            public object GetService(Type serviceType)
            {
                var instance = _container.Resolve<object>(serviceType);
                if (instance is IDisposable disposable)
                {
                    lock (_disposables)
                    {
                        _disposables.Add(disposable);
                    }
                }

                return instance;
            }

            public void Dispose()
            {
                foreach (var disposable in _disposables)
                {
                    disposable.Dispose();
                }

                _disposables.Clear();
            }
        }

        public interface IMyController: IDisposable
        {
        }

        // }
    }
}
#endif