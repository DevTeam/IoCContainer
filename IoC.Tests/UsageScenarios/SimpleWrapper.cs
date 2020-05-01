namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class SimpleWrapper
    {
        [Fact]
        // $visible=true
        // $tag=injection
        // $priority=01
        // $description=Simple Wrapper
        // {
        public void Run()
        {
            // Create and configure the root container
            using var rootContainer = Container
                .Create("root")
                .Bind<string>().To(ctx => "abc")
                // Binds the service to wrap
                .Bind<INamedService>().To<NamedService>()
                .Container;

            // Create and configure the child container
            using var childContainer = rootContainer
                .Create("child")
                .Bind<IDependency>().To<Dependency>()
                // Binds wrapper, injecting the base INamedService from the parent container "root" via constructor
                .Bind<INamedService>().To<Wrapper>()
                .Container;

            var service = childContainer.Resolve<INamedService>();

            service.Name.ShouldBe("Wrapper abc");
        }

        public class Wrapper : INamedService
        {
            private readonly INamedService _wrapping;
            
            public Wrapper(INamedService wrapping) => _wrapping = wrapping;

            public string Name => $"Wrapper {_wrapping.Name}";
        }

        // }
    }
}
