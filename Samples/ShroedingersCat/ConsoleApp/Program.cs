namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    // ReSharper disable once RedundantUsingDirective
    using System.Threading.Tasks;
    using IoC;

    // ReSharper disable once ClassNeverInstantiated.Global
    [SuppressMessage("ReSharper", "ArrangeTypeMemberModifiers")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    class Program
    {
#if !NET40
        public static async Task Main()
#else
        public static void Main()
#endif
        {
            using (var container = Container.Create().Using(new Glue()))
            {
                // Directly getting
                var box1 = container.Get<IBox<ICat>>();
                Console.WriteLine("#1 is alive: " + box1.Content.IsAlive);

                // Func way
                var box2 = container.FuncGet<IBox<ICat>>();
                Console.WriteLine("#2 is alive: " + box2().Content.IsAlive);

#if !NET40
                // Async way
                var box3 = await container.StartGet<IBox<ICat>>(TaskScheduler.Default);
                Console.WriteLine("#3 is alive: " + box3.Content.IsAlive);
#endif
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
