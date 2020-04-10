namespace IoC.Core
{
    using System;

#if !WINDOWS_UWP && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETCOREAPP1_0 && !NETCOREAPP1_1
    [Serializable]
#endif
    internal sealed class BuildExpressionException : InvalidOperationException
    {
        public BuildExpressionException(string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        { }
    }
}
