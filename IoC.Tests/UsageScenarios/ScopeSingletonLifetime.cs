namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ScopeSingletonLifetime
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=08
            // $priority=00
            // $description=Scope Singleton lifetime
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().As(Lifetime.ScopeSingleton).To<Dependency>())
            {
                using (container.Bind<IService>().As(Lifetime.ScopeSingleton).To<Service>())
                {
                    // Default scope
                    var instance1 = container.Resolve<IService>();
                    var instance2 = container.Resolve<IService>();
                    instance1.ShouldBe(instance2);

                    // Scope "1"
                    using (var scope = new Scope("1"))
                    using (scope.Begin())
                    {
                        var instance3 = container.Resolve<IService>();
                        var instance4 = container.Resolve<IService>();

                        instance3.ShouldBe(instance4);
                        instance3.ShouldNotBe(instance1);
                    }

                    // Default scope again
                    var instance5 = container.Resolve<IService>();
                    instance5.ShouldBe(instance1);
                }

                // Reconfigure to check dependencies only
                using (container.Bind<IService>().As(Lifetime.Transient).To<Service>())
                {
                    // Default scope
                    var instance1 = container.Resolve<IService>();
                    var instance2 = container.Resolve<IService>();
                    instance1.Dependency.ShouldBe(instance2.Dependency);

                    // Scope "1"
                    using (var scope = new Scope("1"))
                    using (scope.Begin())
                    {
                        var instance3 = container.Resolve<IService>();
                        instance3.Dependency.ShouldNotBe(instance1.Dependency);
                    }
                }
            }
            // }
        }
    }
}
