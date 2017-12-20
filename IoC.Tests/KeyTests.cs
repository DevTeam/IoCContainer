namespace IoC.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class KeyTests
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ShouldSupportGetHashCodeAndEquals(Key key1, Key key2, bool equals)
        {
            // Given

            // When
            var hashCode1 = key1.GetHashCode();
            var hashCode2 = key2.GetHashCode();
            var equals1 = Equals(key1, key2);
            var equals2 = Equals(key2, key1);

            // Then
            if (equals)
            {
                hashCode1.ShouldBe(hashCode2);
                equals1.ShouldBeTrue();
                equals2.ShouldBeTrue();
            }
            else
            {
                equals1.ShouldBeFalse();
                equals2.ShouldBeFalse();
            }
        }

        private class TestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {new Key(new Contract(typeof(string)), new Tag("abc")), new Key(new Contract(typeof(string)), new Tag("abc")), true},
                new object[] {new Key(new Contract(typeof(int)), new Tag("abc")), new Key(new Contract(typeof(string)), new Tag("abc")), false},
                new object[] {new Key(new Contract(typeof(string)), new Tag("abc")), new Key(new Contract(typeof(string)), new Tag("xyz")), false},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
