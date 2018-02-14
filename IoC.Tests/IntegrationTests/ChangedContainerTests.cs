namespace IoC.Tests.IntegrationTests
{
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public class ChangedContainerTests
    {
        [Fact]
        public void ContainerShouldResolveWithReferencesAfterChangeBindings()
        {
            // Given
            using (var container = Container.Create())
            {
                var firstRef = Mock.Of<IMyService>();
                var expectedRef = Mock.Of<IMyService>();

                // When
                using (container.Bind<IMyService>().As(Lifetime.Transient).To(
                    ctx => new MyService((string)ctx.Args[0], ctx.Container.Inject<IMyService1>())))
                {
                    using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => firstRef))
                    {
                        var actualInstance = container.Get<IMyService>("xyz");
                        ((MyService) actualInstance).Name.ShouldBe("xyz");
                        ((MyService) actualInstance).SomeRef.ShouldBe(firstRef);
                    }

                    using (container.Bind<IMyService1>().As(Lifetime.Transient).To(ctx => expectedRef))
                    {
                        // Then
                        var actualInstance = container.Get<IMyService>("abc");
                        ((MyService)actualInstance).Name.ShouldBe("abc");
                        ((MyService)actualInstance).SomeRef.ShouldBe(expectedRef);
                    }
                }
            }
        }
    }
}
