namespace IoC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    public class TypeMapingAndReplacingExpressionVisitorsTests
    {
        [Fact]
        public void ShouldReplaceWhenNewExpressionAndConvertExpression()
        {
            // Given
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(Tuple<string>), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            Expression<Func<Tuple<TT>>> expression = () => new Tuple<TT>((TT)(object)"abc");

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<Tuple<string>>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));
            typesMap.First().Value.ShouldBe(typeof(string));
            func().Item1.ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitMethodCall()
        {
            // Given
            var myClass = new MyClass();
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(string), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            Expression<Func<TT>> expression = () => (TT)(object)myClass.GetString("abc");

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));
            typesMap.First().Value.ShouldBe(typeof(string));
            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitMethodCallStatic()
        {
            // Given
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(string), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            Expression<Func<TT>> expression = () => (TT)(object)MyClass.StaticGetString("abc");

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));
            typesMap.First().Value.ShouldBe(typeof(string));
            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitMethodCallGeneric()
        {
            // Given
            var myClass = new MyClass();
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(string), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            Expression<Func<TT>> expression = () => myClass.Get((TT)(object)"abc");

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));

            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitMethodCallStaticGeneric()
        {
            // Given
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(string), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            Expression<Func<TT>> expression = () => MyClass.StaticGet((TT)(object)"abc");

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));

            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenTypeConstantExpression()
        {
            // Given
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(string), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            Expression<Func<Type>> expression = () => typeof(TT);

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<Type>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));
            typesMap.First().Value.ShouldBe(typeof(string));
            func().ShouldBe(typeof(string));
        }

        [Fact]
        public void ShouldReplaceWhenVisitFieldMember()
        {
            // Given
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(string), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            Expression<Func<TT>> expression = () => new MyClass2<TT>((TT)(object)"abc").Field;

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));
            typesMap.First().Value.ShouldBe(typeof(string));
            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitPropertyMember()
        {
            // Given
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(string), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            Expression<Func<TT>> expression = () => new MyClass2<TT>((TT)(object)"abc").Prop;

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));
            typesMap.First().Value.ShouldBe(typeof(string));
            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitArray()
        {
            // Given
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(string), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            Expression<Func<TT[]>> expression = () => new[]{(TT)(object)"abc"};

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string[]>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));
            typesMap.First().Value.ShouldBe(typeof(string));
            func()[0].ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitListInit()
        {
            // Given
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(string), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            Expression<Func<IList<TT>>> expression = () => new List<TT> { (TT)(object)"abc" };

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<IList<string>>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));
            typesMap.First().Value.ShouldBe(typeof(string));
            func()[0].ShouldBe("abc");
        }


        [Fact]
        public void ShouldReplaceWhenVisitLambda()
        {
            // Given
            var typesMap = new Dictionary<Type, Type>();
            var typeMapingVisitor = new TypeMapingExpressionVisitor(typeof(string), typesMap);
            var replacingVisitor = new TypeReplacingExpressionVisitor(typesMap);
            // ReSharper disable once RedundantDelegateCreation
            Expression<Func<Func<TT>>> expression = () => new Func<TT>(() => (TT)(object)"abc");

            // When
            typeMapingVisitor.Visit(expression.Body);
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<Func<string>>>(expressionExpression).Compile();

            // Then
            typesMap.Count.ShouldBe(1);
            typesMap.First().Key.ShouldBe(typeof(TT));
            typesMap.First().Value.ShouldBe(typeof(string));
            func()().ShouldBe("abc");
        }

        public class MyClass
        {
            public string GetString(string value)
            {
                return value;
            }

            public static string StaticGetString(string value)
            {
                return value;
            }

            public T Get<T>(T value)
            {
                return value;
            }

            public static T StaticGet<T>(T value)
            {
                return value;
            }
        }

        public class MyClass2<T>
        {
            public MyClass2(T field)
            {
                Field = field;
            }

            public T Field;

            public T Prop => Field;
        }
    }
}
