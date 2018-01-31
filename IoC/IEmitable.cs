namespace IoC.Lifetimes
{
    using System.Linq.Expressions;

    [PublicAPI]
    public interface IEmitable
    {
        [NotNull] Expression Emit([CanBeNull] Expression baseExpression);
    }
}
