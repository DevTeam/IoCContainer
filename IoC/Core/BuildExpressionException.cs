namespace IoC.Core
{
    using System;

    internal class BuildExpressionException: InvalidOperationException
    {
        public BuildExpressionException(string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
