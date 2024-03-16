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
       
        public AddEmployeeView()
        {
            linkaddimage = _localLink + "/Resource/Image/addava.png";
            AddNDCommand = new RelayCommand<AddEmployee>((p) => true, (p) => _AddND(p));
            AddImage = new RelayCommand<ImageBrush>((p) => true, (p) => _AddImage(p));
            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
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
        bool check_SDT(string s)
        {
            for(int i = 0; i < s.Length; i++)
            {
                if (!char.IsDigit(s[i]))
                {
                    return false;
                }
            }
            return true;
        }
        bool check(string m, AddEmployee p)
        {
            string checkExistQuery = "SELECT COUNT(*) FROM NGUOIDUNGs WHERE MAND = @mand";
            SQLiteCommand checkExistCommand = new SQLiteCommand(checkExistQuery, con);
            checkExistCommand.Parameters.AddWithValue("@mand", p.MaND.Text);

            int dem = Convert.ToInt32(checkExistCommand.ExecuteScalar());
            checkExistCommand.ExecuteNonQuery();
            if (dem > 0) return true;
            else return false;
        }
        string rdma(AddEmployee p)
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "ND" + rand.Next(0, 10000).ToString();
            } while (check(ma, p));
            return ma;
        }
        void _AddND(AddEmployee addNDView)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm người dùng ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                if (String.IsNullOrEmpty(addNDView.MaND.Text) || String.IsNullOrEmpty(addNDView.TenND.Text) || String.IsNullOrEmpty(addNDView.SDT.Text) || String.IsNullOrEmpty(addNDView.GT.Text) || String.IsNullOrEmpty(addNDView.QTV.Text) || addNDView.NS.SelectedDate == null || String.IsNullOrEmpty(addNDView.User.Text) || String.IsNullOrEmpty(addNDView.Password.Text) || linkaddimage == _localLink + "/Resource/Image/addava.png")
                {
                    MessageBox.Show("Bạn chưa nhập đầy đủ thông tin !", "THÔNG BÁO");
                    return;
                }


                string check_query = "SELECT COUNT(*) FROM NGUOIDUNGs WHERE USERNAME = @user";
                SQLiteCommand checkCommand = new SQLiteCommand(check_query, con);
                checkCommand.Parameters.AddWithValue("@user", addNDView.User.Text);
                int dem = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (dem > 0)
                {
                    MessageBox.Show("User đã tồn tại!", "THÔNG BÁO");
                    return;
                }
                checkCommand.ExecuteNonQuery();

                string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex reg = new Regex(match);
                if (!reg.IsMatch(addNDView.Mail.Text))
                {
                    MessageBox.Show("Email không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                string tmp = addNDView.SDT.Text;
                if(tmp.Length != 10 || tmp[0] != '0' || check_SDT(tmp) == false) {
                    MessageBox.Show("Số điện thoại không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string fileName = Path.GetFileName(linkaddimage);
                string link_img_temp = "/Resource/avatar/" + fileName;

                bool check = true ;
                if(addNDView.QTV.Text == "Quản lý")
                {
                    check = true;
                }
                else
                {
                    check = false;
                }

                string query = "INSERT INTO NGUOIDUNGs(MAND, TENND, NGSINH, SDT, DIACHI, MAIL, QTV, TTND, GIOITINH, USERNAME, AVA, PASS) VALUES(@mand, @ten, @ngaysinh, @sdt, @diachi, @mail, @qtv, @ttnd, @gioitinh, @username, @ava, @pass)";
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
                command.Parameters.AddWithValue("@username", addNDView.User.Text);
                command.Parameters.AddWithValue("@pass", addNDView.Password.Text);
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
                addNDView.User.Text = "";
                addNDView.Password.Text = "";
                linkaddimage = _localLink + "/Resource/Image/addava.png";
                Uri fileUri = new Uri(linkaddimage);
                addNDView.HinhAnh1.ImageSource = new BitmapImage(fileUri);
            }
            addNDView.Close();
        }
    }
}
