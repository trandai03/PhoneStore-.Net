using Microsoft.Win32;
using OpenTK.Graphics.OpenGL;
using PhoneStore.Net.Controller;
using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : Page
    {
        public string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        public string tmp;
        string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public Setting()
        {
            InitializeComponent();
            this.FullNameTextBox.Text = MainWindow.user.TENND;
            this.GenderComboBox.Text = MainWindow.user.GIOITINH;
            this.DateBox.Text = MainWindow.user.NGSINH.ToString();
            this.PhoneNumberTextBox.Text = MainWindow.user.SDT;
            this.AddressTextBox.Text = MainWindow.user.DIACHI;
            this.EmailTextBox.Text = MainWindow.user.MAIL;
            tmp = DBConnect.DataProvider.Instance.main_user_ava(MainWindow.user.TENND);
            this.UserImage.Fill = new ImageBrush(new BitmapImage(new Uri(_localLink + tmp)));
            
            this.UsernameTextBox.Text = MainWindow.user.USERNAME;
            this.PasswordTextBox.Text = MainWindow.user.PASS;
        }

        public bool updateUser(NGUOIDUNG newU)
        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            string fileName = Path.GetFileName(tmp);
            string link_img_temp = "/Resource/avatar/" + fileName;
            string USERQUERYSTRING = @"UPDATE NGUOIDUNGs SET TENND = $ten , GIOITINH = $gioitinh , NGSINH = $ngsinh , SDT = $sdt , DIACHI = $diachi , MAIL = $mail, AVA = @ava WHERE MAND = $mand";
            var command = _con.CreateCommand();
            command.CommandText = USERQUERYSTRING;
            command.Parameters.AddWithValue("$ten", newU.TENND);
            command.Parameters.AddWithValue("$gioitinh", newU.GIOITINH);
            command.Parameters.AddWithValue("$ngsinh", newU.NGSINH);
            command.Parameters.AddWithValue("$sdt", newU.SDT);
            command.Parameters.AddWithValue("$diachi", newU.DIACHI);
            command.Parameters.AddWithValue("$mail", newU.MAIL);
            command.Parameters.AddWithValue("$mand", newU.MAND);
            command.Parameters.AddWithValue("@ava", link_img_temp);
            var _ = command.ExecuteNonQuery();
            _con.Close();

            return true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.FullNameTextBox.Text))
            {
                MessageBox.Show("Hãy điền họ tên");
                return;
            }

            if (string.IsNullOrEmpty(this.GenderComboBox.Text))
            {
                MessageBox.Show("Hãy chọn giới tính");
                return;
            }

            if (string.IsNullOrEmpty(this.DateBox.Text))
            {
                MessageBox.Show("Hãy chọn ngày sinh");
                return;
            }

            if (string.IsNullOrEmpty(this.PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Hãy điền số điện thoại");
                return;
            }

            if (string.IsNullOrEmpty(this.AddressTextBox.Text))
            {
                MessageBox.Show("Hãy điền địa chỉ");
                return;
            }

            if (string.IsNullOrEmpty(this.EmailTextBox.Text))
            {
                MessageBox.Show("Hãy điền email");
                return;
            }
            else if (!this.EmailTextBox.Text.Contains("@"))
            {
                MessageBox.Show("Sai định dạng email");
                return;
            }

            MainWindow.user.TENND = this.FullNameTextBox.Text;
            MainWindow.user.GIOITINH = this.GenderComboBox.Text;
            MainWindow.user.NGSINH = this.DateBox.SelectedDate;
            MainWindow.user.SDT = this.PhoneNumberTextBox.Text;
            MainWindow.user.DIACHI = this.AddressTextBox.Text;
            MainWindow.user.MAIL = this.EmailTextBox.Text;

            updateUser(MainWindow.user);

            MessageBox.Show("Cập nhật thành công");
        }

        // Event cho nút "Thay đổi ảnh"
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";

            if (open.ShowDialog() == true)
            {
                if (open.FileName != "")
                    tmp = open.FileName;
            };
            Uri fileUri = new Uri(tmp);
            UserImage.Fill = new ImageBrush(new BitmapImage(new Uri(tmp)));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.UsernameTextBox.Text))
            {
                MessageBox.Show("Hãy điền tên đăng nhập");
                return;
            }

            if (string.IsNullOrEmpty(this.PasswordTextBox.Text))
            {
                MessageBox.Show("Hãy điền mật khẩu");
                return;
            }

            MainWindow.user.USERNAME = this.UsernameTextBox.Text;
            MainWindow.user.PASS = this.PasswordTextBox.Text;

            DBConnect.DataProvider.Instance.changeUserLogin(MainWindow.user);
            MessageBox.Show("Cập nhật thành công");
        }
    }
}
