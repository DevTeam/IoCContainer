namespace IoC.Lifetimes
{
    using System.Linq.Expressions;
    using Core;

    [PublicAPI]
    public abstract class SimpleLifetime: ILifetime
    {
        public abstract ILifetime CreateLifetime();

        public virtual IContainer SelectContainer(IContainer registrationContainer, IContainer resolvingContainer) =>
            resolvingContainer;

        public Expression Build(IBuildContext context, Expression bodyExpression)
        {
            var resolverType = bodyExpression.Type.ToResolverType();
            var resolver = Expression.Constant(Expression.Lambda(resolverType, bodyExpression, false, context.ContainerParameter, context.ArgsParameter).Compile());
            return Expression.Call(
                Expression.Constant(this),
                nameof(GetOrCreateInstance),
                new []{ bodyExpression.Type },
                resolver,
                context.ContainerParameter,
                context.ArgsParameter);
        }

        protected virtual T GetOrCreateInstance<T>(Resolver<T> resolver, IContainer container, object[] args) => resolver(container, args);

        public virtual void Dispose() { }
    }
}
