namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using Extensibility;

    internal class DependencyInjectionExpressionBuilder: IExpressionBuilder<Expression>
    {
        public static readonly IExpressionBuilder<Expression> Shared = new DependencyInjectionExpressionBuilder();

        private DependencyInjectionExpressionBuilder()
        {
        }

        public Expression Build(Expression bodyExpression, IBuildContext buildContext, Expression thisExpression)
        {
            if (bodyExpression == null) throw new ArgumentNullException(nameof(bodyExpression));
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            var visitor = new DependencyInjectionExpressionVisitor(buildContext, thisExpression);
            var newExpression = visitor.Visit(bodyExpression);
            return newExpression ?? bodyExpression;
        }
    }
}
