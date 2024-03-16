using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using PhoneStore.Net.View;
using PhoneStore.Net.DBClass;
using System.Data;
using PhoneStore.Net.Model;
using static PhoneStore.Net.DBClass.DBConnect;
using MaterialDesignThemes.Wpf;
using System.Windows.Interop;
namespace PhoneStore.Net.View
{
    public partial class Detail_NhanVien : Window
    {
        public string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        private string _linkimage;
        public string linkimage { get => _linkimage; set { _linkimage = value; } }
        public string MaNDValue { get; set; }
        public string TenNDValue {  get; set; }
        public DateTime NSValue {  get; set; }
        public string GTValue {  get; set; }
        public string SDTValue {  get; set; }
        public string DiaChiValue {  get; set; }
        public string UserValue {  get; set; }
        public string PassValue {  get; set; }
        public bool QTVValue { get; set; }
        public string MailValue {  get; set; }
        public string AvaValue {  get; set; }
        public string tmp {  get; set; }
        public Detail_NhanVien()
        {
            InitializeComponent();
            ConnectToDatabase();
        }
        public void UpdateData()
        {
            MaND.Text = MaNDValue;
            TenND.Text = TenNDValue;
            NS.SelectedDate = NSValue;
            GT.Text = GTValue;
            SDT.Text = SDTValue;
            DC.Text = DiaChiValue;
            User.Text = UserValue;
            Password.Text = PassValue;
            if(QTVValue == true)
            {
                QTV.Text = "Quản lý";
            }
            else
            {
                QTV.Text = "Nhân viên";
            }
            Mail.Text = MailValue;
            tmp = _localLink + AvaValue;
            Uri fileUri = new Uri(tmp);
            HinhAnh.ImageSource = new BitmapImage(fileUri);
        }
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn cập nhật thông tin người dùng ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                string query = "UPDATE NGUOIDUNGs SET TENND = @tennd, NGSINH = @ns, GIOITINH = @gioitinh, SDT = @sdt, DIACHI = @diachi, USERNAME = @user, PASS = @pass, QTV = @qtv, MAIL = @mail, AVA = @ava WHERE MAND = @mand";

                bool check = true;
                if (QTV.Text == "Quản lý")
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
                string fileName = Path.GetFileName(linkimage);
                string link_img_temp = "/Resource/avatar/" + fileName;
                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@tennd", TenND.Text);
                command.Parameters.AddWithValue("@ns", NS.SelectedDate);
                command.Parameters.AddWithValue("@gioitinh", GT.Text);
                command.Parameters.AddWithValue("@sdt", SDT.Text);
                command.Parameters.AddWithValue("@diachi", DC.Text);
                command.Parameters.AddWithValue("@user", User.Text);
                command.Parameters.AddWithValue("@pass", Password.Text);
                command.Parameters.AddWithValue("qtv", check);
                command.Parameters.AddWithValue("@mail", Mail.Text);
                command.Parameters.AddWithValue("@mand", MaND.Text);
                command.Parameters.AddWithValue("@ava", link_img_temp);
                command.ExecuteNonQuery();

                TenND.Text = ""; NS.SelectedDate = null; GT.Text = ""; SDT.Text = "";
                DC.Text = ""; User.Text = ""; Password.Text = ""; QTV.Text = "";
                Mail.Text = "";
                Uri fileUri = new Uri(_localLink + "/Resource/Image/addava.png");
                HinhAnh.ImageSource = new BitmapImage(fileUri);
                MessageBox.Show("Cập nhật người dùng thành công !", "THÔNG BÁO");
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa người dùng này  ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                string query = "DELETE FROM NGUOIDUNGs WHERE MAND = @mand";
                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@mand", MaND.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Xóa người dùng thành công !", "THÔNG BÁO");
                TenND.Text = ""; NS.SelectedDate = null; GT.Text = ""; SDT.Text = "";
                DC.Text = ""; User.Text = ""; Password.Text = ""; QTV.Text = "";
                Mail.Text = "";
            }
        }

        private void imagebtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";

            if (open.ShowDialog() == true)
            {
                if (open.FileName != "")
                    linkimage = open.FileName;
            };
            Uri fileUri = new Uri(linkimage);
            HinhAnh.ImageSource = new BitmapImage(fileUri);
        }
    }
}
