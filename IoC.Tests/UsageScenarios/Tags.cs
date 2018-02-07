namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class Tags
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=02
            // $description=Tags
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().Tag(10).Tag().Tag("abc").To<Service>())
            {
                // Resolve instances using tags
                var instance1 = container.Tag("abc").Get<IService>();
                var instance2 = container.Tag(10).Get<IService>();

                // Resolve the instance using the empty tag
                var instance3 = container.Get<IService>();
            }
            // }
        }
    }
}
