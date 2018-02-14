#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class AsyncGet
    {
        [Fact]
        public async void Run()
        {
            // $visible=true
            // $group=01
            // $priority=02
            // $description=Asynchronous get
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<TaskScheduler>().To(ctx => TaskScheduler.Default))
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            {
                // Resolve the instance asynchronously
                var instance = await container.Get<Task<IService>>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
#endif