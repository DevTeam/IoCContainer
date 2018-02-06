namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Expressions
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=02
            // $description=Expressions
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().ToExpression<Service>((curContainer, args) => new Service(new Dependency(), (string)args[0])))
            {
                // Resolve the instance
                var instance = container.Get<IService>("abc");

                instance.ShouldBeOfType<Service>();
                instance.State.ShouldBe("abc");
            }
            // }
        }
    }
}
