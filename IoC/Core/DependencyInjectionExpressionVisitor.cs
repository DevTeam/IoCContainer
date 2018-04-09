namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Extensibility;
    using static Extensibility.WellknownExpressions;
    using static TypeDescriptorExtensions;

    internal class DependencyInjectionExpressionVisitor: ExpressionVisitor
    {
        private static readonly Key ContextKey = new Key(typeof(Context));
        [NotNull] private static readonly TypeDescriptor ContextTypeDescriptor = Descriptor<Context>();
        [NotNull] private static readonly TypeDescriptor GenericContextTypeDescriptor = typeof(Context<>).Descriptor();
        [NotNull] private static readonly MethodInfo InjectMethodInfo;
        [NotNull] private static readonly MethodInfo InjectWithTagMethodInfo;
        private static readonly MethodInfo AssigmentCallExpressionMethodInfo;
        [NotNull] private static readonly ConstructorInfo ContextConstructor;
        [NotNull] private readonly Stack<Key> _keys = new Stack<Key>();
        [NotNull] private readonly IContainer _container;
        [NotNull] private readonly IBuildContext _buildContext;
        [CanBeNull] private readonly Expression _thisExpression;

        static DependencyInjectionExpressionVisitor()
        {
            Expression<Func<object>> injectExpression = () => default(IContainer).Inject<object>();
            InjectMethodInfo = ((MethodCallExpression)injectExpression.Body).Method.GetGenericMethodDefinition();
            Expression<Func<object>> injectWithTagExpression = () => default(IContainer).Inject<object>(null);
            InjectWithTagMethodInfo = ((MethodCallExpression)injectWithTagExpression.Body).Method.GetGenericMethodDefinition();
            Expression<Action<object, object>> assigmentCallExpression = (item1, item2) => default(IContainer).Inject<object>(null, null);
            AssigmentCallExpressionMethodInfo = ((MethodCallExpression)assigmentCallExpression.Body).Method.GetGenericMethodDefinition();
            ContextConstructor = Descriptor<Context>().GetDeclaredConstructors().Single();
        }

        public DependencyInjectionExpressionVisitor([NotNull] IBuildContext buildContext, [CanBeNull] Expression thisExpression)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            _keys.Push(buildContext.Key);
            _container = buildContext.Container;
            _buildContext = buildContext;
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
                    if (dstExpression != null && srcExpression != null)
                    {
                        return Expression.Assign(dstExpression, srcExpression);
                    }
                }
            }

            // ctx.It
            if (methodCall.Object is MemberExpression memberExpression && memberExpression.Member is FieldInfo fieldInfo)
            {
                var typeDescriptor = fieldInfo.DeclaringType.Descriptor();
                if (typeDescriptor.IsConstructedGenericType() && typeDescriptor.GetGenericTypeDefinition() == typeof(Context<>) && fieldInfo.Name == nameof(Context<object>.It))
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

            var typeDescriptor = node.Type.Descriptor();
            if (typeDescriptor.IsConstructedGenericType() && typeDescriptor.GetGenericTypeDefinition() == typeof(Context<>))
            {
                var contextType = GenericContextTypeDescriptor.MakeGenericType(typeDescriptor.GetGenericTypeArguments()).Descriptor();
                var ctor = contextType.GetDeclaredConstructors().Single();
                return Expression.New(
                    ctor,
                    _thisExpression,
                    Expression.Constant(_keys.Peek()),
                    ContainerParameter,
                    ArgsParameter);
            }

            return base.VisitParameter(node);
        }

        [NotNull]
        private Expression CreateNewContextExpression()
        {
            return Expression.New(
                ContextConstructor,
                Expression.Constant(_keys.Peek()),
                ContainerParameter,
                ArgsParameter);
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
                    var expression = Visit(tagExpression) ?? throw new BuildExpressionException(new InvalidOperationException("Null expression"));
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
                if (containerExpression is ParameterExpression parameterExpression && parameterExpression.Type == typeof(IContainer))
                {
                    return _container;
                }

                var containerSelectorExpression = Expression.Lambda<ContainerSelector>(containerExpression, true, ContainerParameter);
                var selectContainer = containerSelectorExpression.Compile();
                return selectContainer(_container);
            }

            return _container;
        }

        [NotNull]
        private Expression CreateDependencyExpression(
            Key key,
            [CanBeNull] Expression containerExpression)
        {
            if (Equals(key, ContextKey))
            {
                return CreateNewContextExpression();
            }

            var selectedContainer = SelectedContainer(containerExpression);
            if (!selectedContainer.TryGetDependency(key, out var dependency, out var lifetime))
            {
                try
                {
                    var dependenctyInfo = _container.Resolve<IIssueResolver>().CannotResolveDependency(selectedContainer, key);
                    dependency = dependenctyInfo.Item1;
                    lifetime = dependenctyInfo.Item2;
                }
                catch (Exception ex)
                {
                    throw new BuildExpressionException($"Cannot find dependency {key}.", ex);
                }
            }

            _keys.Push(key);
            try
            {
                var childBuildContext = _buildContext.CreateChild(key, selectedContainer);
                if (childBuildContext.Depth >= 64)
                {
                    _container.Resolve<IIssueResolver>().CyclicDependenceDetected(key, childBuildContext.Depth);
                }

                if (dependency.TryBuildExpression(childBuildContext, lifetime, out var expression))
                {
                    return expression;
                }
                else
                {
                    try
                    {
                        return _container.Resolve<IIssueResolver>().CannotBuildExpression(childBuildContext, dependency, lifetime);
                    }
                    catch (Exception ex)
                    {
                        throw new BuildExpressionException($"Cannot build dependency {key}.", ex);
                    }
                }
            }
            finally
            {
                _keys.Pop();
            }
        }

        private bool TryReplaceContextFields([CanBeNull] Type type, string name, out Expression expression)
        {
            if (type == null)
            {
                expression = default(Expression);
                return false;
            }

            var typeDescriptor = type.Descriptor();
            if (ContextTypeDescriptor.IsAssignableFrom(typeDescriptor))
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
                    expression = ContainerParameter;
                    return true;
                }

                // ctx.Args
                if (name == nameof(Context.Args))
                {
                    expression = ArgsParameter;
                    return true;
                }
            }

            if (typeDescriptor.IsConstructedGenericType())
            {
                if (GenericContextTypeDescriptor.IsAssignableFrom(typeDescriptor.GetGenericTypeDefinition().Descriptor()))
                {
                    // ctx.It
                    if (name == nameof(Context<object>.It))
                    {
                        expression = _thisExpression;
                        return true;
                    }
                }
            }

            expression = default(Expression);
            return false;
        }

        private delegate IContainer ContainerSelector(IContainer container);
    }
}
