namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;
    using static Lifetime;

    public class ScopeSingletonLifetime
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=03
            // $description=Scope Singleton lifetime
            // $header=Each scope has its own [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance for specific binding. Scopes can be created, activated and deactivated. Scope can be injected like other registered container instances.
            // {
            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<IDependency>().As(ScopeSingleton).To<Dependency>()
                .Container;

            // Use the Scope Singleton lifetime for instance
            using (container.Bind<IService>().As(ScopeSingleton).To<Service>())
            {
                // Resolve the default scope singleton twice
                var defaultScopeInstance1 = container.Resolve<IService>();
                var defaultScopeInstance2 = container.Resolve<IService>();

                // Check that instances from the default scope are equal
                defaultScopeInstance1.ShouldBe(defaultScopeInstance2);

                // Using the scope #1
                using var scope1 = container.CreateScope();
                using (scope1.Activate())
                {
                    var scopeInstance1 = container.Resolve<IService>();
                    var scopeInstance2 = container.Resolve<IService>();

                    // Check that instances from the scope #1 are equal
                    scopeInstance1.ShouldBe(scopeInstance2);

                    // Check that instances from different scopes are not equal
                    scopeInstance1.ShouldNotBe(defaultScopeInstance1);
                }

                // Default scope again
                var defaultScopeInstance3 = container.Resolve<IService>();

                // Check that instances from the default scope are equal
                defaultScopeInstance3.ShouldBe(defaultScopeInstance1);
            }

            // Reconfigure the container to check dependencies only
            using (container.Bind<IService>().As(Transient).To<Service>())
            {
                // Resolve transient instances
                var transientInstance1 = container.Resolve<IService>();
                var transientInstance2 = container.Resolve<IService>();

                // Check that transient instances are not equal
                transientInstance1.ShouldNotBe(transientInstance2);

                // Check that dependencies from the default scope are equal
                transientInstance1.Dependency.ShouldBe(transientInstance2.Dependency);

                // Using the scope #1
                using var scope2 = container.CreateScope();
                using (scope2.Activate())
                {
                    // Resolve a transient instance in scope #2
                    var transientInstance3 = container.Resolve<IService>();

                    // Check that dependencies from different scopes are not equal
                    transientInstance3.Dependency.ShouldNotBe(transientInstance1.Dependency);
                }
            }

            // }
        }
    }
}
