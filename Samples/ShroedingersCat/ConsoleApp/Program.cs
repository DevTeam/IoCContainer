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
                var box = container.Resolve<IBox<ICat>>();
                Console.WriteLine(box);

                // Func
                var func = container.Resolve<Func<IBox<ICat>>>();
                Console.WriteLine(func());

                // Async
                box = await container.Resolve<Task<IBox<ICat>>>();
                Console.WriteLine(box);

                // Async value
                box = await container.Resolve<ValueTask<IBox<ICat>>>();
                Console.WriteLine(box);

                // Tuple<,>
                var tuple = container.Resolve<Tuple<IBox<ICat>, ICat>>();
                Console.WriteLine(tuple.Item1 + ", " + tuple.Item2);

                // ValueTuple(,,)
                var valueTuple = container.Resolve<(IBox<ICat> box, ICat cat, IBox<ICat> anotherBox)>();
                Console.WriteLine(valueTuple.box + ", " + valueTuple.cat + ", " + valueTuple.anotherBox);

                // Lazy
                var lazy = container.Resolve<Lazy<IBox<ICat>>>();
                Console.WriteLine(lazy.Value);

                // Enumerable
                var enumerable = container.Resolve<IEnumerable<IBox<ICat>>>();
                Console.WriteLine(enumerable.Single());

                // List
                var list = container.Resolve<IList<IBox<ICat>>>();
                Console.WriteLine(list[0]);
            }
        }

        interface IBox<out T> { T Content { get; } }

        interface ICat { bool IsAlive { get; } }

        class CardboardBox<T> : IBox<T>
        {
            public CardboardBox(T content) => Content = content;

            public T Content { get; }

            public override string ToString() => Content.ToString();
        }

        class ShroedingersCat : ICat
        {
            public bool IsAlive => new Random().Next(2) == 1;

            public override string ToString() => $"Is alive: {IsAlive}";
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
