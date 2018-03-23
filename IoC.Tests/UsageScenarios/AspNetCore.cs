#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Features;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedVariable")]
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
            using (var container = Container.Create().Using(new AspNetCoreFeature()))
            using (container.Bind<IMyController>().As(Lifetime.ScopeSingleton).To(ctx => CreateController(mocks)))
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

        public interface IMyController: IDisposable
        {
        }

        // }
    }
}
#endif