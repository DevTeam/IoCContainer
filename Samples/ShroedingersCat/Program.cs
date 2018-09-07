// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace ShroedingersCat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using IoC;

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
                Console.WriteLine("Func: " + func());

                // Async
                box = await container.Resolve<Task<IBox<ICat>>>();
                Console.WriteLine("Task: " + box);

                // Async value
                box = await container.Resolve<ValueTask<IBox<ICat>>>();
                Console.WriteLine("ValueTask: " + box);

                // Tuple
                var tuple = container.Resolve<Tuple<IBox<ICat>, ICat>>();
                Console.WriteLine("Tuple: " + tuple.Item1 + ", " + tuple.Item2);

                // ValueTuple
                var valueTuple = container.Resolve<(IBox<ICat> box, ICat cat, IBox<ICat> anotherBox)>();
                Console.WriteLine("ValueTuple: " + valueTuple.box + ", " + valueTuple.cat + ", " + valueTuple.anotherBox);

                // Lazy
                var lazy = container.Resolve<Lazy<IBox<ICat>>>();
                Console.WriteLine("Lazy: " + lazy.Value);

                // Enumerable
                var enumerable = container.Resolve<IEnumerable<IBox<ICat>>>();
                Console.WriteLine("IEnumerable: " + enumerable.Single());

                // List
                var list = container.Resolve<IList<IBox<ICat>>>();
                Console.WriteLine("IList: " + list[0]);

                // Reactive
                var source = container.Resolve<IObservable<IBox<ICat>>>();
                using (source.Subscribe(value => Console.WriteLine("IObservable: " + value))) { }

                // Complex
                var complex = container.Resolve<Lazy<Func<Task<IEnumerable<IBox<ICat>>>>>>();
                Console.WriteLine("Complex: " + (await complex.Value()).Single());
            }
        }
    }

    public interface IBox<out T> { T Content { get; } }

    public interface ICat { bool IsAlive { get; } }

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

    public class Glue : IConfiguration
    {
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            yield return container.Bind<IBox<TT>>().To<CardboardBox<TT>>();
            yield return container.Bind<ICat>().To<ShroedingersCat>();
        }
    }
}
