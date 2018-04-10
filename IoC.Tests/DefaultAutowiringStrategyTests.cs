#if !NET40
namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using Core;
    using IntegrationTests;
    using Moq;
    using Shouldly;
    using Xunit;

    public class DefaultAutowiringStrategyTests
    {
        [Theory]
        [InlineData(typeof(string), typeof(string), typeof(string), true)]
        [InlineData(typeof(List<>), typeof(IEnumerable<string>), typeof(List<TT>), true)]
        [InlineData(typeof(AutowiringTests.MyGenericServiceWithConstraint<,>), typeof(AutowiringTests.IMyGenericServiceWithConstraint<IEnumerable<string>, int>), typeof(AutowiringTests.MyGenericServiceWithConstraint<TT, IEnumerable<string>>), true)]
        public void ShouldDefinedGenericType(Type type, Type targetType, Type expectedType, bool expectedResult)
        {
            // Given
            var autowiringStrategy = new DefaultAutowiringStrategy(Mock.Of<IContainer>());

            // When
            var actualResult = autowiringStrategy.TryResolveType(type, targetType, out var actualType);

            // Then
            actualResult.ShouldBe(expectedResult);
            if (actualResult)
            {
                actualType.ShouldBe(expectedType);
            }
        }
    }
}
#endif