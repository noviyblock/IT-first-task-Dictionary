using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BasicMvvmSample.ViewModels
{
    public class SimpleViewModel : INotifyPropertyChanged
    {
        private workList<int> myList = new workList<int>();

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string? _ValueData;

        public string? ValueData
        {
            get => _ValueData;
            set
            {
                if (_ValueData != value)
                {
                    _ValueData = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(Greeting));
                }
            }
        }

        public string Greeting => "Show List: " + myList.Print();

        public ICommand addButtonClickCommand { get; }
        public ICommand delButtonClickCommand { get; }

        public SimpleViewModel()
        {
            addButtonClickCommand = new RelayCommand(addOnButtonClick);
            delButtonClickCommand = new RelayCommand(delOnButtonClick);
        }

        private void addOnButtonClick()
        {
            if (int.TryParse(_ValueData, out int x))
            {
                myList.Add(x);
                RaisePropertyChanged(nameof(Greeting));
            }
        }

        private void delOnButtonClick()
        {
            if (int.TryParse(_ValueData, out int x))
            {
                myList.Remove(x);
                RaisePropertyChanged(nameof(Greeting));
            }
        }
    }

    public class workList<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item) => items.Add(item);
        public void Remove(T item) => items.Remove(item);
        public string Print() => string.Join(", ", items);
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute) => _execute = execute;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute();
        public event EventHandler? CanExecuteChanged;
    }
}
