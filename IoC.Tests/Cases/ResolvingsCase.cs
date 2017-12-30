namespace IoC.Tests.Cases
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ResolvingsCase
    {
        [Fact]
        public async void Run()
        {
            using (var container = Container.Create())
            {
                // Singletone
                container.Bind<IService>().Tag("singletone").Lifetime(Lifetime.Singletone).To<Service>().ToSelf();
                var singletone1 = container.Tag("singletone").Get<IService>();
                var singletone2 = container.Tag("singletone").Get<IService>();
                singletone1.ShouldBe(singletone2);
                singletone2.Name.ShouldBe("noname");

                // Autowiring
                container.Bind<IService>().Tag("autowiring").To<Service>().ToSelf();
                var autowiring = container.Tag("autowiring").Get<IService>();
                autowiring.Name.ShouldBe("noname");

                // Autowiring with argsuments
                container.Bind<IService>().Tag("autowiring with arg").To<Service>(Has.Arg("name", 0)).ToSelf();
                var autowiringWithArg = container.Tag("autowiring with arg").Get<IService>("custom name");
                autowiringWithArg.Name.ShouldBe("custom name");

                // ... using Func with argsuments
                var autowiringWithArgFunc = container.Tag("autowiring with arg").FuncGet<string, IService>();
                for (var i = 0; i < 10; i++)
                {
                    var newInstance = autowiringWithArgFunc($"name {i}");
                    newInstance.Name.ShouldBe($"name {i}");
                }

                // Singletone created manually
                container.Bind<IService>().Tag("singletone created manually").Lifetime(Lifetime.Container).To<Service>(() => new Service("my name")).ToSelf();
                var byHand = container.Tag("singletone created manually").Get<IService>();
                byHand.Name.ShouldBe("my name");

                // ... using async way
                var service = await container.Tag("singletone created manually").AsyncGet<IService>(TaskScheduler.Default);
                service.ShouldBe(byHand);

                // Resolve all possible
                var all = container.Get<IEnumerable<IService>>("some name").ToList();
                all.Count.ShouldBe(4);
            }
        }

        public interface IService
        {
            string Name { get; }
        }

        public class Service : IService
        {
            // ReSharper disable once UnusedMember.Global
            public Service(): this("noname")
            {
            }

            public Service(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }
    }
}
