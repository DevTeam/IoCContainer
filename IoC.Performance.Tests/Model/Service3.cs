namespace IoC.Performance.Tests.Model
{
    using System.Diagnostics.CodeAnalysis;

    public sealed class Service3 : IService3
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        public Service3(IService4 service41, IService4 service42, IService4 service43, IService4 service44, IService4 service45)
        {
        }
    }
}