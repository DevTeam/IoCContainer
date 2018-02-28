namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
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
                var box1 = container.Get<IBox<ICat>>();
                Console.WriteLine("#1 is alive: " + box1.Content.IsAlive);

                // Func
                var box2 = container.Get<Func<IBox<ICat>>>();
                Console.WriteLine("#2 is alive: " + box2().Content.IsAlive);

                // Async
                var box3 = await container.Get<Task<IBox<ICat>>>();
                Console.WriteLine("#3 is alive: " + box3.Content.IsAlive);

                // Tuple<,>
                var box4 = container.Get<Tuple<IBox<ICat>, ICat>>();
                Console.WriteLine("#4 is alive: " + box4.Item1.Content.IsAlive + ", " + box4.Item2.IsAlive);

                // Lazy
                var box5 = container.Get<Lazy<IBox<ICat>>>();
                Console.WriteLine("#5 is alive: " + box5.Value.Content.IsAlive);

                // Enumerable
                var boxes6 = container.Get<IEnumerable<IBox<ICat>>>();
                Console.WriteLine("#6 is alive: " + boxes6.Single().Content.IsAlive);

                // List
                var boxes7 = container.Get<IList<IBox<ICat>>>();
                Console.WriteLine("#7 is alive: " + boxes7[0].Content.IsAlive);
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
                yield return container.Bind<IBox<TT>>().To<CardboardBox<TT>>();
                yield return container.Bind<ICat>().To<ShroedingersCat>();
            }
        }
    }
}
