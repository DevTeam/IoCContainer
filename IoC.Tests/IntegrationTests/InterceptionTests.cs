namespace IoC.Tests.IntegrationTests
{
    using System.Reflection;
    using Castle.DynamicProxy;
    using Features;
    using Moq;
    using Xunit;
    using IInvocation = Castle.DynamicProxy.IInvocation;

    public class InterceptionTests
    {
        [Fact]
        public void InterceptorShouldIntercept()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name")));
            using var container = Container.Create().Using<InterceptionFeature>();

            // When
            using (container.Bind<string>().To(ctx => "SomeRef"))
            using (container.Bind<IMyService1>().To(ctx => Mock.Of<IMyService1>()))
            using (container.Bind<IMyService>().To<MyService>())
            using (container.Intercept<IMyService>(interceptor.Object))
            {
                var instance = container.Resolve<IMyService>();
                // ReSharper disable once UnusedVariable
                var val = instance.Name;

                // Then
                interceptor.Verify(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name")));
            }
        }

        [Fact]
        public void InterceptorShouldInterceptWhenClass()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name")));
            using var container = Container.Create().Using<InterceptionFeature>();

            // When
            using (container.Bind<string>().To(ctx => "SomeRef"))
            using (container.Bind<IMyService1>().To(ctx => Mock.Of<IMyService1>()))
            using (container.Bind<MyServiceWithDefaultCtor>().To<MyServiceWithDefaultCtor>())
            using (container.Intercept<MyServiceWithDefaultCtor>(interceptor.Object))
            {
                var instance = (IMyService)container.Resolve<MyServiceWithDefaultCtor>();
                // ReSharper disable once UnusedVariable
                var val = instance.Name;

                // Then
                interceptor.Verify(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name")));
            }
        }

        [Fact]
        public void InterceptorShouldInterceptWhenRegistrationInChildContainer()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name")));
            using var container = Container.Create().Using<InterceptionFeature>();
            using var childContainer = container.Create();

            // When
            using (childContainer.Bind<string>().To(ctx => "SomeRef"))
            using (childContainer.Bind<IMyService1>().To(ctx => Mock.Of<IMyService1>()))
            using (childContainer.Bind<IMyService>().To<MyService>())
            using (container.Intercept<IMyService>(interceptor.Object))
            {
                var instance = childContainer.Resolve<IMyService>();
                // ReSharper disable once UnusedVariable
                var val = instance.Name;

                // Then
                interceptor.Verify(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name")));
            }
        }

        [Fact]
        public void InterceptorShouldInterceptWhenDefinedGenericType()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "Do")));
            using var container = Container.Create().Using<InterceptionFeature>();

            // When
            using (container.Bind<IMyGenericService<string, int>>().To<MyGenericService<string, int>>())
            using (container.Intercept<IMyGenericService<string, int>>(interceptor.Object))
            {
                var instance = container.Resolve<IMyGenericService<string, int>>();
                instance.Do();

                // Then
                interceptor.Verify(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "Do")));
            }
        }

        [Fact]
        public void InterceptorShouldInterceptWhenUndefinedGenericType()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "Do")));
            using var container = Container.Create().Using<InterceptionFeature>();

            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            using (container.Intercept(new Key(typeof(IMyGenericService<,>)), interceptor.Object))
            {
                var instance = container.Resolve<IMyGenericService<string, int>>();
                instance.Do();

                // Then
                interceptor.Verify(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "Do")));
            }
        }

        [Fact]
        public void InterceptorShouldInterceptWhenGenericType()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "Do")));
            using var container = Container.Create().Using<InterceptionFeature>();

            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            using (container.Intercept<IMyGenericService<TT1, TT2>>(interceptor.Object))
            {
                var instance = container.Resolve<IMyGenericService<string, int>>();
                instance.Do();

                // Then
                interceptor.Verify(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "Do")));
            }
        }
    }
}
