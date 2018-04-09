namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using Extensibility;
    using static Core.TypeDescriptorExtensions;

    /// <summary>
    /// Represents singleton lifetime.
    /// </summary>
    [PublicAPI]
    public sealed class SingletonLifetime : ILifetime, IDisposable
    {
        private static readonly FieldInfo LockObjectFieldInfo = Descriptor<SingletonLifetime>().GetDeclaredFields().Single(i => i.Name == nameof(LockObject));
        private static readonly FieldInfo InstanceFieldInfo = Descriptor<SingletonLifetime>().GetDeclaredFields().Single(i => i.Name == nameof(Instance));

        [NotNull] internal object LockObject = new object();
        internal volatile object Instance;

        /// <inheritdoc />
        public Expression Build(Expression expression, IBuildContext buildContext, object state)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var type = expression.Type;
            var thisVar = buildContext.AppendValue(this);
            var lockObjectField = Expression.Field(thisVar, LockObjectFieldInfo);
            var instanceField = Expression.Field(thisVar, InstanceFieldInfo);
            var typedInstance = instanceField.Convert(type);

            // if(this.Instance != null)
            return Expression.Condition(
                Expression.NotEqual(instanceField, ExpressionExtensions.NullConst),
                // return (T)this.Instance;
                typedInstance,
                Expression.Block(
                    // lock(this.LockObject)
                    Expression.IfThen(
                        // if(this.Instance == null)
                        Expression.Equal(instanceField, ExpressionExtensions.NullConst),
                        // this.Instance = new T();
                        Expression.Assign(instanceField, expression)).Lock(lockObjectField),
                    // return (T)this.Instance;
                    typedInstance));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            IDisposable disposable;
            lock (LockObject)
            {
                disposable = Instance as IDisposable;
            }

            disposable?.Dispose();
        }

        /// <inheritdoc />
        public ILifetime Clone() => new SingletonLifetime();

        /// <inheritdoc />
        public override string ToString() => Lifetime.Singleton.ToString();
    }
}
