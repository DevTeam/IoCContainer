namespace IoC.Core.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Core;

    internal sealed class SingletoneLifetime : ILifetime, IDisposable, IExpressionBuilder
    {
        [NotNull] private object _lockObject = new object();
        private volatile object _instance;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            if (_instance != null)
            {
                return (T) _instance;
            }

            lock (_lockObject)
            {
                if (_instance == null)
                {
                    _instance = resolver(container, args);
                }
            }

            return (T)_instance;
        }

        public void Dispose()
        {
            lock (_lockObject)
            {
                if (_instance is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        public ILifetime Clone()
        {
            return new SingletoneLifetime();
        }

        public override string ToString()
        {
            return Lifetime.Singletone.ToString();
        }

        private static readonly MethodInfo EnterMethod = typeof(Monitor).Info().DeclaredMethods.Single(i => i.Name == "Enter" && i.GetParameters().Length == 2);
        private static readonly MethodInfo ExitMethod = typeof(Monitor).Info().DeclaredMethods.Single(i => i.Name == "Exit");
        private static readonly ParameterExpression LockWasTakenVar = Expression.Variable(typeof(bool), "lockWasTaken");
        private static readonly Expression NullConst = Expression.Constant(null);

        public Expression Build(Expression baseExpression)
        {
            if (baseExpression == null) throw new NotSupportedException($"The argument {nameof(baseExpression)} should not be null for lifetime.");

            var instanceField = Expression.Field(Expression.Constant(this), nameof(_instance));
            var typedInstance = Expression.Convert(instanceField, baseExpression.Type);
            var lockObject = Expression.Constant(_lockObject);
            
            var underLockStatement = Expression.TryFinally(
                Expression.Block(
                    baseExpression.Type,
                    Expression.Call(null, EnterMethod, lockObject, LockWasTakenVar),
                    Expression.IfThen(Expression.Equal(instanceField, NullConst), Expression.Assign(instanceField, baseExpression)),
                    typedInstance),
                Expression.IfThen(Expression.IsTrue(LockWasTakenVar), Expression.Call(null, ExitMethod, lockObject))
            );

            var lifetimeBody = Expression.Condition(
                Expression.NotEqual(instanceField, NullConst),
                typedInstance,
                Expression.Block(new[] { LockWasTakenVar }, underLockStatement));

            return lifetimeBody;
        }
    }
}
