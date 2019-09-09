namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public class TransientLifetimeTests
    {
        [Fact]
        public void ContainerShouldResolveWhenTransientLifetime()
        {
            // Given
            using var container = Container.Create();
            Func<IMyService> func = Mock.Of<IMyService>;
            // When
            using (container.Bind<IMyService>().To(ctx => func()))
            {
                // Then
                var instance1 = container.Resolve<IMyService>();
                instance1.ShouldNotBeNull();
                var instance2 = container.Resolve<IMyService>();
                instance2.ShouldNotBeNull();
                using var childContainer = container.Create();
                var instance3 = childContainer.Resolve<IMyService>();
                instance3.ShouldNotBeNull();
                instance1.ShouldNotBe(instance2);
                instance1.ShouldNotBe(instance3);
            }
        }

        [Fact]
        public void ContainerShouldResolveDependencyFromRegistrationContainerWhenTransientLifetime()
        {
            // Given
            var myService1 = new Mock<IMyService1>();
            var myService12 = new Mock<IMyService1>();
            using var container = Container.Create();
            // When
            using (container.Bind<IMyService>().To<MyService>())
            using (container.Bind<IMyService1>().To(ctx => myService1.Object))
            using (container.Bind<string>().To(ctx => "abc"))
            {
                // Then
                using var childContainer = container.Create();
                using (childContainer.Bind<IMyService1>().To(ctx => myService12.Object))
                using (childContainer.Bind<string>().To(ctx => "xyz"))
                {
                    // Then
                    var instance = childContainer.Resolve<IMyService>();
                    instance.ShouldNotBeNull();
                    instance.Name.ShouldBe("xyz");
                    instance.SomeRef.ShouldBe(myService12.Object);
                }
            }
        }
    }
}