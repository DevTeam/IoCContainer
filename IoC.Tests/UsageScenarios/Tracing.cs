﻿namespace IoC.Tests.UsageScenarios
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
            // $tag=5 Advanced
            // $priority=08
            // $description=Tracing
            // $header=Tracing allows to explore most aspects of container behavior: creating and removing child containers, adding and removing bindings, compiling instance factories.
            // {
            var traceMessages = new List<string>();

            // This block is just to mark the scope for "using" statements
            {
                // Create and configure a root container
                using var rootContainer = Container
                    .Create("root")
                    // Aggregate trace messages to the list 'traceMessages'
                    .Trace(e => traceMessages.Add(e.Message))
                    .Container;

                // Create and configure a parent container
                using var parentContainer = rootContainer
                    .Create("parent")
                    .Bind<IDependency>().To<Dependency>(ctx => new Dependency())
                    .Container;

                // Create and configure a child container
                using var childContainer = parentContainer
                    .Create("child")
                    .Bind<IService<TT>>().To<Service<TT>>()
                    .Container;

                childContainer.Resolve<IService<int>>();
            } // All containers were disposed of here

            traceMessages.Count.ShouldBe(8);
            // }
        }
    }
}
