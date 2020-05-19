#if !NET40
namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using Dependencies;
    using IntegrationTests;
    using Shouldly;
    using Xunit;

    public class AutowiringDependencyTests
    {
        [Theory]
        [InlineData(typeof(List<>), typeof(IEnumerable<string>), typeof(List<TT>))]
        [InlineData(typeof(AutowiringTests.MyGenericServiceWithConstraint<,>), typeof(AutowiringTests.IMyGenericServiceWithConstraint<IEnumerable<string>, int>), typeof(AutowiringTests.MyGenericServiceWithConstraint<TT, IEnumerable<string>>))]
        public void ShouldGetInstanceTypeWithConstrains(Type type, Type targetType, Type expectedType)
        {
            // Given
            var dependency = new AutowiringDependency(type);

            // When
            var actualType = dependency.GetInstanceTypeBasedOnTargetGenericConstrains(targetType);
            
            // Then
            actualType.ShouldBe(expectedType);
        }
    }
}
#endif