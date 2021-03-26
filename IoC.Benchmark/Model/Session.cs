namespace IoC.Benchmark.Model
{
    using System;

    public class Session : ISession
    {
        public readonly IService1 Service1;
        public readonly IService2 Service21;
        public readonly IService2 Service22;
        public readonly IService2 Service23;
        public readonly IService3 Service3;
        public readonly IDisposable Disposable;
        
        public Session(IService1 service1, IService2 service21, IService2 service22, IService2 service23, IService3 service3, IDisposable disposable)
        {
            Service1 = service1;
            Service21 = service21;
            Service22 = service22;
            Service23 = service23;
            Service3 = service3;
            Disposable = disposable;
        }
    }
}