namespace IoC.Dependencies
{
    using System;
    using System.Linq.Expressions;
    using Core.Emitters;
    using Lifetimes;

    public class ResolverExpression<T>: IDependency, IEmitable
    {
        [NotNull] public readonly Expression<Resolver<T>> Expression;

        public ResolverExpression([NotNull] Expression<Resolver<T>> expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public Type Type => Expression.Type;

        public Expression Emit(Expression baseExpression)
        {
            // ReSharper disable once CoVariantArrayConversion
            return System.Linq.Expressions.Expression.Invoke(Expression, Arguments.ResolverParameters);
        }
    }
}
