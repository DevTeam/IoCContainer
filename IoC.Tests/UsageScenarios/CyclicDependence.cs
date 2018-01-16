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
            issueResolver.Setup(i => i.CyclicDependenceDetected(It.IsAny<Key>(), 128)).Throws(expectedException);

            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IIssueResolver>().ToValue(issueResolver.Object))
            using (container.Bind<ILink>().To<Link>(Has.Constructor(Has.Dependency<ILink>(1).For("link"))))
            using (container.Bind<ILink>().Tag(1).To<Link>(Has.Constructor(Has.Dependency<ILink>(2).For("link"))))
            using (container.Bind<ILink>().Tag(2).To<Link>(Has.Constructor(Has.Dependency<ILink>(3).For("link"))))
            using (container.Bind<ILink>().Tag(3).To<Link>(Has.Constructor(Has.Dependency<ILink>(1).For("link"))))
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

            issueResolver.Verify(i => i.CyclicDependenceDetected(Key.Create<ILink>(1), 128));
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