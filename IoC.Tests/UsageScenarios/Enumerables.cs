namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using Xunit;

    public class Enumerables
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=05
            // $description=Resolve all possible items as IEnumerable<>
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().Tag(1).To<Service>())
            using (container.Bind<IService>().Tag(2).To<Service>())
            using (container.Bind<IService>().Tag(3).To<Service>())
            {
                // Resolve all possible instances
                var instances = container.Get<IEnumerable<IService>>().ToList();

                instances.Count.ShouldBe(3);
                instances.ForEach(instance => instance.ShouldBeOfType<Service>());
            }
            // }
        }
    }
}
