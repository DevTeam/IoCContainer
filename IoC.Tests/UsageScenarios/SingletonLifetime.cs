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
            // $tag=2 Lifetimes
            // $priority=01
            // $description=Singleton lifetime
            // $header=[Singleton](https://en.wikipedia.org/wiki/Singleton_pattern) is a design pattern that supposes for having only one instance of some class during the whole application lifetime. The main complaint about Singleton is that it contradicts the Dependency Injection principle and thus hinders testability. It essentially acts as a global constant, and it is hard to substitute it with a test when needed. The _Singleton lifetime_ is indispensable in this case.
            // $footer=The lifetime could be:
            // $footer=- _Transient_ - a new instance is creating each time (it's default lifetime)
            // $footer=- [_Singleton_](https://en.wikipedia.org/wiki/Singleton_pattern) - single instance
            // $footer=- _ContainerSingleton_ - singleton per container
            // $footer=- _ScopeSingleton_ - singleton per scope
            // $footer=- _ScopeRoot_ - root of a scope
            // $footer=- _Disposing_ - Automatically calls a Disposable() method for disposable instances
            // {
            var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Use the Singleton lifetime
                .Bind<IService>().As(Singleton).To<Service>()
                .Container;

            // Resolve the singleton twice
            var instance1 = container.Resolve<IService>();
            var instance2 = container.Resolve<IService>();

            // Check that instances from the parent container are equal
            instance1.ShouldBe(instance2);

            // Create a child container
            using var childContainer = container.Create();
            
            // Resolve the singleton twice
            var childInstance1 = childContainer.Resolve<IService>();
            var childInstance2 = childContainer.Resolve<IService>();

            // Check that instances from the child container are equal
            childInstance1.ShouldBe(childInstance2);

            // Check that instances from different containers are equal
            instance1.ShouldBe(childInstance1);

            // Dispose of instances on disposing of a container
            container.Dispose();
            ((Service)childInstance1).DisposeCount.ShouldBe(1);
            ((Service)childInstance2).DisposeCount.ShouldBe(1);
            ((Service)instance1).DisposeCount.ShouldBe(1);
            ((Service)instance2).DisposeCount.ShouldBe(1);
            // }
        }
    }
}
