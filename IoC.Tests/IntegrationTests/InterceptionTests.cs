namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Castle.DynamicProxy;
    using Features;
    using Moq;
    using Shouldly;
    using Xunit;
    using IInvocation = Castle.DynamicProxy.IInvocation;

    public class InterceptionTests
    {
        [Fact]
        public void InterceptorShouldIntercept()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name"))).Callback<IInvocation>(i => i.ReturnValue = "abc");
            using var container = Container.Create().Using<InterceptionFeature>();

            // When
            using (container.Bind<string>().To(ctx => "SomeRef"))
            using (container.Bind<IMyService1>().To(ctx => Mock.Of<IMyService1>()))
            using (container.Bind<IMyService>().To<MyService>())
            // Intercepts any invocations
            using (container.Intercept(key => true, interceptor.Object))
            {
                var instance = container.Resolve<IMyService>();
                // ReSharper disable once UnusedVariable
                var val = instance.Name;

                // Then
                val.ShouldBe("abc");
                interceptor.Verify(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name")));
            }
        }

        [Fact]
        public void InterceptorShouldInterceptWhenFunc()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name"))).Callback<IInvocation>(i => i.ReturnValue = "abc");
            using var container = Container.Create().Using<InterceptionFeature>();

            // When
            using (container.Bind<string>().To(ctx => "SomeRef"))
            using (container.Bind<IMyService1>().To(ctx => Mock.Of<IMyService1>()))
            using (container.Bind<IMyService>().To<MyService>())
            // Intercepts any invocations
            using (container.Intercept(key => true, interceptor.Object))
            {
                var instanceFactory = container.Resolve<Func<IMyService>>();
                // ReSharper disable once UnusedVariable
                var instance = instanceFactory();
                var val = instance.Name;

                // Then
                val.ShouldBe("abc");
                interceptor.Verify(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name")));
            }
        }

        [Fact]
        public void InterceptorShouldInterceptWhenEnum()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name"))).Callback<IInvocation>(i => i.ReturnValue = "abc");
            using var container = Container.Create().Using<InterceptionFeature>();

            // When
            using (container.Bind<string>().To(ctx => "SomeRef"))
            using (container.Bind<IMyService1>().To(ctx => Mock.Of<IMyService1>()))
            using (container.Bind<IMyService>().Tag(1).To<MyService>())
            using (container.Bind<IMyService>().Tag(2).To<MyService>())
            // Intercepts any invocations
            using (container.Intercept(key => true, interceptor.Object))
            {
                var instanceEnum = container.Resolve<IEnumerable<IMyService>>().ToList();
                // ReSharper disable once UnusedVariable
                var instance = instanceEnum.Last();
                var val = instance.Name;

                // Then
                val.ShouldBe("abc");
                interceptor.Verify(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name")));
            }
        }

        [Fact]
        public void InterceptorShouldInterceptWhenArray()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name"))).Callback<IInvocation>(i => i.ReturnValue = "abc");
            using var container = Container.Create().Using<InterceptionFeature>();

            // When
            using (container.Bind<string>().To(ctx => "SomeRef"))
            using (container.Bind<IMyService1>().To(ctx => Mock.Of<IMyService1>()))
            using (container.Bind<IMyService>().Tag(1).To<MyService>())
            using (container.Bind<IMyService>().Tag(2).To<MyService>())
                // Intercepts any invocations
            using (container.Intercept(key => true, interceptor.Object))
            {
                var instanceEnum = container.Resolve<IMyService[]>();
                // ReSharper disable once UnusedVariable
                var instance = instanceEnum.Last();
                var val = instance.Name;

                // Then
                val.ShouldBe("abc");
                interceptor.Verify(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name")));
            }
        }

        [Fact]
        public void InterceptorShouldInterceptWhenClass()
        {
            // Given
            var interceptor = new Mock<IInterceptor>();
            interceptor.Setup(i => i.Intercept(It.Is<IInvocation>(j => j.Method.Name == "get_Name"))).Callback<IInvocation>(i => i.ReturnValue = "abc");
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
