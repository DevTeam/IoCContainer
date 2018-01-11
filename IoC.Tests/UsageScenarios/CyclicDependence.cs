namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class CyclicDependence
    {
        [Fact]
        // $visible=true
        // $group=10
        // $priority=00
        // $description=Cyclic Dependence
        // {
        public void Run()
        {
            var expectedException = new InvalidOperationException("error");
            var issueResolver = new Mock<IIssueResolver>();
            issueResolver.Setup(i => i.CyclicDependenceDetected(It.IsAny<ResolvingContext>(), It.IsAny<Type>(), 32)).Throws(expectedException);

            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IIssueResolver>().To(ctx => issueResolver.Object))
            using (container.Bind<ILink>().To(typeof(Link), Has.Ref("link", 1)))
            using (container.Bind<ILink>().Tag(1).To(typeof(Link), Has.Ref("link", 2)))
            using (container.Bind<ILink>().Tag(2).To(typeof(Link), Has.Ref("link", 3)))
            using (container.Bind<ILink>().Tag(3).To(typeof(Link), Has.Ref("link", 1)))
            {
                try
                {
                    // Resolve the first link
                    container.Get<ILink>();
                }
                catch (InvalidOperationException actualException)
                {
                    actualException.ShouldBe(expectedException);
                }
            }

            issueResolver.Verify(i => i.CyclicDependenceDetected(It.IsAny<ResolvingContext>(), It.IsAny<Type>(), 32));
        }

        public interface ILink
        {
        }

        public class Link : ILink
        {
            // ReSharper disable once UnusedParameter.Local
            public Link(ILink link)
            {
            }
        }
        // }
    }
}