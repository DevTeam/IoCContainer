namespace IoC.Lifetimes
{
    using System.Linq.Expressions;

    internal interface ILifetimeBuilder
    {
        bool TryBuildRestoreInstance([NotNull] IBuildContext context, out Expression getExpression);

        bool TryBuildBeforeCreating([NotNull] IBuildContext context, out Expression beforeCreatingExpression);

        bool TryBuildAfterCreation([NotNull] IBuildContext context, [NotNull] Expression instanceExpression, out Expression newInstanceExpression);

        bool TryBuildSaveInstance([NotNull] IBuildContext context, [NotNull] Expression instanceExpression, out Expression setExpression);
    }
}
