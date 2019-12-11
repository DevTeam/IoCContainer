namespace IoC.Tests
{
    using System;
    using Core;
    using Moq;
    using Shouldly;
    using Xunit;

    public class ScopeTest
    {
        [Fact]
        public void ScopeShouldSupportEqWhenNotEq()
        {
            // Given
            using var scope1 = new Scope(new LockObject(), 1);
            using var scope2 = new Scope(new LockObject(), 2);

            // When

            // Then
            scope1.Equals(scope2).ShouldBeFalse();
            scope1.GetHashCode().Equals(scope2.GetHashCode()).ShouldBeFalse();
        }

        [Fact]
        public void ScopeShouldSupportEqWhenEq()
        {
            // Given
            using var scope11 = new Scope(new LockObject(), 1);
            using var scope12 = new Scope(new LockObject(), 1);

            // When

            // Then
            scope11.Equals(scope12).ShouldBeTrue();
            scope11.GetHashCode().Equals(scope12.GetHashCode()).ShouldBeTrue();
        }

        [Fact]
        public void ActiveScopeShouldAccessibleViaCurrentProperty()
        {
            // Given
            using var scope1 = new Scope(new LockObject(), 1);
            using var scope2 = new Scope(new LockObject(), 2);

            // When
            using var token1 = scope1.Activate();
            using var token2 = scope2.Activate();

            // Then
            Scope.Current.ShouldBe(scope2);
            token2.Dispose();
            Scope.Current.ShouldBe(scope1);
            token1.Dispose();
        }

        [Fact]
        public void ShouldSupportMultiActivation()
        {
            // Given
            using var scope1 = new Scope(new LockObject(), 1);

            // When
            using var token1 = scope1.Activate();
            using var token2 = scope1.Activate();
            using var token3 = scope1.Activate();

            // Then
            Scope.Current.ShouldBe(scope1);
        }

        [Fact]
        public void ShouldManageResources()
        {
            // Given
            using var scope = new Scope(new LockObject(), 1);
            var resource1 = new Mock<IDisposable>();
            var resource2 = new Mock<IDisposable>();
            var resource3 = new Mock<IDisposable>();
            scope.RegisterResource(resource1.Object);
            scope.RegisterResource(resource2.Object);
            scope.RegisterResource(resource3.Object);

            scope.UnregisterResource(resource2.Object);

            // When
            scope.Dispose();

            // Then
            resource1.Verify(i => i.Dispose());
            resource2.Verify(i => i.Dispose(), Times.Never);
            resource3.Verify(i => i.Dispose());
        }
    }
}
