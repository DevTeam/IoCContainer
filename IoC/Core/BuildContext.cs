namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Represents build context.
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [PublicAPI]
    internal class BuildContext : IBuildContext
    {
        private static readonly ICollection<IBuilder> EmptyBuilders = new List<IBuilder>();
        // Should be at least internal to be accessible from for compiled code from expressions
        private readonly ICollection<IDisposable> _resources;
        [NotNull] private readonly ICollection<IBuilder> _builders;
        private readonly List<ParameterExpression> _parameters = new List<ParameterExpression>();
        private readonly List<Expression> _statements = new List<Expression>();
        private readonly IDictionary<Type, Type> _typesMap = new Dictionary<Type, Type>();
        private int _curId;

        internal BuildContext(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] ICollection<IDisposable> resources,
            [NotNull] ICollection<IBuilder> builders,
            [NotNull] IAutowiringStrategy defaultAutowiringStrategy,
            int depth = 0)
        {
            Key = key;
            Container = resolvingContainer ?? throw new ArgumentNullException(nameof(resolvingContainer));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _builders = builders ?? throw new ArgumentNullException(nameof(builders));
            AutowiringStrategy = defaultAutowiringStrategy ?? throw new ArgumentNullException(nameof(defaultAutowiringStrategy));
            Depth = depth;
        }

        public Key Key { get; }

        public IContainer Container { get; }

        public IAutowiringStrategy AutowiringStrategy { get; }

        public int Depth { get; }

        public IBuildContext CreateChild(Key key, IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return CreateChildInternal(key, container);
        }

        public Expression AppendValue(object value, Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return Expression.Constant(value, type);
        }

        public Expression AppendValue<T>(T value) => AppendValue(value, TypeDescriptor<T>.Type);

        public ParameterExpression AppendVariable(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            var varExpression = Expression.Variable(expression.Type, "var" + GenerateId());
            _parameters.Add(varExpression);
            _statements.Add(Expression.Assign(varExpression, expression));
            return varExpression;
        }

        public Expression PrepareTypes(Expression baseExpression) =>
            TypeReplacerExpressionBuilder.Shared.Build(baseExpression, this, _typesMap);

        public Expression MakeInjections(Expression baseExpression, ParameterExpression instanceExpression = null) =>
            DependencyInjectionExpressionBuilder.Shared.Build(baseExpression, this, instanceExpression);

        public Expression AppendLifetime(Expression baseExpression, ILifetime lifetime)
        {
            if (_builders.Count > 0)
            {
                var buildContext = CreateChildInternal(Key, Container, forBuilders: true);
                foreach (var builder in _builders)
                {
                    baseExpression = baseExpression.Convert(Key.Type);
                    baseExpression = builder.Build(baseExpression, buildContext);
                }
            }

            baseExpression = baseExpression.Convert(Key.Type);
            baseExpression = LifetimeExpressionBuilder.Shared.Build(baseExpression, this, lifetime);
            return CloseBlock(baseExpression);
        }

        public Expression CloseBlock(Expression targetExpression, params ParameterExpression[] variableExpressions)
        {
            if (targetExpression == null) throw new ArgumentNullException(nameof(targetExpression));
            if (variableExpressions == null) throw new ArgumentNullException(nameof(variableExpressions));
            var statements = (
                from binaryExpression in _statements.OfType<BinaryExpression>()
                join parameterExpression in variableExpressions on binaryExpression.Left equals parameterExpression
                select (Expression)binaryExpression).ToList();

            var parameterExpressions = variableExpressions.OfType<ParameterExpression>();
            if (!statements.Any() && !parameterExpressions.Any())
            {
                return targetExpression;
            }

            statements.Add(targetExpression);
            return Expression.Block(variableExpressions, statements);
        }

        [NotNull]
        private Expression CloseBlock([NotNull] Expression targetExpression)
        {
            if (targetExpression == null) throw new ArgumentNullException(nameof(targetExpression));
            if (!_parameters.Any() && !_statements.Any())
            {
                return targetExpression;
            }

            _statements.Add(targetExpression);
            return Expression.Block(_parameters, _statements);
        }

        public IBuildContext CreateChildInternal(Key key, IContainer container, bool forBuilders = false)
        {
            var child = new BuildContext(key, container, _resources, forBuilders ? EmptyBuilders : _builders, AutowiringStrategy, Depth + 1);
            child._parameters.AddRange(_parameters);
            child._statements.AddRange(_statements);
            return child;
        }

        [MethodImpl((MethodImplOptions)256)]
        private int GenerateId() => System.Threading.Interlocked.Increment(ref _curId);
    }
}
