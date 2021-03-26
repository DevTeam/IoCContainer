/*
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
            var scopeManager = new ScopeManager(new LockObject());
            using var scope1 = new Scope(scopeManager, new LockObject());
            using var scope2 = new Scope(scopeManager, new LockObject());

            // When

            // Then
            scope1.Equals(scope2).ShouldBeFalse();
            scope1.GetHashCode().Equals(scope2.GetHashCode()).ShouldBeFalse();
        }

        [Fact]
        public void ActiveScopeShouldAccessibleViaCurrentProperty()
        {
            // Given
            var scopeManager = new ScopeManager(new LockObject());
            using var scope1 = new Scope(scopeManager, new LockObject());
            using var scope2 = new Scope(scopeManager, new LockObject());

            // When
            using var token1 = scope1.Activate();
            using var token2 = scope2.Activate();

            // Then
            scopeManager.Current.ShouldBe(scope2);
            token2.Dispose();
            scopeManager.Current.ShouldBe(scope1);
            token1.Dispose();
        }

        [Fact]
        public void ShouldSupportMultiActivation()
        {
            // Given
            var scopeManager = new ScopeManager(new LockObject());
            using var scope1 = new Scope(scopeManager, new LockObject());

            // When
            using var token1 = scope1.Activate();
            using var token2 = scope1.Activate();
            using var token3 = scope1.Activate();

            // Then
            scopeManager.Current.ShouldBe(scope1);
        }

        [Fact]
        public void ShouldManageResources()
        {
            // Given
            var scopeManager = new ScopeManager(new LockObject());
            using var scope = new Scope(scopeManager, new LockObject());
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
*/