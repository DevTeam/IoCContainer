namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ThreadLocal
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=3 BCL types
            // $priority=01
            // $description=ThreadLocal
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                .Container;

            // Resolve the instance of ThreadLocal<IService>
            var threadLocal = container.Resolve<ThreadLocal<IService>>();

            // Get the instance via ThreadLocal
            var instance = threadLocal.Value;
            // }
            // Check the instance
            instance.ShouldBeOfType<Service>();
        }
    }
}
