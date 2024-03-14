using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
using System.Data.SQLite;
using System.Windows;

namespace PhoneStore.Net.View
{
    public partial class Dang_Nhap : Window
    {
        public Dang_Nhap()
        {
            InitializeComponent();
        }
        
        private void DangNhapClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            try
            {
                NGUOIDUNG user = DBConnect.DataProvider.Instance.checkUser(username, password);
                if (user != null)
                {
                    MainWindow main = new MainWindow(user);
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai username hoặc password");
                }
            }
            catch (SQLiteException error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
