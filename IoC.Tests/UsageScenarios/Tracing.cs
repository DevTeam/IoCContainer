namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class Tracing
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=08
            // $description=Tracing
            // {
            // Create and configure the container
            var messages = new List<string>();
            {
                using var rootContainer = Container
                    .Create("root")
                    .Trace(message => messages.Add(message))
                    .Container;

                using var parentContainer = rootContainer
                    .Create("parent")
                    .Bind<IDependency>().To<Dependency>(ctx => new Dependency())
                    .Container;

                using var childContainer = parentContainer
                    .Create("child")
                    .Bind<IService<TT>>().To<Service<TT>>()
                    .Container;

                childContainer.Resolve<IService<int>>();
            }

            messages.Count.ShouldBe(8);
            // }
        }
    }
}
