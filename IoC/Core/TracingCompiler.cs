namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal class TracingCompiler: ICompiler
    {
        public bool TryCompile(IBuildContext context, LambdaExpression expression, out Delegate resolver)
        {
            resolver = default(Delegate);
            return false;
        }
    }
}
