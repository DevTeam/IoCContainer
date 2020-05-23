// ReSharper disable IdentifierTypo
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
        // $tag=advanced
        // $priority=10
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
                // Bind cancellation token
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
            // Time-consuming logic constructor with cancellation token
            public SomeDependency(CancellationToken cancellationToken)
            {
                while (!cancellationToken.IsCancellationRequested) { }
            }
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