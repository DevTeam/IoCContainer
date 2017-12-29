namespace IoC.Tests.Cases
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public class MultipleResolvingCase
    {
        [Fact]
        public void Run()
        {
            var observer = new Mock<IObserver<IMessage>>();

            var id = 33;
            using (var container = Container.Create())
            using (container.Bind<int>().Tag("IdGenerator").To(() => id++))
            using (container.Bind(typeof(IInstanceMessanger<>)).To(typeof(InstanceMessanger<>)))
            using (container.Bind<IMessage>().To<Message>(Has.Arg("address", 0), Has.Arg("text", 1), Has.Ref("id", "IdGenerator")))
            {
                var instanceMessanger = container.Get<IInstanceMessanger<IMessage>>();
                using (instanceMessanger.Subscribe(observer.Object))
                {
                    for (var i = 0; i < 10; i++)
                    {
                        instanceMessanger.SendMessage("John", "Hello");
                    }
                }
            }

            observer.Verify(i => i.OnNext(It.Is<IMessage>(message => message.Id >= 33 && message.Address == "John" && message.Text == "Hello")), Times.Exactly(10));
        }

        public interface IInstanceMessanger<out T>: IObservable<T>
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

        public class InstanceMessanger<T> : IInstanceMessanger<T>
        {
            private readonly Func<string, string, T> _createMessage;
            private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

            public InstanceMessanger(Func<string, string, T> createMessage)
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
    }
}
