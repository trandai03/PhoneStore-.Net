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
using System.Data;
namespace PhoneStore.Net
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {    
        public static NGUOIDUNG user;
        public static string AVAType;
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        

        Boolean QTV;
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
        }
        public MainWindow(NGUOIDUNG u)
        {
            InitializeComponent();
            user = u;
            
            selectUser();
            
            TenDangNhap.Text = string.Join(" ", user.TENND.Split().Reverse().Take(2).Reverse());
            
            if(QTV)
            {
                chucVu.Text = "Quản lý ";
                
            }
            else
            {
                chucVu.Text = "Nhân viên ";
                quanlyButton.Visibility = Visibility.Collapsed;
            }
        }
        public void selectUser()
        {
            //string sql = "SELECT QTV from NGUOIDUNGs WHEre MAND = @keyword ;";
            string sql = "SELECT MAND, QTV from NGUOIDUNGs";
            ConnectToDatabase();
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
            string keyword = string.Format("{0}", user.MAND);
            Console.WriteLine(keyword);
            cmd.Parameters.AddWithValue("@keyword", keyword);
            
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(reader.GetString(0)== user.MAND)
                {
                     QTV = reader.GetBoolean(1);
                }
                
            }
            Console.WriteLine(QTV.ToString());
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
            this.WindowState = WindowState.Minimized;
        }

        private void Dang_Xuat(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) {
                
                Dang_Nhap d = new Dang_Nhap();
                d.Show();
                this.Close();             
            }

        }
    }
}
