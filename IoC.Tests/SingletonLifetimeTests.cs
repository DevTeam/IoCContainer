namespace IoC.Tests
{
    using Lifetimes;
    using Xunit;

    public class SingletonLifetimeTests
    {
        [Fact]
        public void Should()
        {
            // Given
            var lifetime = new SingletonLifetime();
            //lifetime.Compile(() => 10,)

            // When

            // Then
        }
    }
}
