namespace IoC.Tests.IntegrationTests
{
    using System;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ContainerLifetimeTests
    {
        [Fact]
        public void ContainerShouldResolveWhenContainerLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService>().Lifetime(Lifetime.Container).To(ctx => func()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService>();

                    // Then
                    instance1.ShouldBe(instance2);
                    IMyService instance3;
                    using (var childContainer1 = container.CreateChild())
                    {
                        instance3 = childContainer1.Get<IMyService>();
                        var instance4 = childContainer1.Get<IMyService>();
                        instance3.ShouldBe(instance4);
                        instance1.ShouldNotBe(instance3);
                    }

                    using (var childContainer2 = container.CreateChild())
                    {
                        // Then
                        var instance5 = childContainer2.Get<IMyService>();
                        var instance6 = childContainer2.Get<IMyService>();
                        instance5.ShouldBe(instance6);
                        instance1.ShouldNotBe(instance5);
                        instance3.ShouldNotBe(instance5);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenContainerLifetimeAndSeveralContracts()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Container).To(ctx => func()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService1>();
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var instance3 = childContainer.Get<IMyService>();
                        var instance4 = childContainer.Get<IMyService1>();
                        instance1.ShouldBe(instance2);
                        instance1.ShouldNotBe(instance3);
                        instance3.ShouldBe(instance4);
                    }
                }
            }
        }
    }
}