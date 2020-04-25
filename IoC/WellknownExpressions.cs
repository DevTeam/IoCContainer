namespace IoC
{
    using System.Linq.Expressions;
    using Core;

    /// <summary>
    /// The list of well-known expressions.
    /// </summary>
    [PublicAPI]
    public static class WellknownExpressions
    {
        /// <summary>
        /// The container parameter.
        /// </summary>
        [NotNull]
        public static readonly ParameterExpression ContainerParameter = Expression.Parameter(typeof(IContainer), nameof(Context.Container));

        /// <summary>
        /// The args parameters.
        /// </summary>
        [NotNull]
        public static readonly ParameterExpression ArgsParameter = Expression.Parameter(typeof(object[]), nameof(Context.Args));
    }
}
