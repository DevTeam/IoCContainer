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
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IService>().To(ctx => func()))
            {
                // Resolve the instance
                var instance = container.Get<IService>();

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
