namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class GenericTests
    {
        private readonly IToken _token;

        public GenericTests()
        {
            _token = Container
                .Create()
                .Bind<int>().To(ctx => 2)
                .Bind<string>().To(ctx => "abc")
                .Bind<string>().Tag("null").To(ctx => null);
        }

        [Fact]
        public void ShouldCreateWhenArray()
        {
            // Given

            // When
            _token.Bind<Holder<TT>>().To(ctx => new Holder<TT>(new[] {ctx.Container.Inject<TT>()}));

            // Then
            _token.Container.Resolve<Holder<int>>();
        }

        [Fact]
        public void ShouldCreateWhenList()
        {
            // Given

            // When
            _token.Bind<Holder<TT>>().To(ctx => new Holder<TT>(new List<TT> { ctx.Container.Inject<TT>() }));

            // Then
            _token.Container.Resolve<Holder<int>>();
        }

        [Fact]
        public void ShouldCreateWhenDefault()
        {
            // Given

            // When
            _token.Bind<Holder<TT>>().To(ctx => new Holder<TT>(default(TT)));

            // Then
            _token.Container.Resolve<Holder<int>>();
        }

        [Fact]
        public void ShouldCreateWhenTypeof()
        {
            // Given

            // When
            _token.Bind<Holder<TT>>().To(ctx => new Holder<TT>(typeof(TT)));

            // Then
            _token.Container.Resolve<Holder<int>>().Type.ShouldBe(typeof(int));
        }

        [Fact]
        public void ShouldCreateWhenNewValueType()
        {
            // Given

            // When
            _token.Bind<HolderWithCtor<TTC>>().To(ctx => new HolderWithCtor<TTC>(new TTC()));

            // Then
            _token.Container.Resolve<HolderWithCtor<int>>();
        }

        [Fact]
        public void ShouldCreateWhenArrayOfNewOfValueType()
        {
            // Given

            // When
            _token.Bind<HolderWithCtor<TTC>>().To(ctx => new HolderWithCtor<TTC>(new [] { new TTC() }));

            // Then
            _token.Container.Resolve<HolderWithCtor<int>>();
        }


        [Fact]
        public void ShouldCreateWhenNewRefType()
        {
            // Given

            // When
            _token.Bind<HolderWithCtor<TTC>>().To(ctx => new HolderWithCtor<TTC>(new TTC()));

            // Then
            _token.Container.Resolve<HolderWithCtor<MyClass>>();
        }

        public class Holder<T>
        {
            public Holder(T[] value)
            {
            }

            public Holder(List<T> value)
            {
            }

            public Holder(T value)
            {
            }

            public Holder(Type type)
            {
                Type = type;
            }

            public Type Type { get; }
        }

        public class HolderWithCtor<T>
            where T : new()
        {
            public HolderWithCtor(T value) { }

            public HolderWithCtor(T[] values) { }
        }

        public class MyClass
        {
        }
    }
}