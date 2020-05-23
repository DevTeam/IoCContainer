namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ConstantDependency
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=basic
            // $priority=01
            // $description=Constant dependency
            // $header=It's obvious here.
            // {
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
