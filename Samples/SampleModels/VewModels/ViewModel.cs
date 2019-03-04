namespace SampleModels.VewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal class ViewModel: INotifyPropertyChanged
    {
        private readonly IUIDispatcher _uiDispatcher;
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel(IUIDispatcher uiDispatcher) => _uiDispatcher = uiDispatcher;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _uiDispatcher.Dispatch(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));            
        }
    }
}
