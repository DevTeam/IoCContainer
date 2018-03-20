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
        internal static ResizableArray<BuildContext> Contexts = ResizableArray<BuildContext>.Empty;
        private static readonly MemberInfo ContextsMemberInfo = TypeExtensions.Info<BuildContext>().DeclaredMembers.Single(i => i.Name == nameof(Contexts));
        private static readonly FieldInfo ValuesFieldInfo = (FieldInfo)TypeExtensions.Info<BuildContext>().DeclaredMembers.Single(i => i.Name == nameof(Values));
        private static readonly FieldInfo BuildContextItemsFieldInfo = (FieldInfo)TypeExtensions.Info<ResizableArray<BuildContext>>().DeclaredMembers.Single(i => i.Name == nameof(ResizableArray<object>.Items));
        private static readonly FieldInfo ObjectItemsFieldInfo = (FieldInfo)TypeExtensions.Info<ResizableArray<object>>().DeclaredMembers.Single(i => i.Name == nameof(ResizableArray<object>.Items));

        internal ResizableArray<object> Values = ResizableArray<object>.Empty;
        private readonly ICollection<IDisposable> _resources;
        private readonly int _id;
        private readonly List<ParameterExpression> _parameters = new List<ParameterExpression>();
        private readonly List<Expression> _statements = new List<Expression>();
        private int _curId;
        private int _valuesCount;
        private ParameterExpression _contextExpression;
        private ParameterExpression _contextArrayExpression;

        internal BuildContext([NotNull] IExpressionCompiler compiler, Key key, [NotNull] IContainer container, [NotNull] ICollection<IDisposable> resources)
        {
            Compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
            Key = key;
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _id = -1;
            var contexts = Contexts.Items;
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
            Contexts = Contexts.Add(this);
            resources.Add(Disposable.Create(() =>
            {
                Contexts.Items[_id] = null;
            }));
        }

        public IExpressionCompiler Compiler { get; }

        public Key Key { get; }

        public IContainer Container { get; }

        public IBuildContext CreateChild(Key key, IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var child = new BuildContext(Compiler, key, container, _resources);
            child._parameters.AddRange(_parameters);
            child._statements.AddRange(_statements);
            return child;
        }

        public Expression DefineValue(object value, Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (Compiler.IsSupportingCompextTypeConstant)
            {
                return Expression.Constant(value, type);
            }

            var fieldInfo = (FieldInfo) TypeExtensions.Info<BuildContext>().DeclaredMembers.SingleOrDefault(i => i.Name == $"State{_valuesCount:00}");
            if (fieldInfo != null)
            {
                _valuesCount++;
                fieldInfo.SetValue(this, value);
                return Expression.Field(GetContextExpression(), fieldInfo).Convert(type);
            }

            var valueId = Values.Items.Length;
            Values = Values.Add(value);
            return Expression.ArrayAccess(GetContextArrayExpression(), Expression.Constant(valueId)).Convert(type);
        }

        public Expression DefineValue<T>(T value) => DefineValue(value, typeof(T));

        public ParameterExpression DefineVariable(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            var varExpression = Expression.Variable(expression.Type, "var" + GenerateId());
            _parameters.Add(varExpression);
            _statements.Add(Expression.Assign(varExpression, expression));
            return varExpression;
        }

        public Expression CloseBlock(Expression targetExpression)
        {
            if (targetExpression == null) throw new ArgumentNullException(nameof(targetExpression));
            if (!_parameters.Any() && !_statements.Any())
            {
                return targetExpression;
            }

            _statements.Add(targetExpression);
            return Expression.Block(_parameters, _statements);
        }

        public Expression PartiallyCloseBlock(Expression targetExpression, params Expression[] expressions)
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

        [NotNull]
        private ParameterExpression GetContextArrayExpression()
        {
            if (_contextArrayExpression != null)
            {
                return _contextArrayExpression;
            }

            _contextArrayExpression = Expression.Variable(typeof(object[]), "contextArray" + GenerateId());
            var getExpression = Expression.Field(
                // Contexts[_id].Values.Items
                Expression.Field(
                    // Contexts[_id].Values
                    GetContextExpression(),
                    ValuesFieldInfo),
                ObjectItemsFieldInfo);

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
                // ReSharper disable once AssignNullToNotNullAttribute
                Expression.Field(
                    Expression.MakeMemberAccess(null, ContextsMemberInfo),
                    BuildContextItemsFieldInfo),
                Expression.Constant(_id));

            _parameters.Insert(0, _contextExpression);
            _statements.Insert(0, Expression.Assign(_contextExpression, getExpression));
            return _contextExpression;
        }

        [MethodImpl((MethodImplOptions)256)]
        private int GenerateId() => System.Threading.Interlocked.Increment(ref _curId);

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

        internal object State40;
        internal object State41;
        internal object State42;
        internal object State43;
        internal object State44;
        internal object State45;
        internal object State46;
        internal object State47;
        internal object State48;
        internal object State49;

        internal object State50;
        internal object State51;
        internal object State52;
        internal object State53;
        internal object State54;
        internal object State55;
        internal object State56;
        internal object State57;
        internal object State58;
        internal object State59;

        internal object State60;
        internal object State61;
        internal object State62;
        internal object State63;
        internal object State64;
        internal object State65;
        internal object State66;
        internal object State67;
        internal object State68;
        internal object State69;
    }
}
