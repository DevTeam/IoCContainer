namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class Collection
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=05
            // $description=Resolve all appropriate instances as ICollection
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().Tag(1).To<Service>())
            using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
            using (container.Bind<IService>().Tag(3).To<Service>())
            {
                // Resolve all appropriate instances
                var instances = container.Resolve<ICollection<IService>>();

                instances.Count.ShouldBe(3);
                foreach (var instance in instances)
                {
                    instance.ShouldBeOfType<Service>();
                }
            }
            // }
        }
    }
}
