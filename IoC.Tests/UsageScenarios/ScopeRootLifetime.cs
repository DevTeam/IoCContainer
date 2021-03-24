// ReSharper disable ClassNeverInstantiated.Local
namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;
    using static Lifetime;

    public class ScopeRootLifetime
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=2 Lifetimes
            // $priority=01
            // $description=Scope Root lifetime
            // $header=ScopeRoot lifetime creates an instance together with new scope and allows control of all scope singletons by IScopeToken.
            // {
            using var container = Container
                .Create()
                // Bind "session" as a root of scope
                .Bind<Session>().As(ScopeRoot).To<Session>()
                // Bind a dependency as a scope singleton
                .Bind<Service>().As(ScopeSingleton).To<Service>()
                // It is optional. Bind IDisposable to IScopeToken to prevent any reference to IoC types from models
                .Bind<IDisposable>().To(ctx => ctx.Container.Inject<IScopeToken>())
                .Container;

            // Resolve 2 sessions in own scopes
            var session1 = container.Resolve<Session>();
            var session2 = container.Resolve<Session>();

            // Check sessions are not equal
            session1.ShouldNotBe(session2);

            // Check scope singletons are equal in the first scope 
            session1.Service1.ShouldBe(session1.Service2);

            // Check scope singletons are equal in the second scope
            session2.Service1.ShouldBe(session2.Service2);

            // Check scope singletons are not equal for different scopes
            session1.Service1.ShouldNotBe(session2.Service1);

            // Dispose of the instance from the first scope
            session1.Dispose();

            // Check dependencies are disposed for the first scope
            session1.Service1.DisposeCounter.ShouldBe(1);

            // Dispose container
            container.Dispose();

            // Check all dependencies are disposed for the all scopes
            session2.Service1.DisposeCounter.ShouldBe(1);
            session1.Service1.DisposeCounter.ShouldBe(1);
        // }
        }

        // {
        class Service: IDisposable
        {
            public int DisposeCounter;

            public void Dispose() => DisposeCounter++;
        }

        class Session: IDisposable
        {
            private readonly IDisposable _scope;
            public readonly Service Service1;
            public readonly Service Service2;

            public Session(
                // There is no reference to the IoC type here
                IDisposable scope,
                Service service1,
                Service service2)
            {
                _scope = scope;
                Service1 = service1;
                Service2 = service2;
            }

            public void Dispose() => _scope.Dispose();
        }
        // }
    }
}
