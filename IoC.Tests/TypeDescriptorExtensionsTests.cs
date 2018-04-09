#if !NET40
namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using Core;
    using IntegrationTests;
    using Shouldly;
    using Xunit;

    public class TypeDescriptorExtensionsTests
    {
        [Theory]
        [InlineData(typeof(string), typeof(string), typeof(string))]
        [InlineData(typeof(List<>), typeof(IEnumerable<string>), typeof(List<TT>))]
        [InlineData(typeof(AutowiringTests.MyGenericServiceWithConstraint<,>), typeof(AutowiringTests.IMyGenericServiceWithConstraint<IEnumerable<string>, int>), typeof(AutowiringTests.MyGenericServiceWithConstraint<TT, IEnumerable<string>>))]
        public void ShouldDefinedGenericType(Type type, Type targetType, Type expectedType)
        {
            // Given

            // When
            var actualType = type.Descriptor().ToDefinedGenericType(targetType.Descriptor());

            // Then
            actualType.ShouldBe(expectedType);
        }
    }
}
#endif