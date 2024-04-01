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

namespace PhoneStore.Net.ViewModel
{
    public class DetailProductView
    {
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private string _linkimage;
        public string linkimage { get => _linkimage; set { _linkimage = value; } }
        public ICommand UpdateProduct { get; set; }
        public ICommand AddImage { get; set; }
        public ICommand Loadwd { get; set; }
        public ICommand DeleteProduct { get; set; }
        public string getImage;
        public DetailProductView()
        {
            UpdateProduct = new RelayCommand<Detail_product>((p) => true, (p) => _UpdateProduct(p));
            DeleteProduct = new RelayCommand<Detail_product>((p) => true, (p) => _DeleteProduct(p));
            AddImage = new RelayCommand<Image>((p) => true, (p) => _AddImage(p));
            ConnectToDatabase();
        }
       
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
        }
        void _AddImage(Image img)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
            if (open.ShowDialog() == true)
            {
                linkimage = open.FileName;
            };
            if(linkimage == null)
            {
                linkimage = _localLink + "/Resource/Image/add.png";
            }
                Uri fileUri = new Uri(linkimage);
                img.Source = new BitmapImage(fileUri);
        }
        void _UpdateProduct(Detail_product p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn cập nhật sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                string query = "UPDATE SANPHAMs SET TENSP = @tensp, GIA = @gia, MOTA = @mota, SL = @sl, LOAISP = @loai, SIZE = @size, HINHSP = @hinhsp WHERE MASP = @masp";
                
                string fileName = Path.GetFileName(p.tmp);
                string link_sp = "/Resource/ImgProduct/" + fileName;
                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@tensp", p.TenSP.Text);
                command.Parameters.AddWithValue("@gia", p.GiaSP.Text);
                command.Parameters.AddWithValue("@mota", p.Mota.Text);
                command.Parameters.AddWithValue("@sl", p.SLSP.Text);
                command.Parameters.AddWithValue("@loai", p.LoaiSP.Text);
                command.Parameters.AddWithValue("@size", p.Size.Text);
                command.Parameters.AddWithValue("@masp", p.MaSP.Text);
                command.Parameters.AddWithValue("@hinhsp", link_sp);
                command.ExecuteNonQuery();
                p.TenSP.Text = "";
                p.GiaSP.Text = "";
                p.Mota.Text = "";
                p.SLSP.Text = "";
                p.LoaiSP.Text = "";
                p.Size.Text = "";
                p.MaSP.Text = "";
                Uri fileUri = new Uri(_localLink + "/Resource/Image/add.png");
                p.HinhAnh.Source = new BitmapImage(fileUri);
                MessageBox.Show("Cập nhật sản phẩm thành công !", "THÔNG BÁO");
                p.Close();
            }
        }
        void _DeleteProduct(Detail_product p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                string sql = "DELETE FROM CTHDs WHERE MASP = @masp";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.Parameters.AddWithValue("@masp", p.MaSP.Text);
                cmd.ExecuteNonQuery();

                string query = "DELETE FROM SANPHAMs WHERE MASP = @masp";
                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@masp", p.MaSP.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Xóa sản phẩm thành công !", "THÔNG BÁO");
                p.TenSP.Text = "";
                p.GiaSP.Text = "";
                p.Mota.Text = "";
                p.SLSP.Text = "";
                p.LoaiSP.Text = "";
                p.Size.Text = "";
                p.MaSP.Text = "";
                Uri fileUri = new Uri(_localLink + "/Resource/Image/add.png");
                p.HinhAnh.Source = new BitmapImage(fileUri);
                p.Close();
            }
        }
    }
}
