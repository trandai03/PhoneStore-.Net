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
    public class DetailProductView
    {
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        
        public ICommand UpdateProduct { get; set; }
        public ICommand GetName { get; set; }
        private string TenSP1;
        public ICommand Loadwd { get; set; }
        public ICommand DeleteProduct { get; set; }
        public DetailProductView()
        {
            
            GetName = new RelayCommand<Detail_product>((p) => true, (p) => _GetName(p));
            UpdateProduct = new RelayCommand<Detail_product>((p) => true, (p) => _UpdateProduct(p));
            DeleteProduct = new RelayCommand<Detail_product>((p) => true, (p) => _DeleteProduct(p));
            ConnectToDatabase();
        }
       
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
        }

        
        void _GetName(Detail_product p)
        {
            TenSP1 = p.TenSP.Text;
        }
        void _UpdateProduct(Detail_product p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn cập nhật sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                string query = "UPDATE SANPHAMs SET TENSP = @tensp, GIA = @gia, MOTA = @mota, SL = @sl, LOAISP = @loai, SIZE = @size WHERE MASP = @masp";

                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@tensp", p.TenSP.Text);
                command.Parameters.AddWithValue("@gia", p.GiaSP.Text);
                command.Parameters.AddWithValue("@mota", p.Mota.Text);
                command.Parameters.AddWithValue("@sl", p.SLSP.Text);
                command.Parameters.AddWithValue("@loai", p.LoaiSP.Text);
                command.Parameters.AddWithValue("@size", p.Size.Text);
                command.ExecuteNonQuery();
                p.TenSP.Text = "";
                p.GiaSP.Text = "";
                p.Mota.Text = "";
                p.SLSP.Text = "";
                p.LoaiSP.Text = "";
                p.Size.Text = "";
                MessageBox.Show("Cập nhật sản phẩm thành công !", "THÔNG BÁO");
            }
        }
        void _DeleteProduct(Detail_product p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn xóa sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                string query = "DELETE FROM SANPHAMs WHERE TENSP = @tensp AND SL = @sl";
                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@tensp", p.TenSP.Text);
                command.Parameters.AddWithValue("@sl", p.SLSP.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Xóa sản phẩm thành công !", "THÔNG BÁO");
                p.TenSP.Text = "";
                p.SLSP.Text = "";
            }
        }
    }
}
