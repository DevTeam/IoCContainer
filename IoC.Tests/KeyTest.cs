namespace IoC.Tests
{
    // ReSharper disable once RedundantUsingDirective
    using System.Collections;
    // ReSharper disable once RedundantUsingDirective
    using System.Collections.Generic;
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

#if !NET40
        [Theory]
        [ClassData(typeof(TestData))]
        public void KeyShouldSupportEqualsAndGetHashCode(Key key1, Key key2, bool expectedIsEqual)
        {
            // Given

            // When
            var hashCode1 = key1.GetHashCode();
            var hashCode2 = key1.GetHashCode();
            var actualIsEqual = key1.Equals(key2);
            
            // Then
            actualIsEqual.ShouldBe(expectedIsEqual);
            if (expectedIsEqual)
            {
                hashCode1.ShouldBe(hashCode2);
            }
        }

        private class TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Key(typeof(string)), new Key(typeof(string)), true };
                yield return new object[] { new Key(typeof(string), 10), new Key(typeof(string), 10), true };
                yield return new object[] { new Key(typeof(string), "abc"), new Key(typeof(string), "abc"), true };
                yield return new object[] { new Key(typeof(string)), new Key(typeof(int)), false };
                yield return new object[] { new Key(typeof(int), "abc"), new Key(typeof(string), "abc"), false };
                yield return new object[] { new Key(typeof(string), "abc"), new Key(typeof(string), "xyz"), false };
                yield return new object[] { new Key(typeof(string), "abc"), new Key(typeof(string)), false };
                yield return new object[] { new Key(typeof(string), "abc"), new Key(typeof(object), "abc"), false };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
#endif
    }
}