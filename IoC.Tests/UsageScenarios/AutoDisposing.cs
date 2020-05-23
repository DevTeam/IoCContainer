namespace IoC.Tests.UsageScenarios
{
    using Moq;
    using Xunit;

    public class AutoDisposing
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=basic
            // $priority=07
            // $description=Auto Disposing
            // $header=A [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance it's a very special instance. If it implements the _IDisposable_ (or IAsyncDisposable) interface the _Sigleton_ lifetime take care about disposing this instance after disposing of the owning container (where this type was registered) or if after the binding cancelation.
            // {
            var disposableService = new Mock<IDisposableService>();

            using (
                var container = Container
                .Create()
                .Bind<IService>().As(Lifetime.Singleton).To<IDisposableService>(ctx => disposableService.Object)
                .Container)
            {
                var disposableInstance = container.Resolve<IService>();
            }

            // Check the singleton was disposed after the container was disposed
            disposableService.Verify(i => i.Dispose(), Times.Once);
            // }
#if NETCOREAPP5_0 || NETCOREAPP3_1
            // {
            disposableService.Verify(i => i.DisposeAsync(), Times.Once);
            // }
#endif
        }
    }
}
