namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ResolveEnumerablesTests
    {
        [Fact]
        public void ContainerShouldResolveEnumerable()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                Func<IMyService1> func1 = Mock.Of<IMyService1>;
                // When
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Singleton).Tag(1).To(ctx => func()))
                using (container.Bind<IMyService1>().Tag("abc").To(ctx => func1()))
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Transient).Tag("xyz").To(ctx => func()))
                {
                    // Then
                    var actualInstances = container.Get<IEnumerable<IMyService1>>().ToList();
                    actualInstances.Count.ShouldBe(3);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveEnumerableAfterContainerChanged()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                Func<IMyService1> func1 = Mock.Of<IMyService1>;
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Singleton).Tag(1).To(ctx => func()))
                using (container.Bind<IMyService1>().Tag("abc").To(ctx => func1()))
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Transient).Tag("xyz").To(ctx => func()))
                {
                    var actualInstances = container.Get<IEnumerable<IMyService1>>().ToList();
                    actualInstances.Count.ShouldBe(3);
                    // When
                    using (container.Bind<IMyService1>().Tag(123).To(ctx => func1()))
                    {
                        // Then
                        var actualInstances2 = container.Get<IEnumerable<IMyService1>>().ToList();
                        actualInstances2.Count.ShouldBe(4);
                    }
                }
            }
        }
    }
}