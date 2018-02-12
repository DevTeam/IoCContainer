namespace IoC.Tests.IntegrationTests
{
    using System.Diagnostics.CodeAnalysis;

    public interface IMyService : IMyService1
    {
        string Name { get; set; }

        IMyService1 SomeRef { get; }

        IMyService1 SomeRef2 { get; set; }

        IMyService1 SomeRef3 { get; set; }

        void Init(IMyService1 someRef2, IMyService1 someRef3, string intiValue);
    }

    public interface IMyService1
    {
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class MyGenericService<T> : IMyGenericService<T>
    {
        public MyGenericService(T value, IMyService service)
        {
            Value = value;
            Service = service;
        }

        public T Value { get; }

        public IMyService Service { get; }
    }

    public interface IMyGenericService<out T>
    {
        T Value { get; }

        IMyService Service { get; }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class MyService : IMyService
    {
        public MyService(string name, IMyService1 someRef)
        {
            Name = name;
            SomeRef = someRef;
        }

        public string Name { get; set; }

        public IMyService1 SomeRef { get; }

        public IMyService1 SomeRef2 { get; set; }

        public IMyService1 SomeRef3 { get; set; }

        public void Init(IMyService1 someRef2, IMyService1 someRef3, string intiValue)
        {
            Name = intiValue;
            SomeRef2 = someRef2;
            SomeRef3 = someRef3;
        }
    }

    public class MyGenericService<T1, T2> : IMyGenericService<T1, T2>
    {
    }

    public interface IMyGenericService<T1, T2> : IMyGenericService1<T1>
    {
    }

    public interface IMyGenericService1<T1>
    {
    }

    public interface IMyWrapper
    {
        IMyWrapper Wrapped { get; }
    }

    public class Wrappered : IMyWrapper
    {
        public IMyWrapper Wrapped => null;
    }

    public class Wrapper : IMyWrapper
    {
        public Wrapper(IMyWrapper wrapped)
        {
            Wrapped = wrapped;
        }

        public IMyWrapper Wrapped { get; }
    }

    public class MySimpleClass
    {
    }
}
