namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
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
        // {
        public void Run()
        {
            var methods = new List<string>();
            // Create a container
            using (var container = Container.Create().Using<InterceptionFeature>())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            using (container.Intercept<IService>().By(new MyInterceptor(methods)))
            {
                // Resolve an instance
                var instance = container.Resolve<IService>();

                var state = instance.State;

                methods.ShouldContain("get_State");
            }
        }

        // This interceptor stores the name of the called method
        public class MyInterceptor : IInterceptor
        {
            private readonly ICollection<string> _methods;

            public MyInterceptor(ICollection<string> methods) => _methods = methods;

            public void Intercept(IInvocation invocation) => _methods.Add(invocation.Method.Name);
        }
        // }
    }
}
