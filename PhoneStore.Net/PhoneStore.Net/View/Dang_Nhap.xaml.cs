using PhoneStore.Net.DBClass;
using System.Data.SQLite;
using System.Windows;

namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for Dang_Nhap.xaml
    /// </summary>
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
                if (DBConnect.DataProvider.Instance.checkUser(username, password))
                {
                    MainWindow main = new MainWindow();
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
