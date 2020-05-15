// ReSharper disable All
namespace IoC.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

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

    public interface IMyDisposableService: IDisposable
#if NETCOREAPP5_0 || NETCOREAPP3_1
    , IAsyncDisposable
#endif
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

    public interface IMyGenericService<out T>: IMyService1
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

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class MyServiceWithDefaultCtor : IMyService
    {
        public string Name { get; set; }

        public IMyService1 SomeRef { get; }

        public IMyService1 SomeRef2 { get; set; }

        public IMyService1 SomeRef3 { get; set; }

        public void Init(IMyService1 someRef2, IMyService1 someRef3, string intiValue)
        {
            throw new NotImplementedException();
        }
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public class MyGenericService<T1, T2> : IMyGenericService<T1, T2>
    {
        private int _counter;

        public MyGenericService()
        {
        }

        public MyGenericService(string name)
        {
        }

        public void Init(MySimpleClass mySimpleClass) => MySimpleClass = mySimpleClass;

        public void Init(T1 t1 = default(T1), T2 t2 = default(T2)) { }

        public MySimpleClass MySimpleClass { get; private set; }

        public int Counter => _counter;

        public void Do()
        {
            _counter++;
        }
    }

    public interface IMyGenericService<T1, T2> : IMyGenericService1<T1>
    {
        void Do();
    }

    public interface IMyGenericService1<T1>: IMyService1
    {
    }

    public interface IMyWrapper
    {
        IMyWrapper Wrapped { get; }
    }

    public class Wrappering : IMyWrapper
    {
        public IMyWrapper Wrapped => null;
    }

    public class Wrapper : IMyWrapper
    {
        public Wrapper(IMyWrapper wrapped, Func<string, IMyWrapper> wrappedFunc, Tuple<IMyWrapper> wrapperTuple)
        {
            Wrapped = wrapped;
            Wrapped = wrappedFunc("aa");
        }

        public IMyWrapper Wrapped { get; }
    }

    public class MySimpleClass
    {
    }
}
