namespace IoC.Extensibility
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

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

        /// <summary>
        /// All resolver's parameters.
        /// </summary>
        [NotNull][ItemNotNull]
#if !NETSTANDARD1_0
        public static readonly IEnumerable<ParameterExpression> ResolverParameters = new List<ParameterExpression>{ ContainerParameter, ArgsParameter }.AsReadOnly();
#else
        public static readonly IEnumerable<ParameterExpression> ResolverParameters = new List<ParameterExpression>{ ContainerParameter, ArgsParameter };
#endif
    }
}
