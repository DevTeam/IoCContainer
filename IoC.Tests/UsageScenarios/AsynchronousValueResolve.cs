#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NET40 && !NET45 && !NET46 && !NET47 && !NET48
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class AsynchronousValueResolve
    {
        [Fact]
        public async void Run()
        {
            // $visible=true
            // $tag=multithreading
            // $priority=02
            // $description=Asynchronous lightweight resolve
            // {
            // Create a container
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind Service
                .Bind<IService>().To<Service>()
                .Container;

            // Resolve an instance asynchronously via ValueTask
            var instance = await container.Resolve<ValueTask<IService>>();
            // }
            // Check the instance's type
            instance.ShouldBeOfType<Service>();
        }
    }
}
#endif