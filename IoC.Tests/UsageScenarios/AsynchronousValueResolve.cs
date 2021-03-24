#if !NET40 && !NET403 && !NET45 && !NET45 && !NET451 && !NET452 && !NET46 && !NET461 && !NET462 && !NET48 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !WINDOWS_UWP
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
            // $tag=4 Async
            // $priority=01
            // $description=ValueTask
            // $header=In this scenario, ValueTask is just a container for a resolved instance.
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind Service
                .Bind<IService>().To<Service>()
                .Container;

            // Resolve an instance asynchronously via ValueTask
            var instance = await container.Resolve<ValueTask<IService>>();
            // }
            // Check the instance
            instance.ShouldBeOfType<Service>();
        }
    }
}
#endif