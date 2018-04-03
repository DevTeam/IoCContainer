namespace UwpApp.Models
{
    using System;
    using System.Collections.Generic;
    using Windows.UI.Xaml;

    internal class Timer : ITimer, IDisposable
    {
        private readonly DispatcherTimer  _timer;
        private readonly List<IObserver<Tick>> _observers = new List<IObserver<Tick>>();

        public Timer(TimeSpan period)
        {
            _timer = new DispatcherTimer {Interval = period};
            _timer.Tick += Tick;
            _timer.Start();
        }

        public IDisposable Subscribe(IObserver<Tick> observer)
        {
            _observers.Add(observer);
            return new Token(() => _observers.Remove(observer));
        }

        public void Dispose() => _timer.Stop();

        private void Tick(object state, object o) => _observers.ForEach(i => i.OnNext(Models.Tick.Shared));

        private class Token: IDisposable
        {
            private readonly Action _action;

            public Token(Action action) => _action = action ?? throw new ArgumentNullException(nameof(action));

            public void Dispose() => _action();
        }
    }
}