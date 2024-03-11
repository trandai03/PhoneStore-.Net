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

namespace PhoneStore.Net.ViewModel
{
    public class AddEmployeeView
    {
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";

        public string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private string _linkaddimage;
        public string linkaddimage { get => _linkaddimage; set { _linkaddimage = value; } }
        public ICommand AddNDCommand { get; set; }
        public ICommand AddImage { get; set; }
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }

       
        public AddEmployeeView()
        {
            linkaddimage = _localLink + "/Resource/Image/addava.png";
            AddNDCommand = new RelayCommand<AddEmployee>((p) => true, (p) => _AddND(p));
            AddImage = new RelayCommand<ImageBrush>((p) => true, (p) => _AddImage(p));
            Closewd = new RelayCommand<AddEmployee>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<AddEmployee>((p) => true, (p) => Minimize(p));
            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
        }
        void Close(AddEmployee p)
        {
            linkaddimage = _localLink + "/Resource/Image/addava.png";
            p.Close();
        }
        void Minimize(AddEmployee p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _AddImage(ImageBrush img)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";

            if (open.ShowDialog() == true)
            {
                if (open.FileName != "")
                    linkaddimage = open.FileName;
            };
            Uri fileUri = new Uri(linkaddimage);
            img.ImageSource = new BitmapImage(fileUri);
        }
        void _AddND(AddEmployee addNDView)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm người dùng ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                if (String.IsNullOrEmpty(addNDView.MaND.Text) || String.IsNullOrEmpty(addNDView.TenND.Text) || String.IsNullOrEmpty(addNDView.SDT.Text) || String.IsNullOrEmpty(addNDView.GT.Text) || String.IsNullOrEmpty(addNDView.QTV.Text) || addNDView.NS.SelectedDate == null)
                {
                    MessageBox.Show("Bạn chưa nhập đầy đủ thông tin !", "THÔNG BÁO");
                    return;
                }
                string checkExistQuery = "SELECT COUNT(*) FROM NGUOIDUNGs WHERE MAND = @mand";
                SQLiteCommand checkExistCommand = new SQLiteCommand(checkExistQuery, con);
                checkExistCommand.Parameters.AddWithValue("@mand", addNDView.MaND.Text);
                
                int existingCount = Convert.ToInt32(checkExistCommand.ExecuteScalar());

                if (existingCount > 0)
                {
                    MessageBox.Show("Mã người dùng đã tồn tại!", "THÔNG BÁO");
                    return;
                }
                checkExistCommand.ExecuteNonQuery();
                string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex reg = new Regex(match);
                if (!reg.IsMatch(addNDView.Mail.Text))
                {
                    MessageBox.Show("Email không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string match1 = @"^((09(\d){8})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9}))$";
                Regex reg1 = new Regex(match1);
                if (!reg1.IsMatch(addNDView.SDT.Text))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string link_img_temp = "";
                if (linkaddimage == "/Resource/Image/addava.png")
                    link_img_temp = "/Resource/Image/addava.png";
                else
                    link_img_temp = "/Resource/Ava/" + addNDView.MaND.Text + ((linkaddimage.Contains(".jpg")) ? ".jpg" : ".png").ToString();

                bool check = true ;
                if(addNDView.QTV.Text == "Quản lý")
                {
                    check = true;
                }
                else
                {
                    check = false;
                }

                string query = "INSERT INTO NGUOIDUNGs(MAND, TENND, NGSINH, SDT, DIACHI, MAIL, QTV, TTND, GIOITINH, USERNAME, AVA) VALUES(@mand, @ten, @ngaysinh, @sdt, @diachi, @mail, @qtv, @ttnd, @gioitinh, @username, @ava)";
                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@mand", addNDView.MaND.Text);
                command.Parameters.AddWithValue("@ten", addNDView.TenND.Text);
                command.Parameters.AddWithValue("@ngaysinh", addNDView.NS.SelectedDate);
                command.Parameters.AddWithValue("@sdt", addNDView.SDT.Text);
                command.Parameters.AddWithValue("@diachi", addNDView.DC.Text);
                command.Parameters.AddWithValue("@mail", addNDView.Mail.Text);
                command.Parameters.AddWithValue("@gioitinh", addNDView.GT.Text);
                command.Parameters.AddWithValue("@qtv", check);
                command.Parameters.AddWithValue("@ttnd", true);
                command.Parameters.AddWithValue("@username", addNDView.MaND.Text);
                command.Parameters.AddWithValue("@ava", link_img_temp);
                command.ExecuteNonQuery();
                MessageBox.Show("Thêm nhân viên mới thành công !", "THÔNG BÁO");
                addNDView.MaND.Text = "";
                addNDView.TenND.Text = "";
                addNDView.NS.SelectedDate = null;
                addNDView.SDT.Text = "";
                addNDView.DC.Text = "";
                addNDView.Mail.Text = "";
                addNDView.GT.Text = "";
                addNDView.QTV.Text = "";
                linkaddimage = _localLink + "/Resource/Image/addava.png";
                Uri fileUri = new Uri(linkaddimage);
                addNDView.HinhAnh1.ImageSource = new BitmapImage(fileUri);
            }
        }
    }
}
