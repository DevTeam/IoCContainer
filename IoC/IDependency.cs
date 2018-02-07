namespace IoC
{
    using System.Linq.Expressions;

    [PublicAPI]
    public interface IDependency
    {
        [NotNull] Expression Expression { get; }
    }
}
