// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ParameterTypeCanBeEnumerable.Local
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable ArrangeTypeMemberModifiers
#pragma warning disable 618
namespace ShroedingersCat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using IoC;
    using static System.Console;

    class Program
    {
        public Program(
            ICat cat,
            IBox<ICat> box,
            IBox<IBox<ICat>> bigBox,
            Func<IBox<ICat>> func,
            Task<IBox<ICat>> task,
            Tuple<IBox<ICat>, ICat, IBox<IBox<ICat>>> tuple,
            Lazy<IBox<ICat>> lazy,
            IEnumerable<IBox<ICat>> enumerable,
#if NETCOREAPP3_0
            IAsyncEnumerable<IBox<ICat>> asyncEnumerable,      
#endif
            IBox<ICat>[] array,
            IList<IBox<ICat>> list,
            ISet<IBox<ICat>> set,
            IObservable<IBox<ICat>> observable,
            IBox<Lazy<Func<IEnumerable<IBox<ICat>>>>> complex,
            ThreadLocal<IBox<ICat>> threadLocal,
            ValueTask<IBox<ICat>> valueTask,
            (IBox<ICat> box, ICat cat, IBox<IBox<ICat>> bigBox) valueTuple)
        {
            WriteLine(cat);
            WriteLine(box);
            WriteLine(bigBox);
            WriteLine("{0} from func", func());
            WriteLine("Tuple of {0}", tuple);
            WriteLine("Lazy of {0}", lazy.Value);
            WriteLine("Enumeration {0}", enumerable.Single());
            WriteLine("Array of {0}", array.Single());
            WriteLine("List of {0}", list.Single());
            WriteLine("Set of {0}", set.Single());
            WriteLine("Observable of {0}", observable.Single());
            WriteLine("Complex {0}", complex.Content.Value().Single());
            WriteLine("Thread local {0}", threadLocal.Value);
            WriteLine("Value tuple of {0}", valueTuple);

            async Task Async()
            {
                WriteLine("{0} from task", await task);
                WriteLine("{0} from value task", await valueTask);
#if NETCOREAPP3_0
                await foreach (var element in asyncEnumerable)
                {
                    WriteLine("Async Enumeration {0}", element);
                }                 
#endif
            }

            Async().Wait();
        }

        static void Main()
        {
            using (var container = Container.Create().Using<Glue>())
            {
                container.BuildUp<Program>();
            }
        }
    }

    public interface IBox<out T> { T Content { get; } }

    public interface ICat { State State { get; } }

    public enum State { Alive, Dead }

    class CardboardBox<T> : IBox<T>
    {
        public CardboardBox(T content) => Content = content;

        public T Content { get; }

        public override string ToString() => $"[{Content}]";
    }

    class ShroedingersCat : ICat
    {
        public ShroedingersCat(State state) => State = state;

        public State State { get; }

        public override string ToString() => $"{State} cat";
    }

    public class Glue : IConfiguration
    {
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            yield return container.Bind<IBox<TT>>().To<CardboardBox<TT>>();
            yield return container.Bind<ICat>().To<ShroedingersCat>();

            yield return container.Bind<Random>().As(Lifetime.Singleton).To<Random>();
            yield return container.Bind<State>().To(ctx => (State)ctx.Container.Resolve<Random>().Next(2));
        }
    }
}
