namespace IoC.Core.Emitters
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading;

    internal class Emitter
    {
        private int _varId;
        [NotNull] public readonly List<ParameterExpression> LocalVars = new List<ParameterExpression>();
        [NotNull] private readonly Stack<Expression> _stack = new Stack<Expression>();
        [NotNull] private readonly ParameterExpression[] _parameters;

        public Emitter([NotNull] params ParameterExpression[] parameters)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        [NotNull]
        public Emitter Newobj([NotNull] ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null) throw new ArgumentNullException(nameof(constructorInfo));
            var parameterCount = constructorInfo.GetParameters().Length;
            var expression = Expression.New(constructorInfo, Pop(parameterCount));
            _stack.Push(expression);
            return this;
        }

        [NotNull]
        public Emitter Store([NotNull] Expression variable)
        {
            if (variable == null) throw new ArgumentNullException(nameof(variable));
            var rightExpression = _stack.Pop();
            _stack.Push(Expression.Assign(variable, rightExpression));
            return this;
        }

        [NotNull]
        public Emitter Block(int statementsCount, [NotNull] params ParameterExpression[] parameterExpressions)
        {
            if (parameterExpressions == null) throw new ArgumentNullException(nameof(parameterExpressions));
            var expressions = Pop(statementsCount);
            _stack.Push(Expression.Block(parameterExpressions, expressions));
            return this;
        }

        [NotNull]
        public ParameterExpression DeclareLocal([NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var localVar = Expression.Variable(type, $"var{Interlocked.Increment(ref _varId)}");
            AddLocalVariable(localVar);
            return localVar;
        }

        [NotNull]
        public Emitter LoadArg(int index)
        {
            _stack.Push(_parameters[index]);
            return this;
        }

        [NotNull]
        public Emitter LoadConst([NotNull] Type type, object value)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            _stack.Push(Expression.Constant(value, type));
            return this;
        }

        [NotNull]
        public Emitter LoadConst<T>(T value)
        {
            return LoadConst(typeof(T), value);
        }

        [NotNull]
        public Emitter Cast([NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var instance = _stack.Pop();
            _stack.Push(Expression.Convert(instance, type));
            return this;
        }

        [NotNull]
        public Emitter LoadElem()
        {
            Expression[] indexes = { _stack.Pop() };
            var array = _stack.Pop();
            _stack.Push(Expression.ArrayAccess(array, indexes));
            return this;
        }

        [NotNull]
        public Emitter Call([NotNull] MethodInfo methodInfo)
        {
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            var parameterCount = methodInfo.GetParameters().Length;
            var arguments = Pop(parameterCount);
            MethodCallExpression methodCallExpression;
            if (!methodInfo.IsStatic)
            {
                var instance = _stack.Pop();
                methodCallExpression = Expression.Call(instance, methodInfo, arguments);
            }
            else
            {
                methodCallExpression = Expression.Call(null, methodInfo, arguments);
            }

            _stack.Push(methodCallExpression);
            return this;
        }

        public Emitter Push(Expression expression)
        {
            _stack.Push(expression);
            return this;
        }

        public Expression Pop()
        {
            return _stack.Pop();
        }

        private void AddLocalVariable([NotNull] ParameterExpression localVar)
        {
            if (localVar == null) throw new ArgumentNullException(nameof(localVar));
            LocalVars.Add(localVar);
        }

        private Expression[] Pop(int statementsCount)
        {
            var expressions = new Expression[statementsCount];
            for (var i = statementsCount - 1; i >= 0; i--)
            {
                expressions[i] = _stack.Pop();
            }

            return expressions;
        }
    }
}
