namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Collections;
    using Extensibility;

    /// <summary>
    /// Represents build context.
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [PublicAPI]
    internal class BuildContext : IBuildContext
    {
        private static readonly MethodInfo GetContextDataMethodInfo = Core.TypeExtensions.Info<BuildContext>().DeclaredMethods.Single(i => i.Name == nameof(GetContextData));
        private static ResizableArray<BuildContext> _contexts = ResizableArray<BuildContext>.Empty;

        private readonly ICollection<IDisposable> _resources;
        private readonly int _id;
        private readonly List<ParameterExpression> _parameters = new List<ParameterExpression>();
        private readonly List<Expression> _statements = new List<Expression>();
        private ResizableArray<object> _values = ResizableArray<object>.Empty;
        private int _curId;
        private ParameterExpression _contextExpression;

        internal BuildContext([NotNull] IExpressionCompiler compiler, Key key, [NotNull] IContainer container, [NotNull] ICollection<IDisposable> resources)
        {
            Compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
            Key = key;
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _id = -1;
            var contexts = _contexts.Items;
            // Try finding an empty element
            for (var i = 0; i < contexts.Length; i++)
            {
                if (contexts[i] == null)
                {
                    _id = i;
                    contexts[i] = this;
                    return;
                }
            }

            // An empty element was not found
            _id = contexts.Length;
            _contexts = _contexts.Add(this);
            resources.Add(Disposable.Create(() =>
            {
                _contexts.Items[_id] = null;
            }));
        }

        [NotNull] public IExpressionCompiler Compiler { get; }

        public Key Key { get; }

        [NotNull] public IContainer Container { get; }

        [NotNull]
        public IBuildContext CreateChild(Key key, [NotNull] IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var child = new BuildContext(Compiler, key, container, _resources);
            child._parameters.AddRange(_parameters);
            child._statements.AddRange(_statements);
            return child;
        }

        [NotNull]
        public Expression DefineValue([CanBeNull] object value, [NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (Compiler.IsSupportingCompextTypeConstant)
            {
                return Expression.Constant(value, type);
            }

            var valueId = _values.Items.Length;
            _values = _values.Add(value);
            return Expression.ArrayAccess(GetContextExpression(), Expression.Constant(valueId)).Convert(type);
        }

        [NotNull]
        public Expression DefineValue<T>([CanBeNull] T value) => DefineValue(value, typeof(T));

        [NotNull]
        public ParameterExpression DefineVariable([NotNull] Expression expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            var varExpression = Expression.Variable(expression.Type, "var" + GenerateId());
            _parameters.Add(varExpression);
            _statements.Add(Expression.Assign(varExpression, expression));
            return varExpression;
        }

        [NotNull]
        public Expression CloseBlock([NotNull] Expression targetExpression)
        {
            if (targetExpression == null) throw new ArgumentNullException(nameof(targetExpression));
            if (!_parameters.Any() && !_statements.Any())
            {
                return targetExpression;
            }

            _statements.Add(targetExpression);
            return Expression.Block(_parameters, _statements);
        }

        [NotNull]
        public Expression PartiallyCloseBlock([NotNull] Expression targetExpression, [NotNull][ItemNotNull] params Expression[] expressions)
        {
            if (targetExpression == null) throw new ArgumentNullException(nameof(targetExpression));
            if (expressions == null) throw new ArgumentNullException(nameof(expressions));
            var statements = (
                from binaryExpression in _statements.OfType<BinaryExpression>()
                join parameterExpression in expressions on binaryExpression.Left equals parameterExpression
                select (Expression)binaryExpression).ToList();

            var parameterExpressions = expressions.OfType<ParameterExpression>();
            if (!statements.Any() && !parameterExpressions.Any())
            {
                return targetExpression;
            }

            statements.Add(targetExpression);
            return Expression.Block(expressions.OfType<ParameterExpression>(), statements);
        }

        [MethodImpl((MethodImplOptions)256)]
        internal static object[] GetContextData(int contextId) => _contexts.Items[contextId]._values.Items;

        [NotNull]
        private ParameterExpression GetContextExpression()
        {
            if (_contextExpression != null)
            {
                return _contextExpression;
            }

            _contextExpression = Expression.Variable(typeof(object[]), "context" + GenerateId());
            _parameters.Insert(0, _contextExpression);
            _statements.Insert(0, Expression.Assign(_contextExpression, Expression.Call(GetContextDataMethodInfo, Expression.Constant(_id))));
            return _contextExpression;
        }

        [MethodImpl((MethodImplOptions)256)]
        private int GenerateId() => System.Threading.Interlocked.Increment(ref _curId);
    }
}
