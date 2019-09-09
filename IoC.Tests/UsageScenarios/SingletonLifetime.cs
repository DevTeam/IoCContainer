namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;
    using static Lifetime;

    public class SingletonLifetime
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=02
            // $description=Singleton lifetime
            // $header=Singleton is a design pattern which stands for having only one instance of some class during the whole application lifetime. The main complaint about Singleton is that it contradicts the Dependency Injection principle and thus hinders testability. It essentially acts as a global constant, and it is hard to substitute it with a test when needed. The _Singleton lifetime_ is indispensable in this case.
            // $footer=The lifetime could be:
            // $footer=- _Singleton_ - single instance
            // $footer=- _ContainerSingleton_ - singleton per container
            // $footer=- _ScopeSingleton_ - singleton per scope
            // $footer=
            // $footer=_Transient_ - is default lifetime and a new instance is creating each time
            // {
            // Create and configure the container
            using var container = Container.Create();
            using (container.Bind<IDependency>().To<Dependency>())
                // Use the Singleton lifetime
            using (container.Bind<IService>().As(Singleton).To<Service>())
            {
                // Resolve the singleton twice
                var parentInstance1 = container.Resolve<IService>();
                var parentInstance2 = container.Resolve<IService>();

                // Check that instances from the parent container are equal
                parentInstance1.ShouldBe(parentInstance2);

                // Create a child container
                using var childContainer = container.Create();
                // Resolve the singleton twice
                var childInstance1 = childContainer.Resolve<IService>();
                var childInstance2 = childContainer.Resolve<IService>();

                // Check that instances from the child container are equal
                childInstance1.ShouldBe(childInstance2);

                // Check that instances from different containers are equal
                parentInstance1.ShouldBe(childInstance1);
            }

            // }
        }
    }
}
