// ReSharper disable ConstantNullCoalescingCondition
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using static TypeDescriptorExtensions;
    // ReSharper disable once RedundantNameQualifier
    using IContainer = IoC.IContainer;

    internal sealed class DependencyInjectionExpressionVisitor : ExpressionVisitor
    {
        private static readonly Exception InvalidExpressionError = new BuildExpressionException("Invalid expression", null);

        private static readonly Key ContextKey = new Key(typeof(Context));
        private static readonly TypeDescriptor ContextTypeDescriptor = new TypeDescriptor(typeof(Context));
        private static readonly TypeDescriptor GenericContextTypeDescriptor = typeof(Context<>).Descriptor();
        [NotNull] private static readonly ConstructorInfo ContextConstructor;
        [NotNull] private readonly IContainer _container;
        [NotNull] private readonly IBuildContext _buildContext;
        [CanBeNull] private readonly Expression _thisExpression;

        static DependencyInjectionExpressionVisitor()
        {
            ContextConstructor = Descriptor<Context>().GetDeclaredConstructors().Single();
        }

        public DependencyInjectionExpressionVisitor([NotNull] IBuildContext buildContext, [CanBeNull] Expression thisExpression)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            _container = buildContext.Container;
            _buildContext = buildContext;
            _thisExpression = thisExpression;
        }

        [SuppressMessage("ReSharper", "NotResolvedInText")]
        protected override Expression VisitMethodCall(MethodCallExpression methodCall)
        {
            var argumentsCount = methodCall.Arguments.Count;
            if (argumentsCount > 0)
            {
                if (methodCall.Method.IsGenericMethod)
                {
                    var genericMethodDefinition = methodCall.Method.GetGenericMethodDefinition();

                    // container.Inject<T>()
                    if (Equals(genericMethodDefinition, Injections.InjectGenericMethodInfo))
                    {
                        var containerExpression = methodCall.Arguments[0];
                        var key = new Key(methodCall.Method.ReturnType);
                        return CreateDependencyExpression(key, containerExpression, null);
                    }

                    if (Equals(genericMethodDefinition, Injections.TryInjectGenericMethodInfo))
                    {
                        var containerExpression = methodCall.Arguments[0];
                        var key = new Key(methodCall.Method.ReturnType);
                        return CreateDependencyExpression(key, containerExpression, Expression.Default(methodCall.Method.ReturnType));
                    }

                    if (Equals(genericMethodDefinition, Injections.TryInjectValueGenericMethodInfo))
                    {
                        var containerExpression = methodCall.Arguments[0];
                        var keyType = methodCall.Method.GetGenericArguments()[0];
                        var key = new Key(keyType);
                        var defaultExpression = Expression.Default(methodCall.Method.ReturnType);
                        var expression = CreateDependencyExpression(key, containerExpression, defaultExpression);
                        if (expression == defaultExpression)
                        {
                            return defaultExpression;
                        }

                        var ctor = methodCall.Method.ReturnType.Descriptor().GetDeclaredConstructors().First(i => i.GetParameters().Length == 1);
                        return Expression.New(ctor, expression);
                    }

                    if (argumentsCount > 2)
                    {
                        // container.Inject<T>(tag, args)
                        if (Equals(genericMethodDefinition, Injections.InjectWithTagGenericMethodInfo))
                        {
                            var containerExpression = methodCall.Arguments[0];
                            var tagExpression = methodCall.Arguments[1];
                            var tag = GetTag(tagExpression);
                            var argsExpression = Visit(methodCall.Arguments[2]);

                            var key = new Key(methodCall.Method.ReturnType, tag);
                            return OverrideArgsAndCreateDependencyExpression(argsExpression, key, containerExpression, null);
                        }

                        if (Equals(genericMethodDefinition, Injections.TryInjectWithTagGenericMethodInfo))
                        {
                            var containerExpression = methodCall.Arguments[0];
                            var tagExpression = methodCall.Arguments[1];
                            var tag = GetTag(tagExpression);
                            var argsExpression = Visit(methodCall.Arguments[2]);

                            var key = new Key(methodCall.Method.ReturnType, tag);
                            return OverrideArgsAndCreateDependencyExpression(argsExpression, key, containerExpression, Expression.Default(methodCall.Method.ReturnType));
                        }

                        if (Equals(genericMethodDefinition, Injections.TryInjectValueWithTagGenericMethodInfo))
                        {
                            var containerExpression = methodCall.Arguments[0];
                            var tagExpression = methodCall.Arguments[1];
                            var tag = GetTag(tagExpression);
                            var argsExpression = Visit(methodCall.Arguments[2]);

                            var keyType = methodCall.Method.GetGenericArguments()[0];
                            var key = new Key(keyType, tag);
                            var defaultExpression = Expression.Default(methodCall.Method.ReturnType);
                            var expression = OverrideArgsAndCreateDependencyExpression(argsExpression, key, containerExpression, defaultExpression);
                            if (expression == defaultExpression)
                            {
                                return defaultExpression;
                            }

                            var ctor = methodCall.Method.ReturnType.Descriptor().GetDeclaredConstructors().First(i => i.GetParameters().Length == 1);
                            return Expression.New(ctor, expression);
                        }

                        if (argumentsCount == 3)
                        {
                            // container.Inject<T>(destination, source)
                            if (Equals(genericMethodDefinition, Injections.AssignGenericMethodInfo))
                            {
                                var dstExpression = Visit(methodCall.Arguments[1]);
                                var srcExpression = Visit(methodCall.Arguments[2]);
                                var containerVar = Expression.Variable(typeof(IContainer));
                                return Expression.Block(
                                    new [] { containerVar },
                                    Expression.Assign(containerVar, Visit(methodCall.Arguments[0])),
                                    Expression.Assign(dstExpression ?? throw InvalidExpressionError, srcExpression ?? throw InvalidExpressionError),
                                    containerVar);
                            }
                        }
                    }
                }
                else
                {
                    if (argumentsCount > 1)
                    {
                        // container.Inject(type)
                        if (Equals(methodCall.Method, Injections.InjectMethodInfo))
                        {
                            var type = (Type) ((ConstantExpression) Visit(methodCall.Arguments[1]) ?? throw InvalidExpressionError).Value ?? throw InvalidExpressionError;
                            var containerExpression = methodCall.Arguments[0];
                            var key = new Key(type);
                            return CreateDependencyExpression(key, containerExpression, null);
                        }

                        if (Equals(methodCall.Method, Injections.TryInjectMethodInfo))
                        {
                            var type = (Type)((ConstantExpression)Visit(methodCall.Arguments[1]) ?? throw InvalidExpressionError).Value ?? throw InvalidExpressionError;
                            var containerExpression = methodCall.Arguments[0];
                            var key = new Key(type);
                            return CreateDependencyExpression(key, containerExpression, Expression.Default(type));
                        }

                        if (argumentsCount > 3)
                        {
                            // container.Inject(type, tag, args)
                            if (Equals(methodCall.Method, Injections.InjectWithTagMethodInfo))
                            {
                                var type = (Type)((ConstantExpression)Visit(methodCall.Arguments[1]) ?? throw InvalidExpressionError).Value ?? throw InvalidExpressionError;
                                var containerExpression = methodCall.Arguments[0];
                                var tagExpression = methodCall.Arguments[2];
                                var tag = GetTag(tagExpression);
                                var argsExpression = Visit(methodCall.Arguments[3]);

                                var key = new Key(type, tag);
                                return OverrideArgsAndCreateDependencyExpression(argsExpression, key, containerExpression, null);
                            }

                            if (Equals(methodCall.Method, Injections.TryInjectWithTagMethodInfo))
                            {
                                var type = (Type)((ConstantExpression)Visit(methodCall.Arguments[1]) ?? throw InvalidExpressionError).Value ?? throw InvalidExpressionError;
                                var containerExpression = methodCall.Arguments[0];
                                var tagExpression = methodCall.Arguments[2];
                                var tag = GetTag(tagExpression);
                                var argsExpression = Visit(methodCall.Arguments[3]);

                                var key = new Key(type, tag);
                                return OverrideArgsAndCreateDependencyExpression(argsExpression, key, containerExpression, Expression.Default(type));
                            }
                        }
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

        private Expression OverrideArgsAndCreateDependencyExpression(Expression argsExpression, Key key, Expression containerExpression, DefaultExpression defaultExpression)
        {
            if (argsExpression is NewArrayExpression newArrayExpression && newArrayExpression.Expressions.Count == 0)
            {
                return CreateDependencyExpression(key, containerExpression, defaultExpression);
            }

            var argsVar = Expression.Variable(_buildContext.ArgsParameter.Type);
            return Expression.Block(
                new[] { argsVar },
                Expression.Assign(_buildContext.ArgsParameter, argsExpression),
                CreateDependencyExpression(key, containerExpression, defaultExpression));
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var result = base.VisitUnary(node);

            if (result is UnaryExpression unaryExpression)
            {
                switch (unaryExpression.NodeType)
                {
                    case ExpressionType.Convert:
                        if (unaryExpression.Type == unaryExpression.Operand.Type)
                        {
                            return unaryExpression.Operand;
                        }

                        break;
                }
            }

            return result;
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

            if (_thisExpression != null)
            {
                var typeDescriptor = node.Type.Descriptor();
                if (typeDescriptor.IsConstructedGenericType() && typeDescriptor.GetGenericTypeDefinition() == typeof(Context<>))
                {
                    var contextType = GenericContextTypeDescriptor.MakeGenericType(typeDescriptor.GetGenericTypeArguments()).Descriptor();
                    var ctor = contextType.GetDeclaredConstructors().Single();
                    return Expression.New(
                        ctor,
                        _thisExpression,
                        Expression.Constant(_buildContext.Key),
                        _buildContext.ContainerParameter,
                        _buildContext.ArgsParameter);
                }
            }

            return base.VisitParameter(node);
        }

        private Expression CreateNewContextExpression() =>
            Expression.New(
                ContextConstructor,
                Expression.Constant(_buildContext.Key),
                _buildContext.ContainerParameter,
                _buildContext.ArgsParameter);

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
                    // ReSharper disable once ConstantNullCoalescingCondition
                    var expression = Visit(tagExpression) ?? throw new BuildExpressionException($"Invalid tag expression {tagExpression}.", new InvalidOperationException());
                    if (_buildContext.TryCompile(Expression.Lambda(expression, true), out var tagFunc, out var error))
                    {
                        tag = ((Func<object>)tagFunc)();
                    }
                    else
                    {
                        throw error;
                    }

                    break;
            }

            return tag;
        }

        private IEnumerable<Expression> InjectAll(IEnumerable<Expression> expressions) => 
            expressions.Select(Visit);

        private IContainer SelectedContainer([NotNull] Expression containerExpression)
        {
            if (containerExpression is ParameterExpression parameterExpression && parameterExpression.Type == typeof(IContainer))
            {
                return _container;
            }

            var containerSelectorExpression = Expression.Lambda<ContainerSelector>(containerExpression, true, _buildContext.ContainerParameter);
            if (_buildContext.TryCompile(containerSelectorExpression, out var selectContainer, out var error))
            {
                return ((ContainerSelector)selectContainer)(_container);
            }

            throw error;
        }

        private Expression CreateDependencyExpression(Key key, [CanBeNull] Expression containerExpression, DefaultExpression defaultExpression)
        {
            if (Equals(key, ContextKey))
            {
                return CreateNewContextExpression();
            }

            var selectedContainer = containerExpression != null ? SelectedContainer(Visit(containerExpression) ?? throw InvalidExpressionError) : _container;
            return _buildContext.CreateChild(key, selectedContainer).CreateExpression(defaultExpression);
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
                    expression = Expression.Constant(_buildContext.Key);
                    return true;
                }

                // ctx.Container
                if (name == nameof(Context.Container))
                {
                    expression = _buildContext.ContainerParameter;
                    return true;
                }

                // ctx.Args
                if (name == nameof(Context.Args))
                {
                    expression = _buildContext.ArgsParameter;
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
