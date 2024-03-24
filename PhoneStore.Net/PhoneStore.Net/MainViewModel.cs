using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PhoneStore.Net.View;
using System.Windows;

namespace PhoneStore.Net
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;
        
        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _canExecute = canExecute;
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));

       
        public ICommand MinimizeLogin { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand GetIdTab { get; set; }
        public ICommand MoveWindow { get; set; }
        public ICommand SwitchTab { get; set; }
        public string Name;
        
        private string _Ava;
        public string Ava { get => _Ava; set { _Ava = value; OnPropertyChanged(); } }
        public MainViewModel()
        {
    
            MinimizeLogin = new RelayCommand<MainWindow>((p) => true, (p) => Minimize(p));
            GetIdTab = new RelayCommand<Button>((p) => true, (p) => Name = p.Uid);
            MoveWindow = new RelayCommand<MainWindow>((p) => true, (p) => moveWindow(p));
            SwitchTab = new RelayCommand<MainWindow>((p) => true, (p) => switchtab(p));
        }

        public void switchtab(MainWindow p)
        {
            int index = int.Parse(Name);
            switch (index)
            {
                case 0:
                    {                    
                        p.Main.NavigationService.Navigate(new HomePage());
                        break;
                    }
                case 1:
                    { 
                        p.Main.NavigationService.Navigate(new Don_Hang());
                        break;
                    }
                case 2:
                    {                
                        p.Main.NavigationService.Navigate(new Detail_product());
                        break;
                    }
                case 3:
                    {
                        p.Main.NavigationService.Navigate(new QLSP());
                        break;
                    }
                case 4:
                    {  
                        p.Main.NavigationService.Navigate(new QLNH());
                        break;
                    }
                case 5:
                    {
                        p.Main.NavigationService.Navigate(new ThongKe());
                        break;
                    }
                case 6:
                    {
                        p.Main.NavigationService.Navigate(new QLNV());
                        break;
                    }
                case 7:
                    {
                        p.Main.NavigationService.Navigate(new Setting());
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public void moveWindow(MainWindow p)
        {
            p.DragMove();
        }
     

        public void Minimize(MainWindow p)
        {
            p.WindowState = WindowState.Minimized;
        }
    }
}