namespace IoC.Tests
{
    using IntegrationTests;
    using Shouldly;
    using Xunit;

    public class BindTests
    {
        [Fact]
        public void ShouldDisposeToken()
        {
            // Given
            using var container = Container.Create();
            var token = container
                .Bind<IMyService>().Tag(1).To<MyService>(ctx => new MyService("", null))
                .Bind<IMyService>().Tag(2).To<MyService>(ctx => new MyService("", null));

            // When
            container.Resolve<IMyService[]>().Length.ShouldBe(2);
            token.Dispose();

            // Then
            container.Resolve<IMyService[]>().Length.ShouldBe(0);
        }
    }
}
