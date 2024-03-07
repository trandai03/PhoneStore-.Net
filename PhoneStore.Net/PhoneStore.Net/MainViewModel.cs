using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PhoneStore.Net
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!(object.Equals(field, newValue)))
            {
                field = (newValue);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
        public ICommand SwitchToHomePageCommand { get; private set; }
        public ICommand SwitchToOrdersPageCommand { get; private set; }
        public ICommand SwitchToProductsPageCommand { get; private set; }
        public MainViewModel()
        {
            SwitchToHomePageCommand = new RelayCommand(SwitchToHomePage);
            SwitchToOrdersPageCommand = new RelayCommand(SwitchToOrdersPage);
            SwitchToProductsPageCommand = new RelayCommand(SwitchToProductsPage);
     
        }

        private void SwitchToHomePage(object parameter)
        {
            if (parameter is MainWindow mainWindow)
            {
                mainWindow.Main.Source = new Uri("/view/HomePage.xaml", UriKind.Relative);
            }
        }

        private void SwitchToOrdersPage(object parameter)
        {
            if (parameter is MainWindow mainWindow)
            {
                mainWindow.Main.Source = new Uri("/view/Don_Hang.xaml", UriKind.Relative);
            }
        }

        private void SwitchToProductsPage(object parameter)
        {
            if (parameter is MainWindow mainWindow)
            {
                mainWindow.Main.Source = new Uri("/view/Detail_phone.xaml", UriKind.Relative);
            }
        }
    }
}