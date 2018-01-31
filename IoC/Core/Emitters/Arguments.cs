namespace IoC.Core.Emitters
{
    using System.Linq.Expressions;

    internal static class Arguments
    {
        public const int Container = 0;
        public const int Args = 1;

        public static readonly ParameterExpression[] ResolverArgsuments =
        {
            Expression.Parameter(typeof(IContainer), "container"),
            Expression.Parameter(typeof(object[]), "args")
        };

    }
}
