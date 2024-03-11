using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using PhoneStore.Net.View;
using PhoneStore.Net.Model;
namespace PhoneStore.Net
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {    
        public static NGUOIDUNG user;
        public static string AVAType;
        public MainWindow(NGUOIDUNG u)
        {
            InitializeComponent();
            user = u;
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            this.Main.NavigationService.Navigate(new HomePage());
        }

        private void Don_Hang(object sender, RoutedEventArgs e)
        {
            this.Main.NavigationService.Navigate(new Don_Hang());
        }

        private void San_Pham(object sender, RoutedEventArgs e)
        {
            this.Main.NavigationService.Navigate(new QLSP());
        }

        private void Nhap_Hang(object sender, RoutedEventArgs e)
        {
            this.Main.NavigationService.Navigate(new QLNH());
        }

        private void Thong_Ke(object sender, RoutedEventArgs e)
        {
            this.Main.NavigationService.Navigate(new ThongKe());
        }

        private void Quan_Ly(object sender, RoutedEventArgs e)
        {
            this.Main.NavigationService.Navigate(new QLNV());
        }

        private void Cai_Dat(object sender, RoutedEventArgs e)
        {
            this.Main.NavigationService.Navigate(new Setting());
        }

        private void Close_Window(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Minimize_Window(object sender, MouseButtonEventArgs e)
        {
            this.WindowState.CompareTo(WindowState.Minimized);
        }

    }
}
