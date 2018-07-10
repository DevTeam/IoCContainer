namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Validation
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=08
            // $description=Validation
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IService>().To<Service>())
            {
                // Try getting a resolver
                var canBeResolved = container.TryGetResolver<IService>(out _);

                canBeResolved.ShouldBeFalse();
            }
            // }
        }
    }
}
