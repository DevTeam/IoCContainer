namespace IoC.Tests
{
    using Shouldly;
    using Xunit;

    public class KeyTest
    {
        [Fact]
        public void KeyHashCodeShouldBeEqualWithTypeHashCodeWhenKeyHasNoTag()
        {
            // Given
            var type = typeof(KeyTest);

            // When
            var key = new Key(type);

            // Then
            key.GetHashCode().ShouldBe(type.GetHashCode());
        }
    }
}
