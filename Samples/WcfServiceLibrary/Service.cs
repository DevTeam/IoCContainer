namespace WcfServiceLibrary
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [Ioc]
    public class Service : IService
    {
        public string GetData(int value) => $"You entered: {value}";
    }
}
