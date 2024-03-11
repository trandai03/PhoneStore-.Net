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

namespace PhoneStore.Net.ViewModel
{
    public class AddProductView
    {
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        public ICommand AddImage { get; set; }
        private string _linkimage;
        public string linkimage { get => _linkimage; set { _linkimage = value; } }
        public ICommand AddProduct { get; set; }
        public ICommand Loadwd { get; set; }
        public AddProductView()
        {
            linkimage = _localLink + "/Resource/Image/add.png";
            AddImage = new RelayCommand<Image>((p) => true, (p) => _AddImage(p));
            AddProduct = new RelayCommand<NewProduct>((p) => true, (p) => _AddProduct(p));
            Loadwd = new RelayCommand<NewProduct>((p) => true, (p) => _Loadwd(p));
            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
        }
        void _Loadwd(NewProduct paramater)
        {
            linkimage = _localLink + "/Resource/Image/add.png";
        }
        void _AddImage(Image img)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
            if (open.ShowDialog() == true)
            {
                linkimage = open.FileName;
            };
            if (linkimage == _localLink + "/Resource/Image/add.png")
            {
                Uri fileUri = new Uri(linkimage, UriKind.Relative);
                img.Source = new BitmapImage(fileUri);
            }
            else
            {
                Uri fileUri = new Uri(linkimage);
                img.Source = new BitmapImage(fileUri);
            }
        }
        void _AddProduct(NewProduct p)
        {
            if (string.IsNullOrEmpty(p.MaSp.Text) || string.IsNullOrEmpty(p.TenSp.Text) || string.IsNullOrEmpty(p.LoaiSp.Text) || string.IsNullOrEmpty(p.GiaSp.Text) || string.IsNullOrEmpty(p.SizeSp.Text) || string.IsNullOrEmpty(p.SlSp.Text))
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string checkExistQuery = "SELECT COUNT(*) FROM SANPHAMs WHERE MASP = @masp";
                SQLiteCommand checkExistCommand = new SQLiteCommand(checkExistQuery, con);
                checkExistCommand.Parameters.AddWithValue("@masp", p.MaSp.Text);

                int existingCount = Convert.ToInt32(checkExistCommand.ExecuteScalar());
                checkExistCommand.ExecuteNonQuery();
                if (existingCount > 0)
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại !", "THÔNG BÁO");
                    return;
                }
                else
                {
                    MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm sản phẩm mới ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (h == MessageBoxResult.Yes)
                    {
                        if (int.Parse(p.GiaSp.Text) < 0)
                        {
                            MessageBox.Show("Giá sản phẩm không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        
                        if (int.Parse(p.SlSp.Text) < 0)
                        {
                            MessageBox.Show("Số lượng sản phẩm không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        string link_sp = "/Resource/ImgProduct/" + "product_" + p.MaSp.Text + ((linkimage.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                        
                        string query = "INSERT INTO SANPHAMs(MASP, TENSP, GIA, MOTA, SL, LOAISP, SIZE, HINHSP) VALUES(@masp, @tensp, @gia, @mota, @sl, @loaisp, @size, @hinhsp)";
                        SQLiteCommand command = new SQLiteCommand(query, con);
                        command.Parameters.AddWithValue("@masp", p.MaSp.Text);
                        command.Parameters.AddWithValue("@tensp", p.TenSp.Text);
                        command.Parameters.AddWithValue("@gia", p.GiaSp.Text);
                        command.Parameters.AddWithValue("@mota", p.MotaSp.Text);
                        command.Parameters.AddWithValue("@sl", p.SlSp.Text);
                        command.Parameters.AddWithValue("@loaisp", p.LoaiSp.Text);
                        command.Parameters.AddWithValue("@size", p.SizeSp.Text);
                        command.Parameters.AddWithValue("@hinhsp", link_sp);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Thêm sản phẩm mới thành công !", "THÔNG BÁO");
                        p.MaSp.Text = "";
                        p.TenSp.Text = "";
                        p.GiaSp.Text = "";
                        p.MotaSp.Text = "";
                        p.SlSp.Text = "";
                        p.LoaiSp.Text = "";
                        p.SizeSp.Text = "";
                        Uri fileUri = new Uri(_localLink + "/Resource/Image/add.png");
                        p.HinhAnh.Source = new BitmapImage(fileUri);
                    }
                }
                
            }
        }
    }
}
