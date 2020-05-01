#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class AsynchronousResolve
    {
        [Fact]
        public async void Run()
        {
            // $visible=true
            // $tag=async
            // $priority=02
            // $description=Asynchronous resolve
            // $header=Do you want to resolve instances asynchronously? It's simple ...
            // #footer=_TaskScheduler.Current_ is used by default to resolve instances, but it is easy to override it, binding abstract class _TaskScheduler_ to required implementation in a IoC container.
            // {
            // Create the container and configure it
            using var container = Container.Create()
                // Bind some dependency
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                // Override _TaskScheduler if it is required, TaskScheduler.Current will be used by default
                .Bind<TaskScheduler>().To(ctx => TaskScheduler.Default)
                .Container;

            // Resolve an instance asynchronously
            var instance = await container.Resolve<Task<IService>>();
            // }
            // Check the instance's type
            instance.ShouldBeOfType<Service>();
        }
    }
}
#endif