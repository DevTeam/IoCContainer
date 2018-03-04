namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class Func
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=02
            // $description=Func
            // {
            Func<IService> func = () => new Service(new Dependency());
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IService>().To(ctx => func()))
            {
                // Resolve an instance
                var instance = container.Resolve<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
