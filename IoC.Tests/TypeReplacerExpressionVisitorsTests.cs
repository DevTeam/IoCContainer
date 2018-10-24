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
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    public class TypeReplacerExpressionVisitorsTests
    {
        [Fact]
        public void ShouldReplaceWhenArrayExpressionAndConvertExpression()
        {
            // Given
            Expression<Func<TTT[]>> expression = () => new[] { new TTT() };
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(MyClass[]));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);
            
            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<MyClass[]>>(expressionExpression).Compile();

            // Then
            func().ShouldBeOfType<MyClass[]>();
        }

        [Fact]
        public void ShouldReplaceWhenArrayExpressionAndGenericAndConvertExpression()
        {
            // Given
            Expression<Func<List<TTT>[]>> expression = () => new[] { new List<TTT>() }.ToArray();
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(List<MyClass>[]));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<List<MyClass>[]>>(expressionExpression).Compile();

            // Then
            func().ShouldBeOfType<List<MyClass>[]>();
        }

        [Fact]
        public void ShouldReplaceWhenNewExpressionAndConvertExpression()
        {
            // Given
            Expression<Func<Tuple<TT>>> expression = () => new Tuple<TT>((TT)(object)"abc");
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(Tuple<string>));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<Tuple<string>>>(expressionExpression).Compile();

            // Then
            func().Item1.ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitMethodCall()
        {
            // Given
            var myClass = new MyClass();
            Expression<Func<TT>> expression = () => (TT)(object)myClass.GetString("abc");
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(string));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitMethodCallStatic()
        {
            // Given
            Expression<Func<TT>> expression = () => (TT)(object)MyClass.StaticGetString("abc");
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(string));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitMethodCallGeneric()
        {
            // Given
            var myClass = new MyClass();
            Expression<Func<TT>> expression = () => myClass.Get((TT)(object)"abc");
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(string));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitMethodCallStaticGeneric()
        {
            // Given
            Expression<Func<TT>> expression = () => MyClass.StaticGet((TT)(object)"abc");
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(string));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenTypeConstantExpression()
        {
            // Given
            Expression<Func<Type>> expression = () => typeof(TT);
            var typesMap = new Dictionary<Type, Type> { {typeof(TT), typeof(string)} };
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<Type>>(expressionExpression).Compile();

            // Then
            func().ShouldBe(typeof(string));
        }

        [Fact]
        public void ShouldReplaceWhenVisitFieldMember()
        {
            // Given
            Expression<Func<TT>> expression = () => new MyClass2<TT>((TT)(object)"abc").Field;
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(string));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitPropertyMember()
        {
            // Given
            Expression<Func<TT>> expression = () => new MyClass2<TT>((TT)(object)"abc").Prop;
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(string));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string>>(expressionExpression).Compile();

            // Then
            func().ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitArray()
        {
            // Given
            Expression<Func<TT[]>> expression = () => new[]{(TT)(object)"abc"};
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(string[]));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<string[]>>(expressionExpression).Compile();

            // Then
            func()[0].ShouldBe("abc");
        }

        [Fact]
        public void ShouldReplaceWhenVisitListInit()
        {
            // Given
            Expression<Func<IList<TT>>> expression = () => new List<TT> { (TT)(object)"abc" };
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(IList<string>));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<IList<string>>>(expressionExpression).Compile();

            // Then
            func()[0].ShouldBe("abc");
        }


        [Fact]
        public void ShouldReplaceWhenVisitLambda()
        {
            // Given
            // ReSharper disable once RedundantDelegateCreation
            Expression<Func<Func<TT>>> expression = () => new Func<TT>(() => (TT)(object)"abc");
            var typesMap = CreateTypesMap(expression.ReturnType, typeof(Func<string>));
            var replacingVisitor = new TypeReplacerExpressionVisitor(typesMap);

            // When
            var expressionExpression = replacingVisitor.Visit(expression.Body);
            var func = Expression.Lambda<Func<Func<string>>>(expressionExpression).Compile();

            // Then
            func()().ShouldBe("abc");
        }

        private static IDictionary<Type, Type> CreateTypesMap(Type type, Type targetType)
        {
            var typesMap = new Dictionary<Type, Type>();
            TypeMapper.Shared.Map(type, targetType, typesMap);
            return typesMap;
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

        [GenericTypeArgument]
        // ReSharper disable once InconsistentNaming
        public class TTT { }
    }
}
