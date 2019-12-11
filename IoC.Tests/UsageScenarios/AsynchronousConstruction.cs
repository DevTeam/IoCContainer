﻿// ReSharper disable IdentifierTypo
#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class AsynchronousConstruction
    {
        [Fact]
        // $visible=true
        // $tag=Asynchronous cases
        // $priority=03
        // $description=Asynchronous construction
        // {
        public async void Run()
        {
            // Create the container and configure it
            using var container = Container.Create()
                // Bind some dependency
                .Bind<IDependency>().To<SomeDependency>()
                .Bind<Consumer>().To<Consumer>().Container;

            // Resolve an instance asynchronously
            var instance = await container.Resolve<Task<Consumer>>();

            // Check the instance's type
            instance.ShouldBeOfType<Consumer>();
        }

        public class SomeDependency: IDependency
        {
            // Time-consuming logic constructor
            public SomeDependency() { }
        }

        public class Consumer
        {
            public Consumer(Task<IDependency> dependency1, Task<IDependency> dependency2)
            {
                // Time-consuming logic
                var dep1 = dependency1.Result;
                var dep2 = dependency2.Result;
            }
        }
        // }
    }
}
#endif