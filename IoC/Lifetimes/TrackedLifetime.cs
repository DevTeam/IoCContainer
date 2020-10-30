namespace IoC.Lifetimes
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Core;
    using static Core.TypeDescriptorExtensions;

    public abstract class TrackedLifetime: ILifetime, ILifetimeBuilder
    {
        private static readonly MethodInfo BeforeCreatingMethodInfo = Descriptor<TrackedLifetime>().GetDeclaredMethods().Single(i => i.Name == nameof(BeforeCreating));
        private static readonly MethodInfo AfterCreationMethodInfo = Descriptor<TrackedLifetime>().GetDeclaredMethods().Single(i => i.Name == nameof(AfterCreation));
        private readonly LifetimeDirector _lifetimeDirector;
        private readonly ConstantExpression _thisConst;

        protected TrackedLifetime()
        {
            _lifetimeDirector = new LifetimeDirector(this);
            _thisConst = Expression.Constant(this);
        }

        public Expression Build(IBuildContext context, Expression bodyExpression) =>
            _lifetimeDirector.Build(context, bodyExpression).Convert(bodyExpression.Type);

        public virtual void Dispose() { }

        public abstract ILifetime CreateLifetime();

        public virtual IContainer SelectContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        bool ILifetimeBuilder.TryBuildRestoreInstance(IBuildContext context, out Expression getExpression)
        {
            getExpression = default(Expression);
            return false;
        }

        bool ILifetimeBuilder.TryBuildSaveInstance(IBuildContext context, Expression instanceExpression, out Expression setExpression)
        {
            setExpression = default(Expression);
            return false;
        }

        bool ILifetimeBuilder.TryBuildBeforeCreating(IBuildContext context, out Expression beforeCreatingExpression)
        {
            beforeCreatingExpression = Expression.Call(_thisConst, BeforeCreatingMethodInfo, context.ContainerParameter, context.ArgsParameter);
            return true;
        }

        bool ILifetimeBuilder.TryBuildAfterCreation(IBuildContext context, Expression instanceExpression, out Expression newInstanceExpression)
        {
            newInstanceExpression = Expression.Call(_thisConst, AfterCreationMethodInfo, instanceExpression, context.ContainerParameter, context.ArgsParameter);
            return true;
        }

        /// <summary>
        /// Invoked before the new instance creation.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The created instance.</returns>
        protected virtual void BeforeCreating(IContainer container, object[] args) { }

        /// <summary>
        /// Invoked after a new instance has been created.
        /// </summary>
        /// <param name="newInstance">The new instance.</param>
        /// <param name="container">The target container.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The created instance.</returns>
        protected virtual object AfterCreation(object newInstance, IContainer container, object[] args)
            => newInstance;
    }
}
