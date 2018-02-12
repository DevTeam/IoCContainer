#if !NET40
namespace IoC.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Core.Configuration;
    using Shouldly;
    using Xunit;

    public class StringToTagsConverterTest
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ShouldConvertStringToTags(string text, bool expectedResult, object[] expectedTags)
        {
            // Given
            var converter = new StringToTagsConverter();

            // When
            var actualResult = converter.TryConvert(new Statement("statement", 1, 2), text, out var actualTags);

            // Then
            actualResult.ShouldBe(expectedResult);
            actualTags.ShouldBe(expectedTags);
        }

        private class TestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {".Tag(\"abc\")", true, new object[] {"abc"}},
                new object[] {".Tag()", true, new object[] {null}},
                new object[] {".Tag(\"abc\").Tag()", true, new object[] {"abc", null}},
                new object[] { ".Tag(\"abc\").Tag(\"abc\")", true, new object[] {"abc"}},
                new object[] {"  .  Tag (  \"abc\"  ) ", true, new object[] {"abc"}},
                new object[] { ".Tag(\"abc\").Tag(\"xyz\")", true, new object[] {"abc", "xyz"}},
                new object[] { ".Tag(\"abc\").Lifetime(Transient).Tag(\"xyz\")", true, new object[] {"abc", "xyz"}},
                new object[] {".Tag(10)", true, new object[] {10}},
                new object[] {".Tag(100000000)", true, new object[] { 100000000 } },
                new object[] {".Tag('a')", true, new object[] {'a'}},
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
#endif
