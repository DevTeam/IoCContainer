namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Constant
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=01
            // $description=Constant
            // $header=It's obvious here.
            // {
            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<int>().To(ctx => 10)
                .Container;
            // Resolve an integer
            var val = container.Resolve<int>();
            // Check the value
            val.ShouldBe(10);
            // }
        }
    }
}
