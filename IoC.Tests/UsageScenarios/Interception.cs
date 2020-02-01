// ReSharper disable UnusedVariable
namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using Castle.DynamicProxy;
    using Features;
    using Shouldly;
    using Xunit;

    public class Interception
    {
        [Fact]
        // $visible=true
        // $tag=customization
        // $priority=00
        // $description=Interception
        // $header=The _Interception_ feature allows specify the set of bindings which will be used to produce instances wrapped by proxy objects. These proxy objects intercept any invocations to the created (or injected) instances and allows to add any logic around it: checking arguments, logging, thread safety, authorization aspects and etc.
        // {
        // To use this feature just add the NuGet package https://www.nuget.org/packages/IoC.Interception
        // or https://www.nuget.org/packages/IoC.Interception.Source
        public void Run()
        {
            var methods = new List<string>();
            // Create and configure the container
            using var container = Container
                // Creates an Inversion of Control container
                .Create()
                // Using the feature InterceptionFeature
                .Using<InterceptionFeature>()
                // Configures binds
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                // Configures interception for IService calls
                .Intercept<IService>(new MyInterceptor(methods))
                .Container;

            // Resolve an instance
            var instance = container.Resolve<IService>();

            // Invoke the getter "get_State"
            var state = instance.State;

            // Check invocations from the interceptor
            methods.ShouldContain("get_State");
        }

        // This interceptor just stores the name of called methods
        public class MyInterceptor : IInterceptor
        {
            private readonly ICollection<string> _methods;

            // Stores the collection of called method names
            public MyInterceptor(ICollection<string> methods) => _methods = methods;

            // Intercepts the invocations and appends the called method name to the collection
            public void Intercept(IInvocation invocation) => _methods.Add(invocation.Method.Name);
        }
        // }
    }
}
