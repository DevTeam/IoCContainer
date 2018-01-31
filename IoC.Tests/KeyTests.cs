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
                new object[] {new Key(typeof(string)), new Key(typeof(string), Key.AnyTag), true},
                new object[] {new Key(typeof(string)), new Key(typeof(string), 10), false},
                new object[] {new Key(typeof(string), "abc"), new Key(typeof(string), Key.AnyTag), true},
                new object[] {new Key(typeof(int), "abc"), new Key(typeof(string), Key.AnyTag), false},
                new object[] {new Key(typeof(string), "abc"), new Key(typeof(string), "abc"), true},
                new object[] {new Key(typeof(int), "abc"), new Key(typeof(string), "abc"), false},
                new object[] {new Key(typeof(string), "abc"), new Key(typeof(string), "xyz"), false},
                new object[] {new Key(typeof(IEnumerable<string>), "abc"), new Key(typeof(IEnumerable<int>), Key.AnyTag), false},
                new object[] {new Key(typeof(IEnumerable<string>), Key.AnyTag), new Key(typeof(IEnumerable<int>), Key.AnyTag), false},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
