namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class InjectingExpressionVisitor: ExpressionVisitor
    {
        [NotNull] private static readonly MethodInfo InjectMethodInfo;
        [NotNull] private static readonly MethodInfo InjectWithTagMethodInfo;
        private static readonly MethodInfo AssigmentCallExpressionMethodInfo;
        [NotNull] private static readonly ConstructorInfo ContextConstructor;
        [NotNull] private readonly Stack<Key> _keys = new Stack<Key>();
        [NotNull] private readonly IContainer _container;
        [CanBeNull] private readonly Expression _thisExpression;
        private int _reentrancy;

        static InjectingExpressionVisitor()
        {
            Expression<Func<object>> injectExpression = () => default(IContainer).Inject<object>();
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method.GetGenericMethodDefinition();
            Expression<Func<object>> injectWithTagExpression = () => default(IContainer).Inject<object>(null);
            InjectWithTagMethodInfo = ((MethodCallExpression)injectWithTagExpression.Body).Method.GetGenericMethodDefinition();
            Expression<Action<object, object>> assigmentCallExpression = (item1, item2) => default(IContainer).Inject<object>(null, null);
            AssigmentCallExpressionMethodInfo = ((MethodCallExpression)assigmentCallExpression.Body).Method.GetGenericMethodDefinition();

            ContextConstructor = Type<Context>.Info.DeclaredConstructors.Single();
        }

        public InjectingExpressionVisitor([NotNull] Key key, [NotNull] IContainer container, [CanBeNull] Expression thisExpression)
        {
            _keys.Push(key ?? throw new ArgumentNullException(nameof(key)));
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _thisExpression = thisExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression methodCall)
        {
            if (methodCall.Method.IsGenericMethod)
            {
                // container.Inject<T>()
                if (Equals(methodCall.Method.GetGenericMethodDefinition(), InjectMethodInfo))
                {
                    var type = methodCall.Method.GetGenericArguments()[0];
                    var containerExpression = Visit(methodCall.Arguments[0]);
                    var key = new Key(type);
                    return CreateDependencyExpression(key, containerExpression);
                }

                // container.Inject<T>(tag)
                if (Equals(methodCall.Method.GetGenericMethodDefinition(), InjectWithTagMethodInfo))
                {
                    var type = methodCall.Method.GetGenericArguments()[0];
                    var containerExpression = Visit(methodCall.Arguments[0]);
                    var tagExpression = methodCall.Arguments[1];
                    var tag = GetTag(tagExpression);
                    var key = new Key(type, tag);
                    return CreateDependencyExpression(key, containerExpression);
                }

                // container.Inject<T>(destination, source)
                if (Equals(methodCall.Method.GetGenericMethodDefinition(), AssigmentCallExpressionMethodInfo))
                {
                    var dstExpression = Visit(methodCall.Arguments[1]);
                    var srcExpression = Visit(methodCall.Arguments[2]);
                    return Expression.Assign(dstExpression, srcExpression);
                }
            }

            // ctx.It
            if (methodCall.Object is MemberExpression memberExpression && memberExpression.Member is FieldInfo fieldInfo)
            {
                if (fieldInfo.DeclaringType.GetGenericTypeDefinition() == typeof(Context<>) && fieldInfo.Name == nameof(Context<object>.It))
                {
                    return Expression.Call(_thisExpression, methodCall.Method, InjectAll(methodCall.Arguments));
                }
            }

            if (methodCall.Object == null)
            {
                return Expression.Call(methodCall.Method, InjectAll(methodCall.Arguments));
            }

            return Expression.Call(Visit(methodCall.Object), methodCall.Method, InjectAll(methodCall.Arguments));
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (TryReplaceContextFields(node.Member.DeclaringType, node.Member.Name, out var expression))
            {
                return expression;
            }

            return base.VisitMember(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Type == typeof(Context))
            {
                return CreateNewContextExpression();
            }

            var typeInfo = node.Type.Info();
            if (typeInfo.IsConstructedGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Context<>))
            {
                var contextType = typeof(Context<>).Info().MakeGenericType(typeInfo.GenericTypeArguments).Info();
                var ctor = contextType.DeclaredConstructors.Single();
                return Expression.New(
                    ctor,
                    _thisExpression,
                    Expression.Constant(_keys.Peek()),
                    ResolverGenerator.ContainerParameter,
                    ResolverGenerator.ArgsParameter);
            }

            return base.VisitParameter(node);
        }

        [NotNull]
        private Expression CreateNewContextExpression()
        {
            return Expression.New(
                ContextConstructor,
                Expression.Constant(_keys.Peek()),
                ResolverGenerator.ContainerParameter,
                ResolverGenerator.ArgsParameter);
        }

        [CanBeNull]
        private object GetTag([NotNull] Expression tagExpression)
        {
            if (tagExpression == null) throw new ArgumentNullException(nameof(tagExpression));
            object tag;
            switch (tagExpression)
            {
                case ConstantExpression constant:
                    tag = constant.Value;
                    break;

                default:
                    var expression = Visit(tagExpression) ?? throw new InvalidOperationException();
                    var tagFunc = Expression.Lambda<Func<object>>(expression, true).Compile();
                    tag = tagFunc();
                    break;
            }

            return tag;
        }

        [NotNull]
        private IEnumerable<Expression> InjectAll(IEnumerable<Expression> expressions)
        {
            return expressions.Select(Visit);
        }

        [NotNull]
        private IContainer SelectedContainer([CanBeNull] Expression containerExpression)
        {
            if (containerExpression != null)
            {
                var containerSelectorExpression = Expression.Lambda<ContainerSelector>(containerExpression, true, ResolverGenerator.ContainerParameter);
                var selectContainer = containerSelectorExpression.Compile();
                return selectContainer(_container);
            }

            return _container;
        }

        [NotNull]
        private Expression CreateDependencyExpression(
            [NotNull] Key key,
            [CanBeNull] Expression containerExpression)
        {
            Expression dependencyExpression;
            ILifetime lifetime = null;
            if (Equals(key, Key.Create<Context>()))
            {
                dependencyExpression = CreateNewContextExpression();
            }
            else
            {
                if (!SelectedContainer(containerExpression).TryGetDependency(key, out var dependency, out lifetime))
                {
                    throw new InvalidOperationException();
                }

                dependencyExpression = dependency.Expression;
            }

            return CreateDependencyExpression(key, dependencyExpression, lifetime);
        }

        [NotNull]
        private Expression CreateDependencyExpression(
            [NotNull] Key key,
            [NotNull] Expression dependencyExpression,
            [CanBeNull] ILifetime lifetime)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (dependencyExpression == null) throw new ArgumentNullException(nameof(dependencyExpression));

            _keys.Push(key);
            try
            {
                _reentrancy++;
                if (_reentrancy >= 64)
                {
                    _container.Get<IIssueResolver>().CyclicDependenceDetected(key, _reentrancy);
                }

                var typesMap = new Dictionary<Type, Type>();
                dependencyExpression = ExpressionBuilder.Shared.PrepareExpression(dependencyExpression, key, typesMap);
                dependencyExpression = ExpressionBuilder.Shared.Convert(dependencyExpression, key.Type);
                dependencyExpression = ExpressionBuilder.Shared.AddLifetime(dependencyExpression, lifetime, key.Type, this);
                return Visit(dependencyExpression) ?? throw new InvalidOperationException();
            }
            finally
            {
                _keys.Pop();
                _reentrancy--;
            }
        }

        private bool TryReplaceContextFields([NotNull] Type type, string name, out Expression expression)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (Type<Context>.Info.IsAssignableFrom(type.Info()))
            {
                // ctx.Key
                if (name == nameof(Context.Key))
                {
                    expression = Expression.Constant(_keys.Peek());
                    return true;
                }

                // ctx.Container
                if (name == nameof(Context.Container))
                {
                    expression = ResolverGenerator.ContainerParameter;
                    return true;
                }

                // ctx.Args
                if (name == nameof(Context.Args))
                {
                    expression = ResolverGenerator.ArgsParameter;
                    return true;
                }
            }

            expression = default(Expression);
            return false;
        }

        private delegate IContainer ContainerSelector(IContainer container);
    }
}
