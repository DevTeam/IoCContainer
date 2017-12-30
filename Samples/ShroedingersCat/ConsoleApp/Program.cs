namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using IoC;

    // ReSharper disable once ClassNeverInstantiated.Global
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    public class Program
    {
        public static async Task Main()
        {
            using (var container = Container.Create().Using<Glue>())
            {
                // Directly getting
                var box1 = container.Get<IBox<ICat>>();
                Console.WriteLine("#1 is alive: " + box1.Content.IsAlive);

                // Func way
                var box2 = container.FuncGet<IBox<ICat>>();
                Console.WriteLine("#2 is alive: " + box2().Content.IsAlive);

                // Async way
                var box3 = await container.AsyncGet<IBox<ICat>>(TaskScheduler.Default);
                Console.WriteLine("#3 is alive: " + box3.Content.IsAlive);
            }
        }

        interface IBox<out T> { T Content { get; } }

        interface ICat { bool IsAlive { get; } }

        class CardboardBox<T> : IBox<T>
        {
            public CardboardBox(T content) { Content = content; }

            public T Content { get; }
        }

        class ShroedingersCat : ICat
        {
            public bool IsAlive => new Random().Next(2) == 1;
        }

        class Glue : IConfiguration
        {
            public IEnumerable<IDisposable> Apply(IContainer container)
            {
                yield return container.Bind(typeof(IBox<>)).To(typeof(CardboardBox<>));
                yield return container.Bind<ICat>().To<ShroedingersCat>();
            }
        }
    }
}
