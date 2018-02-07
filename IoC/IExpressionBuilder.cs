namespace IoC
{
    using System.Linq.Expressions;

    [PublicAPI]
    public interface IExpressionBuilder
    {
        [NotNull] Expression Build([CanBeNull] Expression baseExpression);
    }
}
