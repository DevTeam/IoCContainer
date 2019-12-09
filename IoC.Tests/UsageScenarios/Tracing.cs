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
            var traceMessages = new List<string>();

            {
                // Create and configure the root container
                using var rootContainer = Container
                    .Create("root")
                    // Aggregate trace messages to the list 'traceMessages'
                    .Trace(message => traceMessages.Add(message))
                    .Container;

                // Create and configure the parent container
                using var parentContainer = rootContainer
                    .Create("parent")
                    .Bind<IDependency>().To<Dependency>(ctx => new Dependency())
                    .Container;

                // Create and configure the child container
                using var childContainer = parentContainer
                    .Create("child")
                    .Bind<IService<TT>>().To<Service<TT>>()
                    .Container;

                childContainer.Resolve<IService<int>>();
            }

            traceMessages.Count.ShouldBe(8);
            // }
        }
    }
}
