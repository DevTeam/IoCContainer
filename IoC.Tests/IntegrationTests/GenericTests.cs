// ReSharper disable UnusedParameter.Local
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
        public void ShouldCreateWhenObjectCast()
        {
            // Given

            // When
            // ReSharper disable once RedundantCast
            _token.Bind<Holder<TT>>().To(ctx => new Holder<TT>((TT)(object)ctx.Container.Inject<TT>()));

            // Then
            _token.Container.Resolve<Holder<int>>();
        }

        [Fact]
        public void ShouldCreateWhenCast()
        {
            // Given

            // When
            // ReSharper disable once RedundantCast
            _token.Bind<Holder<TT>>().To(ctx => new Holder<TT>((TT)ctx.Container.Inject<TT>()));

            // Then
            _token.Container.Resolve<Holder<int>>();
        }

        [Fact]
        public void ShouldCreateWhenTypeAs()
        {
            // Given

            // When
            // ReSharper disable once RedundantCast
            _token.Bind<Holder<TT>>().To(ctx => new Holder<TT>(ctx.Container.Inject<TT>() as TT));

            // Then
            _token.Container.Resolve<Holder<string>>();
        }

        [Fact]
        public void ShouldCreateWhenConditionalAndTypeIs()
        {
            // Given

            // When
            // ReSharper disable once RedundantCast
            // ReSharper disable once SuspiciousTypeConversion.Global
            // ReSharper disable once UseNameOfInsteadOfTypeOf
            _token.Bind<Holder<TT>>().To(ctx => new Holder<TT>((ctx.Container.Inject<TT>() as object) is TT ? (TT)(object)typeof(TT).Name : (TT)(object)"0"));

            // Then
            var val = _token.Container.Resolve<Holder<string>>().Value;
            val.ShouldBe("String");
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
            public readonly T Value;

            public Holder(T[] arr)
            {
            }

            public Holder(List<T> list)
            {
            }

            public Holder(T value)
            {
                Value = value;
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