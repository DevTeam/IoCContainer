namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ChildContainer
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=03
            // $description=Child Container
            // {
            // Create a parent container
            using (var parentContainer = Container.Create())
            // Create a child container
            using (var childContainer = parentContainer.CreateChild())
            {
                childContainer.Parent.ShouldBe(parentContainer);
            }
            // }
        }
    }
}
