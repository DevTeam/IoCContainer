#if !NET45
namespace IoC.Tests.Cases
{
    using System;
    using System.Reflection;
    using Moq;
    using Shouldly;
    using Xunit;

    public class CyclicDependenceCase
    {
        [Fact]
        public void Run()
        {
            var expectedException = new InvalidOperationException("error");
            var issueResolver = new Mock<IIssueResolver>();
            issueResolver.Setup(i => i.CyclicDependenceDetected(It.IsAny<Context>(), It.IsAny<TypeInfo>(), 32)).Throws(expectedException);

            using (var container = Container.Create("base"))
            using (container.Map<IIssueResolver>().To(ctx => issueResolver.Object))
            using (container.Map<ILink>().To(typeof(Link), Has.Ref("link", 1)))
            using (container.Map<ILink>().Tag(1).To(typeof(Link), Has.Ref("link", 2)))
            using (container.Map<ILink>().Tag(2).To(typeof(Link), Has.Ref("link", 3)))
            using (container.Map<ILink>().Tag(3).To(typeof(Link), Has.Ref("link", 4)))
            using (container.Map<ILink>().Tag(4).To(typeof(Link), Has.Ref("link", 5)))
            using (container.Map<ILink>().Tag(5).To(typeof(Link), Has.Ref("link", 1)))
            {
                try
                {
                    container.Get<ILink>();
                }
                catch (InvalidOperationException actualException)
                {
                    actualException.ShouldBe(expectedException);
                }
            }

            issueResolver.Verify(i => i.CyclicDependenceDetected(It.IsAny<Context>(), It.IsAny<TypeInfo>(), 32));
        }
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
}
#endif