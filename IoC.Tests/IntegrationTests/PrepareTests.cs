﻿namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Linq;
    using Moq;
    using Shouldly;
    using Xunit;

    public class PrepareTests
    {
        [Fact]
        public void ContainerShouldPrepare()
        {
            // Given
            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                Func<IMyService1> func1 = Mock.Of<IMyService1>;
                // When
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Singleton).Tag(1).To(ctx => func()))
                using (container.Bind<IMyService1>().Tag("abc").To(ctx => func1()))
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Transient).Tag("xyz").To(ctx => func()))
                {
                    var result = container.Prepare();

                    // Then
                    result.IsValid.ShouldBeTrue();
                }
            }
        }

        [Fact]
        public void ContainerShouldPrepareWhenFailedToResolve()
        {
            // Given
            var expectedUnresolveKey = new Key(typeof(IMyService), "failed");

            using (var container = Container.Create())
            {
                Func<IMyService> func = Mock.Of<IMyService>;
                Func<IMyService1> func1 = Mock.Of<IMyService1>;
                // When
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Singleton).Tag(1).To(ctx => func()))
                using (container.Bind<IMyService1>().Tag("abc").To(ctx => func1()))
                using (container.Bind<IMyService, IMyService1>().Lifetime(Lifetime.Transient).Tag("xyz").To(ctx => func()))
                using (container.Bind<IMyService>().Tag("failed").To(ctx => new MyService(ctx.Container.Inject<string>("failedDep"), ctx.Container.Inject<IMyService1>())))
                {
                    var result = container.Prepare();

                    // Then
                    result.IsValid.ShouldBeFalse();
                    result.UnresolvedKeys.ShouldBe(Enumerable.Repeat(expectedUnresolveKey, 1));
                }
            }
        }

        [Fact]
        public void ContainerShouldPrepareWhenGenericFailedToResolve()
        {
            // Given
            var expectedUnresolveKey = new Key(typeof(IMyGenericService<TT, TT1>), "failed");

            using (var container = Container.Create())
            {
                // When
                using (container.Bind<IMyGenericService<TT,TT1>> ().Tag("failed").To(ctx => new MyGenericService<TT, TT1>(ctx.Container.Inject<string>())))
                {
                    var result = container.Prepare();

                    // Then
                    result.IsValid.ShouldBeFalse();
                    result.UnresolvedKeys.ShouldBe(Enumerable.Repeat(expectedUnresolveKey, 1));
                }
            }
        }
    }
}