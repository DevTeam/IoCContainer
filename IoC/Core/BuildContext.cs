﻿namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Extensibility;
    using static TypeDescriptorExtensions;
    using CollectionExtensions = Core.CollectionExtensions;

    /// <summary>
    /// Represents build context.
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [PublicAPI]
    internal class BuildContext : IBuildContext
    {
        // Should be at least internal to be accessable from for compiled code from expressions
        internal static BuildContext[] Contexts = Core.CollectionExtensions.EmptyArray<BuildContext>();
        private static readonly MemberInfo ContextsMemberInfo = Descriptor<BuildContext>().GetDeclaredMembers().Single(i => i.Name == nameof(Contexts));
        private static readonly FieldInfo ValuesFieldInfo = Descriptor<BuildContext>().GetDeclaredFields().Single(i => i.Name == nameof(Values));

        // Should be at least internal to be accessable from for compiled code from expressions
        internal object[] Values = Core.CollectionExtensions.EmptyArray<object>();
        private readonly ICollection<IDisposable> _resources;
        private readonly int _id;
        private readonly IExpressionCompiler _compiler;
        private readonly List<ParameterExpression> _parameters = new List<ParameterExpression>();
        private readonly List<Expression> _statements = new List<Expression>();
        private int _curId;
        private int _valuesCount;
        private ParameterExpression _contextExpression;
        private ParameterExpression _contextArrayExpression;

        internal BuildContext([NotNull] IExpressionCompiler compiler, Key key, [NotNull] IContainer container, [NotNull] ICollection<IDisposable> resources, int depth = 0)
        {
            _compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
            Key = key;
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            Depth = depth;
            _id = -1;
            // Try finding an empty element
            for (var i = 0; i < Contexts.Length; i++)
            {
                if (Contexts[i] == null)
                {
                    _id = i;
                    Contexts[i] = this;
                    return;
                }
            }

            // An empty element was not found
            _id = Contexts.Length;
            Contexts = Contexts.Add(this);
            resources.Add(Disposable.Create(() =>
            {
                Contexts[_id] = null;
            }));
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
            if (type.Descriptor().IsValueType() || _compiler.IsReferenceConstantSupported)
            {
                return Expression.Constant(value, type);
            }

            var fieldInfo = (FieldInfo) Descriptor<BuildContext>().GetDeclaredMembers().SingleOrDefault(i => i.Name == $"State{_valuesCount:00}");
            if (fieldInfo != null && _valuesCount < StatesCount)
            {
                _valuesCount++;
                fieldInfo.SetValue(this, value);
                return Expression.Field(GetContextExpression(), fieldInfo).Convert(type);
            }

            var valueId = Values.Length;
            Values = Values.Add(value);
            return Expression.ArrayAccess(GetContextArrayExpression(), Expression.Constant(valueId)).Convert(type);
        }

        public Expression AppendValue<T>(T value) => AppendValue(value, typeof(T));

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

        [NotNull]
        private ParameterExpression GetContextArrayExpression()
        {
            if (_contextArrayExpression != null)
            {
                return _contextArrayExpression;
            }

            _contextArrayExpression = Expression.Variable(typeof(object[]), "contextArray" + GenerateId());
            var getExpression = 
                // Contexts[_id].Values
                Expression.Field(
                    // Contexts[_id].Values
                    GetContextExpression(),
                    ValuesFieldInfo);

            _parameters.Insert(1, _contextArrayExpression);
            _statements.Insert(1, Expression.Assign(_contextArrayExpression, getExpression));
            return _contextArrayExpression;
        }

        [NotNull]
        private ParameterExpression GetContextExpression()
        {
            if (_contextExpression != null)
            {
                return _contextExpression;
            }

            _contextExpression = Expression.Variable(typeof(BuildContext), "context" + GenerateId());
            var getExpression = Expression.ArrayAccess(
                // Contexts
                Expression.MakeMemberAccess(null, ContextsMemberInfo),
                Expression.Constant(_id));

            _parameters.Insert(0, _contextExpression);
            _statements.Insert(0, Expression.Assign(_contextExpression, getExpression));
            return _contextExpression;
        }

        [MethodImpl((MethodImplOptions)256)]
        private int GenerateId() => System.Threading.Interlocked.Increment(ref _curId);

        private const int StatesCount = 40;

        internal object State00;
        internal object State01;
        internal object State02;
        internal object State03;
        internal object State04;
        internal object State05;
        internal object State06;
        internal object State07;
        internal object State08;
        internal object State09;

        internal object State11;
        internal object State12;
        internal object State13;
        internal object State14;
        internal object State15;
        internal object State16;
        internal object State17;
        internal object State18;
        internal object State19;

        internal object State20;
        internal object State21;
        internal object State22;
        internal object State23;
        internal object State24;
        internal object State25;
        internal object State26;
        internal object State27;
        internal object State28;
        internal object State29;

        internal object State30;
        internal object State31;
        internal object State32;
        internal object State33;
        internal object State34;
        internal object State35;
        internal object State36;
        internal object State37;
        internal object State38;
        internal object State39;
    }
}
