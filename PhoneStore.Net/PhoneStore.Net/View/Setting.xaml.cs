using Microsoft.Win32;
using OpenTK.Graphics.OpenGL;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        HomePage homePage { get; set; }
        public Setting(HomePage hp)
        {
            InitializeComponent();
            homePage = hp;
            this.FullNameTextBox.Text = HomePage.userSetting.Name;
            this.GenderComboBox.Text = HomePage.userSetting.Gender;
            this.DateBox.SelectedDate = HomePage.userSetting.DOB;
            this.DateBox.Text = HomePage.userSetting.DOB.ToString();
            this.PhoneNumberTextBox.Text = HomePage.userSetting.PhoneNumber;
            this.AddressTextBox.Text = HomePage.userSetting.Address;
            this.EmailTextBox.Text = HomePage.userSetting.Email;
            this.UserImage.Fill = new ImageBrush(HomePage.userSetting.Image);
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

            HomePage.userSetting.Name = this.FullNameTextBox.Text;
            HomePage.userSetting.Gender = this.GenderComboBox.Text;
            HomePage.userSetting.DOB = this.DateBox.SelectedDate;
            HomePage.userSetting.PhoneNumber = this.PhoneNumberTextBox.Text;
            HomePage.userSetting.Address = this.AddressTextBox.Text;
            HomePage.userSetting.Email = this.EmailTextBox.Text;

            this.Close();
            homePage.RefreshDisplay();
        }

        // Event cho nút "X"
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                this.UserImage.Fill = new ImageBrush(image);
                HomePage.userSetting.Image = image;
            }
            
        }
    }
}
