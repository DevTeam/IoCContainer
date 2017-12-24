#if !NET45
namespace IoC.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    // ReSharper disable once RedundantUsingDirective
    using System.Reflection;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class IssueResolverIntegrationTests
    {
        [Fact]
        public void ContainerShouldCannotResolveWhenHasNoRegistration()
        {
            // Given
            using (var container = Container.Create())
            {
                var issueResolvere = new Mock<IIssueResolver>();
                var key = new Key(typeof(IMyService));
                issueResolvere.Setup(i => i.CannotResolve(container, key)).Throws<InvalidOperationException>();

                // When
                using (container.Map<IIssueResolver>().Lifetime(Lifetime.Singletone).To(ctx => issueResolvere.Object))
                {
                    try
                    {
                        container.Get<IMyService>();
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    // Then
                    issueResolvere.Verify(i => i.CannotResolve(container, key), Times.Once);
                }
            }
        }

        [Fact]
        public void ContainerShouldCannotFindConsructorWhenHasNoApropriateConstructor()
        {
            // Given
            using (var container = Container.Create())
            {
                var issueResolvere = new Mock<IIssueResolver>();
                issueResolvere.Setup(i => i.CannotFindConsructor(It.IsAny<ITypeInfo>())).Throws<InvalidOperationException>();

                // When
                using (container.Map<IIssueResolver>().Lifetime(Lifetime.Singletone).To(ctx => issueResolvere.Object))

                try
                {
                    using (container.Autowiring<MyClassWithoutCtor, IMyService>())
                    {
                    }
                }
                catch (InvalidOperationException)
                {
                }

                // Then
                issueResolvere.Verify(i => i.CannotFindConsructor(It.IsAny<ITypeInfo>()), Times.Once);
            }
        }

        public interface IMyService
        {
        }

        public class MyClassWithoutCtor: IMyService
        {
            private MyClassWithoutCtor()
            {
            }
        }
    }
}
#endif