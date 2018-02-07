namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal class Autowring: IDependency
    {
        [NotNull] [ItemNotNull] public readonly Expression[] Statements;

        public Autowring([NotNull] Expression factory, [NotNull][ItemNotNull] Expression[] statements)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (statements == null) throw new ArgumentNullException(nameof(statements));
            Expression = (factory as LambdaExpression ?? throw new ArgumentException(nameof(factory))).Body;
            Statements = statements.Cast<LambdaExpression>().Select(i => i.Body).ToArray();
        }

        public Expression Expression { get; }
    }
}
