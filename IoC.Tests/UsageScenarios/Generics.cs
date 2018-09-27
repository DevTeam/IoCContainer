namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Generics
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=01
            // $description=Generics
            // {
            // Create and configure the container
            using (var container = Container.Create())
            using (container.Bind<IDependency>().To<Dependency>())
            // Bind open generic interface to open generic implementation
            using (container.Bind(typeof(IService<>)).To(typeof(Service<>)))
            // Or (it is working the same) just bind generic interface to generic implementation, using marker classes TT, TT1, TT2 and so on
            using (container.Bind<IService<TT>>().Tag("just generic").To<Service<TT>>())
            {
                // Resolve a generic instance using "open generic" binding
                var instance1 = container.Resolve<IService<int>>();

                // Resolve a generic instance using "just generic" binding
                var instance2 = container.Resolve<IService<string>>("just generic".AsTag());

                // Check the instances' types
                instance1.ShouldBeOfType<Service<int>>();
                instance2.ShouldBeOfType<Service<string>>();
            }
            // }
        }
    }
}
