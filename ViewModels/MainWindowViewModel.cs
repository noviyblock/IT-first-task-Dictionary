namespace BasicMvvmSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public DictionaryViewModel DictionaryViewModel { get; } = new DictionaryViewModel();
    }
}

