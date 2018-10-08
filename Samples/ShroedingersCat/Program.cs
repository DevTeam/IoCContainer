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
    using System.Threading.Tasks;
    using IoC;
    using static System.Console;
    using static System.Threading.Tasks.Task;

    class Program
    {
        public Program(
            IBox<ICat> box,
            IBox<IBox<ICat>> nestedBox,
            Func<IBox<ICat>> func,
            Task<IBox<ICat>> task,
            ValueTask<IBox<ICat>> valueTask,
            Tuple<IBox<ICat>, ICat, IBox<IBox<ICat>>> tuple,
            (IBox<ICat> box, ICat cat, IBox<IBox<ICat>> nestedBox) valueTuple,
            Lazy<IBox<ICat>> lazy,
            IEnumerable<IBox<ICat>> enumerable,
            IBox<ICat>[] array,
            IList<IBox<ICat>> list,
            ISet<IBox<ICat>> set,
            IObservable<IBox<ICat>> observable,
            IBox<Lazy<Func<IEnumerable<IBox<ICat>>>>> complex)
        {
            WaitAll(task, valueTask.AsTask());

            WriteLine("Box {0}", box);
            WriteLine("Nested box {0}", nestedBox);
            WriteLine("Func {0}", func());
            WriteLine("Task {0}" , task.Result);
            WriteLine("Value task {0}", valueTask.Result);
            WriteLine("Tuple {0}", tuple);
            WriteLine("Value tuple {0}", valueTuple);
            WriteLine("Lazy {0}", lazy.Value);
            WriteLine("Enumerable {0}", enumerable.Single());
            WriteLine("Array {0}", array.Single());
            WriteLine("List {0}", list.Single());
            WriteLine("Set {0}", set.Single());
            WriteLine("Observable {0}", observable.Single());
            WriteLine("Complex {0}", complex.Content.Value().Single());
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
