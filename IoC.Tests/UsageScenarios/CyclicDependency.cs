// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Issues;
    using Moq;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class CyclicDependency
    {
        [Fact]
        // $visible=true
        // $tag=9 Samples
        // $priority=00
        // $description=Cyclic dependency
        // $header=By default, a circular dependency is detected after the 256th recursive resolution. This behaviour may be changed by overriding the interface _IFoundCyclicDependency_.
        // {
        public void Run()
        {
            var expectedException = new InvalidOperationException("error");
            var foundCyclicDependency = new Mock<IFoundCyclicDependency>();
            // Throws the exception for reentrancy 128
            foundCyclicDependency.Setup(i => i.Resolve(It.Is<IBuildContext>(ctx => ctx.Depth == 128))).Throws(expectedException);

            using var container = Container
                .Create()
                .Bind<IFoundCyclicDependency>().To(ctx => foundCyclicDependency.Object)
                // Configure the container, where 1,2,3 are tags to produce cyclic dependencies during a resolving
                .Bind<ILink>().To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1)))
                .Bind<ILink>().Tag(1).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(2)))
                .Bind<ILink>().Tag(2).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(3)))
                .Bind<ILink>().Tag(3).To<Link>(ctx => new Link(ctx.Container.Inject<ILink>(1)))
                .Container;

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

        public interface ILink { }

        public class Link : ILink
        {
            public Link(ILink link) { }
        }
        // }
    }
}