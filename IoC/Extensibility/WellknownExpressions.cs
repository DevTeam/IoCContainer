namespace IoC.Extensibility
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// The list of well-known expressions.
    /// </summary>
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

        /// <summary>
        /// All resolver's parameters.
        /// </summary>
        [NotNull]
        public static readonly ParameterExpression[] ResolverParameters = { ContainerParameter, ArgsParameter };
    }
}
