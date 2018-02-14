namespace IoC.Tests.IntegrationTests
{
    using System;
    using Moq;
    using Shouldly;
    using Xunit;

    public class SingletonTests
    {
        [Fact]
        public void ContainerShouldResolveWhenSingletonLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService>().As(Lifetime.Singleton).To(ctx => func()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService>();
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var instance3 = childContainer.Get<IMyService>();
                        instance1.ShouldNotBeNull();
                        instance1.ShouldBe(instance2);
                        instance1.ShouldBe(instance3);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenSingletonLifetimeAndSeveralContracts()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService, IMyService1>().As(Lifetime.Singleton).To(ctx => func()))
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
                        instance1.ShouldBe(instance3);
                        instance1.ShouldBe(instance4);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldDiposeWhenSingletonLifetime()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                // When
                using (container.Bind<IMyService>().As(Lifetime.Singleton).To(ctx => func()))
                {
                    // Then
                    var instance1 = container.Get<IMyService>();
                    var instance2 = container.Get<IMyService>();
                    using (var childContainer = container.CreateChild())
                    {
                        // Then
                        var instance3 = childContainer.Get<IMyService>();
                        instance1.ShouldBe(instance2);
                        instance1.ShouldBe(instance3);
                    }
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenSingletonLifetimeWithGenerics()
        {
            // Given
            using (var container = Container.Create())
            {
                // When
                using (container.Bind<IMyGenericService<TT1, TT2>>().As(Lifetime.Singleton).To(ctx => new MyGenericService<TT1, TT2>()))
                {
                    // Then
                    var instance1 = container.Get<IMyGenericService<int, double>>();
                    var instance2 = container.Get<IMyGenericService<string, object>>();
                    var instance3 = container.Get<IMyGenericService<int, double>>();
                    var instance4 = container.Get<IMyGenericService<string, object>>();
                    instance1.ShouldBe(instance3);
                    instance2.ShouldBe(instance4);
                }
            }
        }
    }
}