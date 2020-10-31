// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedVariable
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using Core;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class SimpleInstantMessenger
    {
        [Fact]
        // $visible=true
        // $tag=9 Samples
        // $priority=10
        // $description=Instant Messenger sample
        // {
        public void Run()
        {
            var observer = new Mock<IObserver<IMessage>>();

            // Create a container
            using var container = Container.Create().Using<InstantMessengerConfig>();

            // Resolve messenger
            var instantMessenger = container.Resolve<IInstantMessenger<IMessage>>();
            using var subscription = instantMessenger.Subscribe(observer.Object);

            // Send messages
            instantMessenger.SendMessage("Nik", "John", "Hello, John");
            instantMessenger.SendMessage("John", "Nik", "Hello, Nik!");

            // Verify messages
            observer.Verify(i => i.OnNext(It.Is<IMessage>(message => message.Id == 34 && message.Text == "Hello, John")));
            observer.Verify(i => i.OnNext(It.Is<IMessage>(message => message.Id == 35 && message.Text == "Hello, Nik!")));
        }

        public class InstantMessengerConfig: IConfiguration
        {
            public IEnumerable<IToken> Apply(IMutableContainer container)
            {
                // Let's suppose that the initial message ID is 33
                var id = 33;

                yield return container
                    // id generator
                    .Bind<int>().To(ctx => Interlocked.Increment(ref id))
                    // abstract messenger
                    .Bind(typeof(IInstantMessenger<>)).To(typeof(InstantMessenger<>))
                    // abstract subject
                    .Bind<ISubject<TT>>().To<Subject<TT>>()
                    // message factory
                    .Bind<IMessageFactory<IMessage>>().To<Message>();
            }
        }

        public interface IInstantMessenger<out T>: IObservable<T>
        {
            void SendMessage(string addressFrom, string addressTo, string text);
        }

        public interface IMessage
        {
            int Id { get; }

            string AddressFrom { get; }

            string AddressTo { get; }

            string Text { get; }
        }

        public interface IMessageFactory<out T>
        {
            T Create([NotNull] string addressFrom, [NotNull] string addressTo, [NotNull] string text);
        }

        public class Message: IMessage, IMessageFactory<IMessage>
        {
            private readonly Func<int> _idFactory;

            // Injected constructor
            public Message(Func<int> idFactory) => _idFactory = idFactory;

            private Message(int id, [NotNull] string addressFrom, [NotNull] string addressTo, [NotNull] string text)
            {
                Id = id;
                AddressFrom = addressFrom ?? throw new ArgumentNullException(nameof(addressFrom));
                AddressTo = addressTo ?? throw new ArgumentNullException(nameof(addressTo));
                Text = text ?? throw new ArgumentNullException(nameof(text));
            }

            public int Id { get; }

            public string AddressFrom { get; }

            public string AddressTo { get; }

            public string Text { get; }

            public IMessage Create(string addressFrom, string addressTo, string text) => new Message(_idFactory(), addressFrom, addressTo, text);
        }

        public class InstantMessenger<T> : IInstantMessenger<T>
        {
            private readonly IMessageFactory<T> _messageFactory;
            private readonly ISubject<T> _messages;

            internal InstantMessenger(IMessageFactory<T> messageFactory, ISubject<T> subject)
            {
                _messageFactory = messageFactory;
                _messages = subject;
            }

            public IDisposable Subscribe(IObserver<T> observer) => _messages.Subscribe(observer);

            public void SendMessage(string addressFrom, string addressTo, string text) => _messages.OnNext(_messageFactory.Create(addressFrom, addressTo, text));
        }
        // }
    }
}
