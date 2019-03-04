namespace SampleModels.VewModels
{
    using System;
    using Models;

    internal class ClockViewModel : ViewModel, IClockViewModel, IDisposable, IObserver<Tick>
    {
        private readonly IClock _clock;
        private readonly IDisposable _timerToken;

        public ClockViewModel(IUIDispatcher uiDispatcher, IClock clock, ITimer timer)
            :base(uiDispatcher)
        {
            if (timer == null) throw new ArgumentNullException(nameof(timer));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
            _timerToken = timer.Subscribe(this);
        }

        public string Time => _clock.Now.ToString("T");

        public string Date => _clock.Now.ToString("d");

        void IObserver<Tick>.OnNext(Tick value)
        {
            OnPropertyChanged(nameof(Time));
            OnPropertyChanged(nameof(Date));
        }

        void IObserver<Tick>.OnError(Exception error) { }

        void IObserver<Tick>.OnCompleted() { }

        void IDisposable.Dispose() => _timerToken.Dispose();
    }
}