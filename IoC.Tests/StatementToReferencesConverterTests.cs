#if !NET40
namespace IoC.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using Core.Configuration;
    using Core;
    using Shouldly;
    using Xunit;

    public class StatementToReferencesConverterTests
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ShouldConvertStatementToAssemblies(string text, bool expectedResult, Assembly[] expectedReferences)
        {
            // Given
            var converter = new StatementToReferencesConverter();

            // When
            var actualResult = converter.TryConvert(BindingContext.Empty, new Statement(text, 1, 2), out var actualContext);

            // Then
            actualResult.ShouldBe(expectedResult);
            actualContext.Assemblies.ShouldBe(expectedReferences);
        }

        private class TestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] {"ref IoC", true, new[] {typeof(IContainer).Descriptor().GetAssembly()}},
                new object[] {"ref IoC, IoC", true, new[] {typeof(IContainer).Descriptor().GetAssembly()}},
                new object[] {"ref IoC, IoC.Tests", true, new[] {typeof(IContainer).Descriptor().GetAssembly(), typeof(TestDataGenerator).Descriptor().GetAssembly() } },
                new object[] {"ref IoC  ,IoC.Tests", true, new[] {typeof(IContainer).Descriptor().GetAssembly(), typeof(TestDataGenerator).Descriptor().GetAssembly() } },
            };

            public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
#endif