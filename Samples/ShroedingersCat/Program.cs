// ReSharper disable ArrangeTypeModifiers
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo
namespace ShroedingersCat
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using static System.Console;

    class Program
    {
        // Entry point
        public static void Main()
        {
            // Creates an Inversion of Control container
            using var container = Container.Create().Using<Glue>();

            // This is the Composition Root. It gets a cardboard box in the same way as the following expression:
            // var box = new CardboardBox<ICat>(new ShroedingersCat(new Lazy<State>(() => (State)indeterminacy.Next(2))));
            var box = container.Resolve<IBox<ICat>>();
            // Checks the cat's state
            WriteLine(box.Content);
        }
    }

    public interface IBox<out T> { T Content { get; } }

    public interface ICat { State State { get; } }

    public enum State { Alive, Dead }

    class CardboardBox<T> : IBox<T>
    {
        public CardboardBox(T content) => Content = content;

        public T Content { get; }
    }

    class ShroedingersCat : ICat
    {
        // Represents the superposition of the states
        private readonly Lazy<State> _superposition;

        public ShroedingersCat(Lazy<State> superposition) => _superposition = superposition;

        // The decoherence of the superposition at the time of observation via an irreversible process
        public State State => _superposition.Value;

        public override string ToString() => $"{State} cat";
    }

    public class Glue : IConfiguration
    {
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            // Returns single token for 2 bindings
            yield return container
                // Represents a cardboard box with any content
                .Bind<IBox<TT>>().To<CardboardBox<TT>>()
                // Represents schrodinger's cat
                .Bind<ICat>().To<ShroedingersCat>();

            // Models a random subatomic event that may or may not occur
            var indeterminacy = new Random();

            // Represents a quantum superposition of 2 states: Alive or Dead
            yield return container.Bind<State>().To(ctx => (State)indeterminacy.Next(2));
        }
    }
}
