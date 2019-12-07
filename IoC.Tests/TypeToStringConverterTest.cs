#if !NET40
namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Shouldly;
    using Xunit;

    public class TypeToStringConverterTest
    {
        [Theory]
        [InlineData(typeof(TypeToStringConverterTest), "TypeToStringConverterTest")]
        [InlineData(typeof(int), "int")]
        [InlineData(typeof(string), "string")]
        [InlineData(typeof(List<string>), "List<string>")]
        [InlineData(typeof(IList<string>), "IList<string>")]
        [InlineData(typeof(Dictionary<string, int>), "Dictionary<string, int>")]
        [InlineData(typeof(Dictionary<string, IList<TypeToStringConverterTest>>), "Dictionary<string, IList<TypeToStringConverterTest>>")]
        [InlineData(typeof(List<>), "List<T>")]
        [InlineData(typeof(Dictionary<,>), "Dictionary<TKey, TValue>")]
        public void ShouldConvertToTypeName(Type type, string expectedTypeName)
        {
            // Given
            var converter = TypeToStringConverter.Shared;

            // When
            var result = converter.TryConvert(type, type, out var actualTypeName);

            // Then
            result.ShouldBeTrue();
            actualTypeName.ShouldBe(expectedTypeName);
        }
    }
}
#endif
