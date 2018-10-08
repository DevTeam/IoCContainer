﻿// ReSharper disable UnusedMemberInSuper.Global
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
            ICat cat,
            IBox<ICat> box,
            IBox<IBox<ICat>> bigBox,
            Func<IBox<ICat>> func,
            Task<IBox<ICat>> task,
            Tuple<IBox<ICat>, ICat, IBox<IBox<ICat>>> tuple,
            Lazy<IBox<ICat>> lazy,
            IEnumerable<IBox<ICat>> enumerable,
            IBox<ICat>[] array,
            IList<IBox<ICat>> list,
            ISet<IBox<ICat>> set,
            IObservable<IBox<ICat>> observable,
            IBox<Lazy<Func<IEnumerable<IBox<ICat>>>>> complex,
            ValueTask<IBox<ICat>> valueTask,
            (IBox<ICat> box, ICat cat, IBox<IBox<ICat>> nestedBox) valueTuple)
        {
            WaitAll(task, valueTask.AsTask());

            WriteLine(cat);
            WriteLine(box);
            WriteLine("Big {0}", bigBox);
            WriteLine("{0} from func", func());
            WriteLine("Tuple of {0}", tuple);
            WriteLine("Lazy of {0}", lazy.Value);
            WriteLine("Enumeration of {0}", enumerable.Single());
            WriteLine("Array of {0}", array.Single());
            WriteLine("List of {0}", list.Single());
            WriteLine("Set of {0}", set.Single());
            WriteLine("Observable of {0}", observable.Single());
            WriteLine("Complex {0}", complex.Content.Value().Single());
            WriteLine("{0} from task", task.Result);
            WriteLine("{0} from value task", valueTask.Result);
            WriteLine("Value tuple of {0}", valueTuple);
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

        public override string ToString() { return $"Box[{Content}]"; }
    }

    class ShroedingersCat : ICat
    {
        public bool IsAlive => new Random().Next(2) == 1;

        public override string ToString() { return $"{(IsAlive ? "Live" : "Dead")} cat"; }
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
