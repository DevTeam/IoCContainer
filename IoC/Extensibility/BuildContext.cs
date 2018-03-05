namespace IoC.Extensibility
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Core;
    using Core.Collections;

    /// <summary>
    /// Represents build context.
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    public class BuildContext: IDisposable
    {
        /// <summary>
        /// The container parameter.
        /// </summary>
        public static readonly ParameterExpression ContainerParameter = Expression.Parameter(typeof(IContainer), nameof(Context.Container));

        /// <summary>
        /// The args parameters.
        /// </summary>
        public static readonly ParameterExpression ArgsParameter = Expression.Parameter(typeof(object[]), nameof(Context.Args));

        /// <summary>
        /// All resolver's parameters.
        /// </summary>
        public static readonly ParameterExpression[] ResolverParameters = { ContainerParameter, ArgsParameter };

        /// <summary>
        /// Types of all resolver's parameters.
        /// </summary>
        public static readonly Type[] ResolverParameterTypes = ResolverParameters.Select(i => i.Type).ToArray();

        /// <summary>
        /// The target key.
        /// </summary>
        public readonly Key Key;

        /// <summary>
        /// The target container.
        /// </summary>
        [NotNull] public readonly IContainer Container;

        private static readonly MethodInfo GetContextDataMethodInfo = Core.TypeExtensions.Info<BuildContext>().DeclaredMethods.Single(i => i.Name == nameof(GetContextData));
        private static ResizableArray<BuildContext> _contexts = ResizableArray<BuildContext>.Empty;

        private readonly int _id;
        private readonly List<ParameterExpression> _parameters = new List<ParameterExpression>();
        private readonly List<Expression> _statements = new List<Expression>();
        private ResizableArray<object> _values = ResizableArray<object>.Empty;
        private int _curId;
        private ParameterExpression _contextExpression;

        internal BuildContext(Key key, [NotNull] IContainer container, BuildContext parentBuildContext = null)
        {
            _id = -1;
            var contexts = _contexts.Items;
            // Try finding an empty element
            for (var i = 0; i < contexts.Length; i++)
            {
                if (contexts[i] != null)
                {
                    continue;
                }

                _id = i;
                contexts[i] = this;
            }

            // An empty element was not found
            if (_id == -1)
            {
                _contexts = _contexts.Add(this);
                _id = _contexts.Items.Length - 1;
            }

            Key = key;
            Container = container ?? throw new ArgumentNullException(nameof(container));
            if (parentBuildContext != null)
            {
                _parameters.AddRange(parentBuildContext._parameters);
                _statements.AddRange(parentBuildContext._statements);
            }
        }

        /// <summary>
        /// Defines value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The value type.</param>
        /// <returns>The parameter expression.</returns>
        public ParameterExpression DefineValue([CanBeNull] object value, Type type)
        {
            var valueId = SetValue(value);
            var varExpression = Expression.Variable(type, "var" + valueId);
            _parameters.Add(varExpression);
            _statements.Add(Expression.Assign(varExpression, GetValueExpression(GetContextExpression(), valueId, type)));
            return varExpression;
        }

        /// <summary>
        /// Defines value.
        /// </summary>
        /// <param name="expression">The value expression.</param>
        /// <returns>The parameter expression.</returns>
        public ParameterExpression DefineValue([NotNull] Expression expression)
        {
            var varExpression = Expression.Variable(expression.Type, "var" + GenerateId());
            _parameters.Add(varExpression);
            _statements.Add(Expression.Assign(varExpression, expression));
            return varExpression;
        }

        /// <summary>
        /// Defines value.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The parameter expression.</returns>
        public ParameterExpression DefineValue<T>([CanBeNull] T value) => DefineValue(value, typeof(T));

        /// <summary>
        /// Closes a block of statements.
        /// </summary>
        /// <param name="expression">The target expression.</param>
        /// <returns>The result expression.</returns>
        public Expression CloseBlock(Expression expression)
        {
            if (_parameters.Any() || _statements.Any())
            {
                _statements.Add(expression);
                return Expression.Block(_parameters, _statements);
            }

            return expression;
        }

        internal Expression PartiallyCloseBlock(Expression expression, params ParameterExpression[] parameterExpressions)
        {
            var statements = (
                from binaryExpression in _statements.OfType<BinaryExpression>()
                join parameterExpression in parameterExpressions on binaryExpression.Left equals parameterExpression
                select (Expression)binaryExpression).ToList();

            foreach (var statement in statements)
            {
                _statements.Remove(statement);
            }

            statements.Add(expression);
            return Expression.Block(parameterExpressions, statements);
        }

        [MethodImpl((MethodImplOptions)256)]
        // ReSharper disable once MemberCanBePrivate.Global
        internal static object[] GetContextData(int contextId) => _contexts.Items[contextId]._values.Items;

        void IDisposable.Dispose()
        {
            _contexts.Items[_id] = null;
            Reset();
        }

        private void Reset()
        {
            _parameters.Clear();
            _statements.Clear();
            _contextExpression = null;
        }

        private int SetValue([CanBeNull] object value)
        {
            _values = _values.Add(value);
            return _values.Items.Length - 1;
        }

        [NotNull]
        private static Expression GetValueExpression([NotNull] Expression contextExpression, int valueId, [NotNull] Type type)
        {
            return Expression.ArrayAccess(contextExpression, Expression.Constant(valueId)).Convert(type);
        }

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
