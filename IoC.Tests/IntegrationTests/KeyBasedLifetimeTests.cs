namespace IoC.Tests.IntegrationTests
{
    using System;
    using Lifetimes;
    using Moq;
    using Shouldly;
    using Xunit;

    public class KeyBasedLifetimeTests
    {
        [Fact]
        public void ContainerShouldResolveWhenRefBasedKeyLifetime()
        {
            // Given
            using var container = Container.Create();
            Func<IMyService> func = Mock.Of<IMyService>;
            // When
            using (container.Bind<IMyService>().Lifetime(new StringKeyLifetime("a")).To(ctx => func()))
            {
                // Then
                var instance1 = container.Resolve<IMyService>();
                var instance2 = container.Resolve<IMyService>();
                using var childContainer = container.Create();
                var instance3 = childContainer.Resolve<IMyService>();
                instance1.ShouldNotBeNull();
                instance1.ShouldBe(instance2);
                instance1.ShouldBe(instance3);
            }
        }

        [Fact]
        public void ContainerShouldResolveWhenValBasedKeyLifetime()
        {
            // Given
            using var container = Container.Create();
            Func<IMyService> func = Mock.Of<IMyService>;
            // When
            using (container.Bind<IMyService>().Lifetime(new IntKeyLifetime(99)).To(ctx => func()))
            {
                // Then
                var instance1 = container.Resolve<IMyService>();
                var instance2 = container.Resolve<IMyService>();
                using var childContainer = container.Create();
                var instance3 = childContainer.Resolve<IMyService>();
                instance1.ShouldNotBeNull();
                instance1.ShouldBe(instance2);
                instance1.ShouldBe(instance3);
            }
        }

        public class StringKeyLifetime: KeyBasedLifetime<string>
        {
            [NotNull] private readonly string _key;

            public StringKeyLifetime([NotNull] string key) => _key = key ?? throw new ArgumentNullException(nameof(key));

            public override ILifetime CreateLifetime() => new StringKeyLifetime(_key);

            protected override string CreateKey(IContainer container, object[] args) => _key;
        }

        public class IntKeyLifetime : KeyBasedLifetime<int>
        {
            private readonly int _key;

            public IntKeyLifetime(int key) => _key = key;

            public override ILifetime CreateLifetime() => new IntKeyLifetime(_key);

            protected override int CreateKey(IContainer container, object[] args) => _key;
        }
    }
}
