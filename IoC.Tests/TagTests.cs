namespace IoC.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class TagTests
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ShouldSupportGetHashCodeAndEquals(Tag tag1, Tag tag2, bool equals)
        {
            // Given

            // When
            var hashCode1 = tag1.GetHashCode();
            var hashCode2 = tag2.GetHashCode();
            var equals1 = Equals(tag1, tag2);
            var equals2 = Equals(tag2, tag1);

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
                new object[] {new Tag(1), new Tag(1), true},
                new object[] {new Tag(1), new Tag(2), false},
                new object[] {new Tag(1), new Tag("abc"), false},
                new object[] {new Tag(null), new Tag(null), true},
                new object[] {new Tag(null), Tag.Default, true},
                new object[] {new Tag(null), new Tag(1), false},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
