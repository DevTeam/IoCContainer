namespace UwpApp
{
    using System;
    using Windows.ApplicationModel.Core;
    using Clock.ViewModels;
    using static Windows.UI.Core.CoreDispatcherPriority;

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Dispatcher: IDispatcher
    {
        public void Dispatch(Action action) => 
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Normal, () => action()).AsTask().Wait();
    }
}
