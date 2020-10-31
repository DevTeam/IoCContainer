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
            // $tag=1 Basics
            // $priority=01
            // $description=Generics
            // $header=Autowriting of generic types via binding of open generic types or generic type markers are working the same way.
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind open generic interface to open generic implementation
                .Bind(typeof(IService<>)).To(typeof(Service<>))
                // Or (it is working the same) just bind generic interface to generic implementation, using marker classes TT, TT1, TT2 and so on
                .Bind<IService<TT>>().Tag("just generic").To<Service<TT>>()
                .Container;

            // Resolve a generic instance using "open generic" binding
            var instance1 = container.Resolve<IService<int>>();

            // Resolve a generic instance using "just generic" binding
            var instance2 = container.Resolve<IService<string>>("just generic".AsTag());
            // }
            // Check the instances' types
            instance1.ShouldBeOfType<Service<int>>();
            instance2.ShouldBeOfType<Service<string>>();
        }
    }
}
