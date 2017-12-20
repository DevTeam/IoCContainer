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
    class Program
    {
        public static async Task<int> Main()
        {
            using (var container = Container.Create().Using(new Glue()))
            {
                // Directly getting
                var box1 = container.Get<IBox<ICat>>();
                Console.WriteLine($"#1 is alive: {box1.Content.IsAlive}");

                // Func way
                var box2 = container.FuncGet<IBox<ICat>>();
                Console.WriteLine($"#2 is alive: {box2().Content.IsAlive}");

                // Async way
                var box3 = await container.StartGet<IBox<ICat>>(TaskScheduler.Default);
                Console.WriteLine($"#3 is alive: {box3.Content.IsAlive}");
                return box3.Content.IsAlive;
            }
        }

        interface IBox<out T> { T Content { get; } }

        interface ICat { int IsAlive { get; } }

        class CardboardBox<T> : IBox<T>
        {
            public CardboardBox(T content) { Content = content; }

            public T Content { get; }
        }

        class ShroedingersCat : ICat
        {
            public int IsAlive => new Random().Next(2);
        }

        class Glue : IConfiguration
        {
            public IEnumerable<IDisposable> Apply(IContainer container)
            {
                yield return container.Pair(typeof(IBox<>), typeof(CardboardBox<>));
                yield return container.Pair<ICat, ShroedingersCat>();
            }
        }
    }
}
