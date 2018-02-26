namespace IoC.Core
{
    using System.Linq.Expressions;
    using Extensibility;

    internal class DependencyInjectionExpressionBuilder: IExpressionBuilder<Expression>
    {
        public static readonly IExpressionBuilder<Expression> Shared = new DependencyInjectionExpressionBuilder();

        private DependencyInjectionExpressionBuilder()
        {
        }

        public Expression Build(Expression expression, Key key, IContainer container, Expression thisExpression)
        {
            var visitor = new DependencyInjectionExpressionVisitor(key, container, thisExpression);
            var newExpression = visitor.Visit(expression);
            return newExpression ?? expression;
        }
    }
}
