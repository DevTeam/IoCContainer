namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using Extensibility;

    /// <summary>
    /// Represents build context.
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [PublicAPI]
    internal class BuildContext : IBuildContext
    {
        // Should be at least internal to be accessable from for compiled code from expressions
        private readonly ICollection<IDisposable> _resources;
        private readonly IExpressionCompiler _compiler;
        private readonly List<ParameterExpression> _parameters = new List<ParameterExpression>();
        private readonly List<Expression> _statements = new List<Expression>();
        private int _curId;

        internal BuildContext([NotNull] IExpressionCompiler compiler, Key key, [NotNull] IContainer container, [NotNull] ICollection<IDisposable> resources, int depth = 0)
        {
            _compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
            Key = key;
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            Depth = depth;
        }

        public Key Key { get; }

        public IContainer Container { get; }

        public int Depth { get; }

        public IBuildContext CreateChild(Key key, IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var child = new BuildContext(_compiler, key, container, _resources, Depth + 1);
            child._parameters.AddRange(_parameters);
            child._statements.AddRange(_statements);
            return child;
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

        public Expression Prepare(Expression baseExpression, ParameterExpression instanceExpression = null)
        {
            var typesMap = new Dictionary<Type, Type>();
            var expression = TypeReplacerExpressionBuilder.Shared.Build(baseExpression, this, typesMap);
            return DependencyInjectionExpressionBuilder.Shared.Build(expression, this, instanceExpression);
        }

        public Expression AppendLifetime(Expression baseExpression, ILifetime lifetime)
        {
            var expression = baseExpression.Convert(Key.Type);
            expression = LifetimeExpressionBuilder.Shared.Build(expression, this, lifetime);
            return CloseBlock(expression);
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

        [MethodImpl((MethodImplOptions)256)]
        private int GenerateId() => System.Threading.Interlocked.Increment(ref _curId);
    }
}
