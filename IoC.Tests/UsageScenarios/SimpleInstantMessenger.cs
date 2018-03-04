namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Core;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class SimpleInstantMessenger
    {
        [Fact]
        // $visible=true
        // $group=10
        // $priority=00
        // $description=Instant Messenger
        // {
        public void Run()
        {
            var observer = new Mock<IObserver<IMessage>>();

            // Initial message id
            var id = 33;
            Func<int> generator = () => id++;

            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<int>().Tag("IdGenerator").To(ctx => generator()))
            using (container.Bind(typeof(IInstantMessenger<>)).To(typeof(InstantMessenger<>)))
            using (container.Bind<IMessage>().To<Message>(ctx => new Message(ctx.Container.Inject<int>("IdGenerator"), (string)ctx.Args[0], (string)ctx.Args[1])))
            {
                var instantMessenger = container.Resolve<IInstantMessenger<IMessage>>();
                using (instantMessenger.Subscribe(observer.Object))
                {
                    for (var i = 0; i < 10; i++)
                    {
                        instantMessenger.SendMessage("John", "Hello");
                    }
                }
            }

            observer.Verify(i => i.OnNext(It.Is<IMessage>(message => message.Id >= 33 && message.Address == "John" && message.Text == "Hello")), Times.Exactly(10));
        }

        public interface IInstantMessenger<out T>: IObservable<T>
        {
            void SendMessage(string address, string text);
        }

        public interface IMessage
        {
            int Id { get; }

            string Address { get; }

            string Text { get; }
        }

        public class Message: IMessage
        {
            public Message(int id, [NotNull] string address, [NotNull] string text)
            {
                Id = id;
                Address = address ?? throw new ArgumentNullException(nameof(address));
                Text = text ?? throw new ArgumentNullException(nameof(text));
            }

            public int Id { get; }

            public string Address { get; }

            public string Text { get; }
        }

        public class InstantMessenger<T> : IInstantMessenger<T>
        {
            private readonly Func<string, string, T> _createMessage;
            private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

            public InstantMessenger(Func<string, string, T> createMessage)
            {
                _createMessage = createMessage;
            }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                _observers.Add(observer);
                return Disposable.Create(() => _observers.Remove(observer));
            }

            public void SendMessage(string address, string text)
            {
                _observers.ForEach(observer => observer.OnNext(_createMessage(address, text)));
            }
        }
        // }
    }
}
