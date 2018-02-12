namespace IoC.Tests.IntegrationTests
{
    using System;
    using Moq;
    using Shouldly;
    using Xunit;

    public class InjectionsTests
    {
        [Fact]
        public void ContainerShouldResolveWhenHasInitializerMethod()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService1>();
                Func<IMyService1> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().Lifetime(Lifetime.Transient).To(ctx => func()))
                using (container.Bind<IMyService>().Lifetime(Lifetime.Transient).To(
                    ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>()),
                    ctx => ctx.It.Init(ctx.Container.Inject<IMyService1>(), ctx.Container.Inject<IMyService1>(), (string) ctx.Args[1])))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>("abc", "xyz");

                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService) actualInstance).Name.ShouldBe("xyz");
                    ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenHasInitializerSetter()
        {
            // Given
            using (var container = Container.Create())
            {
                var expectedRef = Mock.Of<IMyService1>();
                Func<IMyService1> func = () => expectedRef;

                // When
                using (container.Bind<IMyService1>().Lifetime(Lifetime.Transient).To(ctx => func()))
                using (container.Bind<IMyService>().Lifetime(Lifetime.Transient).To(
                    ctx => new MyService((string) ctx.Args[0], ctx.Container.Inject<IMyService1>()),
                    ctx => ctx.Container.Inject(ctx.It.Name, (string) ctx.Args[1])))
                {
                    // Then
                    var actualInstance = container.Get<IMyService>("abc", "xyz");

                    actualInstance.ShouldBeOfType<MyService>();
                    ((MyService) actualInstance).Name.ShouldBe("xyz");
                    ((MyService) actualInstance).SomeRef.ShouldBe(expectedRef);
                }
            }
        }
    }
}