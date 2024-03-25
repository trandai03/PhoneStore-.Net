using Microsoft.Win32;
using OpenTK.Graphics.OpenGL;
using PhoneStore.Net.Controller;
using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
using System;
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
        
        public Setting()
        {
            InitializeComponent();
            this.FullNameTextBox.Text = MainWindow.user.TENND;
            this.GenderComboBox.Text = MainWindow.user.GIOITINH;
            this.DateBox.Text = MainWindow.user.NGSINH.ToString();
            this.PhoneNumberTextBox.Text = MainWindow.user.SDT;
            this.AddressTextBox.Text = MainWindow.user.DIACHI;
            this.EmailTextBox.Text = MainWindow.user.MAIL;
            string tmp = DBConnect.DataProvider.Instance.main_user_ava(MainWindow.user.TENND);
            this.UserImage.Fill = new ImageBrush(new BitmapImage(new Uri(_localLink + tmp)));
            
            this.UsernameTextBox.Text = MainWindow.user.USERNAME;
            this.PasswordTextBox.Text = MainWindow.user.PASS;
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

            DBConnect.DataProvider.Instance.updateUser(MainWindow.user);

            MessageBox.Show("Cập nhật thành công");
        }

        // Event cho nút "Thay đổi ảnh"
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string selectedFileName = openFileDialog.FileName;

                BitmapImage image = new BitmapImage();
                try
                {
                    image.BeginInit();
                    image.UriSource = new Uri(selectedFileName);
                    image.EndInit();
                }
                catch
                {
                    MessageBox.Show($"Error: {selectedFileName}");
                    return;
                }
                //this.UserImage.Fill = new ImageBrush(image);
                //Console.WriteLine(image);
                //MainWindow.user.AVA = image;
            }
            
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
