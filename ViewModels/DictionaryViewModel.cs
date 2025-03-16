using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BasicMvvmSample.ViewModels
{
    public class DictionaryViewModel : INotifyPropertyChanged
    {
        private readonly Models.Dictionary<string, string> _dictionary = new();

        private string _keyInput = string.Empty;
        private string _valueInput = string.Empty;
        private string _dictionaryOutput = string.Empty;
        private string _searchResult = string.Empty;

        public string KeyInput
        {
            get => _keyInput;
            set
            {
                _keyInput = value;
                OnPropertyChanged();
            }
        }

        public string ValueInput
        {
            get => _valueInput;
            set
            {
                _valueInput = value;
                OnPropertyChanged();
            }
        }

        public string DictionaryOutput
        {
            get => _dictionaryOutput;
            private set
            {
                _dictionaryOutput = value;
                OnPropertyChanged();
            }
        }

        public string SearchResult
        {
            get => _searchResult;
            private set
            {
                _searchResult = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand SearchCommand { get; }

        public DictionaryViewModel()
        {
            AddCommand = new RelayCommand(AddKeyValuePair);
            RemoveCommand = new RelayCommand(RemoveKey);
            ClearCommand = new RelayCommand(ClearDictionary);
            SearchCommand = new RelayCommand(SearchKey);
            UpdateOutput();
        }

        private void AddKeyValuePair()
        {
            try
            {
                _dictionary.Add(KeyInput, ValueInput);
                UpdateOutput();
            }
            catch (Exception ex)
            {
                DictionaryOutput = $"Error: {ex.Message}";
            }
            
            UpdateOutput();
        }

        private void RemoveKey()
        {
            if (_dictionary.Remove(KeyInput))
                UpdateOutput();
            else
                DictionaryOutput = "Key not found!";
        }

        private void ClearDictionary()
        {
            _dictionary.Clear();
            UpdateOutput();
        }

        private void UpdateOutput()
        {
            DictionaryOutput = "Dictionary:\n" + string.Join("\n", _dictionary.KeyValuePairs);
        }
        
        private void SearchKey()
        {
            if (_dictionary.TryFind(KeyInput, out var result))
            {
                SearchResult = $"Found: {result}";
            }
            else
            {
                SearchResult = "Key not found!";
            }
            UpdateOutput();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
