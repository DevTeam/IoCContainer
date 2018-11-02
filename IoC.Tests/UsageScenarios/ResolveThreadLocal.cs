namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ResolveThreadLocal
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=injection
            // $priority=02
            // $description=Resolve ThreadLocal
            // {
            // Create and configure the container
            using (var container = Container.Create())
            // Bind some dependency
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            {
                // Resolve the instance of Lazy<IService>
                var lazy = container.Resolve<ThreadLocal<IService>>();

                // Get the instance via ThreadLocal
                var instance = lazy.Value;
                // }
                // Check the instance's type
                instance.ShouldBeOfType<Service>();
                // {
            }
            // }
        }
    }
}
