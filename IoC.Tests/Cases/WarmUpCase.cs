namespace IoC.Tests.Cases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using Xunit;

    public class WarmUpCase
    {
        [Fact]
        public void Run()
        {
            var console = new Mock<IConsole>();
            var timer = new TestTimer();
            
            using (var container = Container.Create())
            using (container.Bind<IConsole>().Lifetime(Lifetime.Singletone).To(() => console.Object))
            using (container.Bind<ITimer>().Lifetime(Lifetime.Singletone).To(() => timer))
            using (container.Bind<AlarmClock, IAlarmClock, IActiveObject>().Lifetime(Lifetime.Singletone).To())
            {
                foreach (var activeObject in container.Get<IEnumerable<IActiveObject>>())
                {
                    activeObject.WarmUp();
                }

                container.Get<IAlarmClock>().AlarmTime = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);

                timer.SendTime(new DateTimeOffset(2017, 12, 31, 23, 59, 57, TimeSpan.Zero));
                timer.SendTime(new DateTimeOffset(2017, 12, 31, 23, 59, 58, TimeSpan.Zero));
                timer.SendTime(new DateTimeOffset(2017, 12, 31, 23, 59, 59, TimeSpan.Zero));
                timer.SendTime(new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero));
                timer.SendTime(new DateTimeOffset(2018, 1, 1, 0, 0, 1, TimeSpan.Zero));
                timer.SendTime(new DateTimeOffset(2018, 1, 1, 0, 0, 2, TimeSpan.Zero));
            }

            console.Verify(i => i.WriteLine("Happy new year!!!"));
        }

        public interface IActiveObject
        {
            void WarmUp();
        }

        public interface ITimer: IObservable<DateTimeOffset>
        {
        }

        public interface IConsole
        {
            void WriteLine(string test);
        }

        public interface IAlarmClock
        {
            DateTimeOffset AlarmTime { set; }
        }

        public class TestTimer: ITimer
        {
            private readonly List<IObserver<DateTimeOffset>> _observers = new List<IObserver<DateTimeOffset>>();

            public IDisposable Subscribe(IObserver<DateTimeOffset> observer)
            {
                _observers.Add(observer);
                return Disposable.Create(() => _observers.Remove(observer));
            }

            public void SendTime(DateTimeOffset time)
            {
                _observers.ToList().ForEach(observer => observer.OnNext(time));
            }
        }

        public class AlarmClock: IAlarmClock, IActiveObject, IObserver<DateTimeOffset>
        {
            private readonly IConsole _console;
            private readonly ITimer _timer;
            private IDisposable _timerSubscription;
            private DateTimeOffset? _alarmTime;

            public AlarmClock(IConsole console, ITimer timer)
            {
                _console = console;
                _timer = timer;
            }

            public DateTimeOffset AlarmTime
            {
                set => _alarmTime = value;
            }

            public void WarmUp()
            {
                _timerSubscription = _timer.Subscribe(this);
            }

            public void OnNext(DateTimeOffset value)
            {
                if (!_alarmTime.HasValue) return;
                if (_alarmTime.Value > value) return;
                _alarmTime = null;
                _console.WriteLine("Happy new year!!!");
                _timerSubscription?.Dispose();
            }

            public void OnError(Exception error) { }

            public void OnCompleted() { }
        }
    }
}
