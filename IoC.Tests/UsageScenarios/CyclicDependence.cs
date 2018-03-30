// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Extensibility;
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
            issueResolver.Setup(i => i.CyclicDependenceDetected(It.IsAny<Key>(), 128)).Throws(expectedException);

            // Create a container
            using (var container = Container.Create())
            // Configure the container: 1,2,3 are tags to produce cyclic dependencies
            using (container.Bind<IIssueResolver>().To(ctx => issueResolver.Object))
            using (container.Bind<ILink>().To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1))))
            using (container.Bind<ILink>().Tag(1).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(2))))
            using (container.Bind<ILink>().Tag(2).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(3))))
            using (container.Bind<ILink>().Tag(3).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1))))
            {
                try
                {
                    // Resolve the first link
                    container.Resolve<ILink>();
                }
                catch (InvalidOperationException actualException)
                {
                    actualException.ShouldBe(expectedException);
                }
            }

            issueResolver.Verify(i => i.CyclicDependenceDetected(It.IsAny<Key>(), 128));
        }

        public interface ILink
        {
        }

        public class Link : ILink
        {
            public Link(ILink link) { }
        }
        // }
    }
}