namespace IoC.Core
{
    using System;

    internal class BuildExpressionException: InvalidOperationException
    {
        public BuildExpressionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BuildExpressionException(Exception innerException) : base(innerException.Message, innerException)
        {
        }
    }
}
