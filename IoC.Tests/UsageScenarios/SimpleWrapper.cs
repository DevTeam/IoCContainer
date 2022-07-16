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
        // $tag=1 Basics
        // $priority=01
        // $description=Wrapper
        // {
        public void Run()
        {
            // Create and configure a parent container
            using var parentContainer = Container
                .Create()
                // Binds a service to wrap
                .Bind<IService>().To<Service>()
                .Container;

            // Create and configure a child container
            using var childContainer = parentContainer
                .Create()
                // Binds a wrapper, injecting the base IService from the parent container via constructor
                .Bind<IService>().To<WrapperForService>()
                .Container;

            var service = childContainer.Resolve<IService>();

            service.Value.ShouldBe("Wrapper abc");
        }

        public interface IService
        {
            string Value { get; }
        }

        public class Service: IService
        {
            public string Value => "abc";
        }

        public class WrapperForService : IService
        {
            private readonly IService _wrapping;

            public WrapperForService(IService wrapping) => _wrapping = wrapping;

            public string Value => $"Wrapper {_wrapping.Value}";
        }
        // }
    }
}
