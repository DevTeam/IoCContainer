namespace IoC.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using Core.Configuration;
    using Shouldly;
    using Xunit;

    public class StatementToNamespacesConverterTests
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ShouldConvertStatementToAssemblies(string text, bool expectedResult, string[] expectedNamespaces)
        {
            // Given
            var converter = new StatementToNamespacesConverter();

            // When
            var actualResult = converter.TryConvert(BindingContext.Empty, new Statement(text, 1, 2), out var actualContext);

            // Then
            actualResult.ShouldBe(expectedResult);
            actualContext.Namespaces.ShouldBe(expectedNamespaces);
        }

        private class TestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] { "using IoC", true, new[] {"IoC"}},
                new object[] { "using IoC, IoC", true, new[] {"IoC"}},
                new object[] { "using IoC, IoC.Tests", true, new[] { "IoC", "IoC.Tests" } },
                new object[] { "using IoC   , IoC.Tests", true, new[] { "IoC", "IoC.Tests" } },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
