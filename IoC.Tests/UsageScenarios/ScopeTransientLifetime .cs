namespace IoC.Tests.UsageScenarios
{
    using System;
    using Moq;
    using Xunit;
    using static Lifetime;

    public class ScopeTransientLifetime
    {
        [Fact]
        // $visible=true
        // $tag=2 Lifetimes
        // $priority=01
        // $description=Scope Transient lifetime
        // $header=This lifetime is similar to a transient (default) lifetime, except that each disposable instance is automatically disposed of when a current scope is disposed of.
        // {
        public void Run()
        {
            var dependency = new Mock<IDisposingDependency>();
            using var container = Container
                .Create()
                .Bind<IService>().As(ScopeSingleton).To<Service>()
                .Bind<IDependency>().As(ScopeTransient).To(ctx => dependency.Object)
                .Container;

            // Create scope
            var scope = container.Resolve<IScope>();
            using (scope.Activate())
            {
                container.Resolve<IService>();
            }

            // Dispose of scope
            scope.Dispose();

            // Verify that scope transient instance was disposed
            dependency.Verify(i => i.Dispose());
        }

        public interface IDisposingDependency: IDependency, IDisposable { }
        // }
    }
}
