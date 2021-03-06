﻿// ReSharper disable IdentifierTypo
#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class AsynchronousConstructionCancellation
    {
        [Fact]
        // $visible=true
        // $tag=4 Async
        // $priority=03
        // $description=Cancellation of asynchronous construction
        // $header=It is possible to inject dependencies in asynchronous style and to cancel their creations using default _CancellationTokenSource_.
        // {
        public void Run()
        {
            // Create a cancellation token source
            var cancellationTokenSource = new CancellationTokenSource();

            using var container = Container.Create()
                // Bind cancellation token source
                .Bind<CancellationTokenSource>().To(ctx => cancellationTokenSource)
                // Bind the cancellation token
                .Bind<CancellationToken>().To(ctx => ctx.Container.Inject<CancellationTokenSource>().Token)
                // Bind some dependency
                .Bind<IDependency>().To<SomeDependency>()
                .Bind<Consumer>().To<Consumer>()
                .Container;

            // Resolve an instance asynchronously
            var instanceTask = container.Resolve<Task<Consumer>>();

            // Cancel tasks
            cancellationTokenSource.Cancel();

            // Get an instance
            instanceTask.Result.ShouldBeOfType<Consumer>();
        }

        public class SomeDependency: IDependency
        {
            // A time-consuming logic constructor with 
            public SomeDependency(CancellationToken cancellationToken)
            {
                while (!cancellationToken.IsCancellationRequested) { }
            }

            public int Index { get; set; }
        }

        public class Consumer
        {
            public Consumer(Task<IDependency> dependency1, Task<IDependency> dependency2)
            {
                // A time-consuming logic
                var dep1 = dependency1.Result;
                var dep2 = dependency2.Result;
            }
        }
        // }
    }
}
#endif