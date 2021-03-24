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
        // $tag=5 Advanced
        // $priority=10
        // $description=Interception
        // $header=The _Interception_ feature allows specifying the set of bindings that will be used to produce instances wrapped by proxy objects. These proxy objects intercept any invocations to the created (or injected) instances and allow to add any logic around it: checking arguments, logging, thread safety, authorization aspects and etc.
        // {
        // To use this feature please add the NuGet package https://www.nuget.org/packages/IoC.Interception
        // or https://www.nuget.org/packages/IoC.Interception.Source
        public void Run()
        {
            var methods = new List<string>();
            using var container = Container
                // Creates the Inversion of Control container
                .Create()
                // Using the feature InterceptionFeature
                .Using<InterceptionFeature>()
                // Configures binds
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                // Intercepts any invocations
                .Intercept(key => true, new MyInterceptor(methods))
                .Container;

            // Resolve an instance
            var instance = container.Resolve<IService>();

            // Invoke the getter "get_State"
            var state = instance.State;
            instance.Dependency.Index = 1;

            // Check invocations by our interceptor
            methods.ShouldContain("get_State");
            methods.ShouldContain("set_Index");
        }

        // This interceptor just stores names of called methods
        public class MyInterceptor : IInterceptor
        {
            private readonly ICollection<string> _methods;

            // Stores the collection of called method names
            public MyInterceptor(ICollection<string> methods) => _methods = methods;

            // Intercepts the invocations and appends the called method name to the collection
            public void Intercept(IInvocation invocation)
            {
                _methods.Add(invocation.Method.Name);
                invocation.Proceed();
            }
        }
        // }
    }
}
