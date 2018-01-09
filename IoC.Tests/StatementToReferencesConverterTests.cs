namespace IoC.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using Internal.Configuration;
    using Shouldly;
    using Xunit;

    public class StatementToReferencesConverterTests
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ShouldConvertStatementToAssemblies(string text, bool expectedResult, Assembly[] expectedRefernces)
        {
            // Given
            var converter = new StatementToReferencesConverter();

            // When
            var actualResult = converter.TryConvert(BindingContext.Empty, new Statement(text, 1, 2), out var actualContext);

            // Then
            actualResult.ShouldBe(expectedResult);
            actualContext.Assemblies.ShouldBe(expectedRefernces);
        }

        private class TestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {"ref IoC", true, new[] {typeof(IContainer).Assembly}},
                new object[] {"ref IoC, IoC", true, new[] {typeof(IContainer).Assembly}},
                new object[] {"ref IoC, IoC.Tests", true, new[] {typeof(IContainer).Assembly, typeof(TestDataGenerator).Assembly } },
                new object[] {"ref IoC  ,IoC.Tests", true, new[] {typeof(IContainer).Assembly, typeof(TestDataGenerator).Assembly } },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
