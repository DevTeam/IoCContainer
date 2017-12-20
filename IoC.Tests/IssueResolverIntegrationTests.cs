namespace IoC.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public class IssueResolverIntegrationTests
    {
        [Fact]
        public void ContainerShouldCannotResolveWhenHasNoRegistration()
        {
            // Given
            using (var container = Container.Create())
            {
                var issueResolvere = new Mock<IIssueResolver>();
                var key = new Key(new Contract(typeof(IMyService)), Tag.Default);
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
                issueResolvere.Setup(i => i.CannotFindConsructor(typeof(MyClassWithoutCtor).GetTypeInfo())).Throws<InvalidOperationException>();

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
                issueResolvere.Verify(i => i.CannotFindConsructor(typeof(MyClassWithoutCtor).GetTypeInfo()), Times.Once);
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
