// ReSharper disable UnusedParameter.Local
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
        // $tag=samples
        // $priority=00
        // $description=Cyclic Dependence
        // {
        public void Run()
        {
            var expectedException = new InvalidOperationException("error");
            var issueResolver = new Mock<IIssueResolver>();
            // Throes the exception for reentrancy 128
            issueResolver.Setup(i => i.CyclicDependenceDetected(It.IsAny<Key>(), 128)).Throws(expectedException);

            // Create the container
            using (var container = Container.Create())
            // Configure the own issue resolver to check cyclic dependencies detection
            using (container.Bind<IIssueResolver>().To(ctx => issueResolver.Object))
            // Configure the container, where 1,2,3 are tags to produce cyclic dependencies during a resolving
            using (container.Bind<ILink>().To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1))))
            using (container.Bind<ILink>().Tag(1).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(2))))
            using (container.Bind<ILink>().Tag(2).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(3))))
            using (container.Bind<ILink>().Tag(3).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1))))
            {
                try
                {
                    // Resolve the root instance
                    container.Resolve<ILink>();
                }
                // Catch the exception about cyclic dependencies at a depth of 128
                catch (InvalidOperationException actualException)
                {
                    // Check the exception
                    actualException.ShouldBe(expectedException);
                }
            }
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