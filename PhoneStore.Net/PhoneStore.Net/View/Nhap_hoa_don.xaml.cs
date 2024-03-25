using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using PhoneStore.Net.Controller;
using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
using static PhoneStore.Net.DBClass.DBConnect;
namespace PhoneStore.Net.View
{
    public partial class Nhap_hoa_don : Window
    {
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public int tongtien = 0;
        
        public Nhap_hoa_don()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadData();
            Load_CombboBox();
        }
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
        }
        
        public void LoadData()
        {
            try
            {
                string query = "SELECT s1.MASP, s1.TENSP, s1.SIZE, s1.GIA, ct.SL, ct.SOHD, (s1.GIA * ct.SL) AS THANHTIEN FROM SANPHAMs AS s1 INNER JOIN CTHDs AS ct ON s1.MASP = ct.MASP WHERE SOHD = @sohd";
                DataTable dataTable = Sql_select(query);
                dtHoaDon.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void MaSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MaSP.SelectedItem != null)
            {
                foreach (SANPHAM x in DBConnect.DataProvider.Instance.List_SP())
                {
                    if (x.MASP == MaSP.SelectedItem.ToString())
                    {
                        DG.Text = x.GIA.ToString();
                        TenSP.Text = x.TENSP.ToString();
                    }
                }
            }
            else
            {
                Console.WriteLine("KHONG");
            }

        }
        public void Load_CombboBox()
        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            string query = "SELECT MASP  FROM SANPHAMs";
            SQLiteCommand cmd = new SQLiteCommand(query, _con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string masp = reader.GetString(0);
                MaSP.Items.Add(masp);
            }
            _con.Close();
        }

        
        private DataTable Sql_select(string sql_querry)
        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            DataTable dt = new DataTable();
            SQLiteCommand cmd = new SQLiteCommand(sql_querry, _con);
            cmd.Parameters.AddWithValue("@sohd", SoHD.Text);
            SQLiteDataReader reader = cmd.ExecuteReader();
            Console.WriteLine(reader.ToString());
            dt.Load(reader);
            _con.Close();
            return dt;
        }

        string rdma_SOHD()
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "HD" + rand.Next(0, 10000).ToString();
            } while (check_SOHD(ma));
            return ma;
        }
        bool check_SOHD(string s)
        {
            
            foreach(HOADON x in DBConnect.DataProvider.Instance.List_HD())
            {
                if(x.SOHD.ToString() == s)
                {
                    return true;
                }
            }
            return false;
        }
        bool check_MAKH(string s)
        {
            foreach (KHACHHANG x in DBConnect.DataProvider.Instance.List_KH())
            {
                if (x.MAKH == s)
                {
                    return true;
                }
            }
            return false;
        }
        string rdma_MAKH()
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "KH" + rand.Next(0, 10000).ToString();
            } while (check_MAKH(ma));
            return ma;
        }
        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            if (MaSP.Text == "")
            {
                System.Windows.MessageBox.Show("Bạn chưa chọn sản phẩm để thêm !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SoHD.Text == "")
            {
                System.Windows.MessageBox.Show("Bạn chưa nhập số hóa đơn !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SL.Text == "")
            {
                System.Windows.MessageBox.Show("Bạn chưa nhập số lượng sản phẩm !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (int.Parse(SL.Text) < 0)
            {
                MessageBox.Show("Số lượng sản phẩm không hợp lệ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MaKH.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn khách hàng !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            SANPHAM temp = null;
            bool ok = false;
            foreach(SANPHAM x in DBConnect.DataProvider.Instance.List_SP())
            {
                if(x.MASP == MaSP.Text)
                {
                    temp = x;
                    ok = true;
                }
            }
            if(ok == false)
            {
                MessageBox.Show("Không tồn tại mã sản phẩm này", "Thông báo");
                return;
            }

            if(temp.SL >= int.Parse(SL.Text))
            {
                MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm sản phẩm này ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if(h == MessageBoxResult.Yes) {
                    if (check_MAKH(MaKH.Text) == false)
                    {
                        string sql = "INSERT INTO KHACHHANGs(MAKH, HOTEN, SDT) VALUES(@makh, @hoten, @sdt)";
                        SQLiteCommand cmd = new SQLiteCommand(sql, con);
                        cmd.Parameters.AddWithValue("@makh", MaKH.Text);
                        cmd.Parameters.AddWithValue("@hoten", TenKH.Text);
                        cmd.Parameters.AddWithValue("@sdt", SDT.Text);
                        cmd.ExecuteNonQuery();
                    }
                    if (check_SOHD(SoHD.Text) == false)
                    {
                        string sql = "INSERT INTO HOADONs(SOHD, NGHD, TRIGIA, MAND, MAKH, KHUYENMAI) VALUES(@sohd, @nghd, @trigia, @mand, @makh, @km)";
                        SQLiteCommand cmd = new SQLiteCommand(sql, con);
                        cmd.Parameters.AddWithValue("@sohd", SoHD.Text);
                        cmd.Parameters.AddWithValue("@nghd", DateTime.Now);
                        cmd.Parameters.AddWithValue("@trigia", 0);
                        cmd.Parameters.AddWithValue("@mand", MaND.Text);
                        cmd.Parameters.AddWithValue("@makh", MaKH.Text);
                        cmd.Parameters.AddWithValue("@km", 0);
                        cmd.ExecuteNonQuery();
                    }
                    
                    string query = "INSERT INTO CTHDs(SOHD, MASP, SL) VALUES(@sohd, @masp, @sl)";
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    command.Parameters.AddWithValue("@sohd", SoHD.Text);
                    command.Parameters.AddWithValue("@masp", MaSP.Text);
                    command.Parameters.AddWithValue("@sl", SL.Text);
                    MessageBox.Show("Thêm thành công", "THÔNG BÁO");
                    command.ExecuteNonQuery();
                    foreach(SANPHAM x in DBConnect.DataProvider.Instance.List_SP())
                    {
                        if(x.MASP == MaSP.Text)
                        {
                            x.SL -= int.Parse(SL.Text);
                            temp.SL = x.SL;
                        }
                    }
                    string query_sql = "UPDATE SANPHAMs SET SL = @sl WHERE MASP = @masp";
                    SQLiteCommand cd = new SQLiteCommand(query_sql, con);
                    cd.Parameters.AddWithValue("@sl", temp.SL);
                    cd.Parameters.AddWithValue("@masp", MaSP.Text)
;                    cd.ExecuteNonQuery();
                    tongtien += int.Parse(DG.Text) * int.Parse(SL.Text);
                    TT.Text = tongtien.ToString();
                    MaSP.Text = "";
                    DG.Text = "";
                    SL.Text = "";
                    TenSP.Text = "";
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Sản phẩm tồn kho không đủ cung cấp !", "THÔNG BÁO");
                return;
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedSanpham = (DataRowView)dtHoaDon.SelectedItem;
            if (selectedSanpham == null)
            {
                System.Windows.MessageBox.Show("Bạn chưa chọn sản phẩm để xóa !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult h = System.Windows.MessageBox.Show(" Bạn có chắc muốn xóa sản phẩm.", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                string query = "DELETE FROM CTHDs WHERE MASP = @masp AND SL = @sl";
                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@masp", selectedSanpham["MASP"].ToString());
                command.Parameters.AddWithValue("@sl", selectedSanpham["SL"].ToString());
                command.ExecuteNonQuery();
                MessageBox.Show("Xóa thành công !!", "Thông báo");
                tongtien -= int.Parse(selectedSanpham["THANHTIEN"].ToString());
                TT.Text = tongtien.ToString();
                LoadData();
            }
            else
                return;
        }

        private void ttbtn_Click(object sender, RoutedEventArgs e)
        {
            if (dtHoaDon.Items.Count == 1)
            {
                System.Windows.MessageBox.Show("Thông tin hóa đơn chưa đầy đủ !", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn tính tiền thanh toán sản phẩm ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                int tonggia = 0;
                foreach(CTHD x in DBConnect.DataProvider.Instance.List_CTHD())
                {
                    if(x.SOHD.ToString() == SoHD.Text)
                    {
                        foreach (SANPHAM y in DBConnect.DataProvider.Instance.List_SP())
                        {
                            if(x.MASP == y.MASP)
                            {
                                tonggia += x.SL * y.GIA;
                            }
                        }
                    }
                }
                int km = 0;
                if (tonggia > 2000000 && tonggia <= 5000000)
                    km = 2;
                else if (tonggia > 5000000 && tonggia <= 10000000)
                    km = 5;
                else if (tonggia > 10000000)
                    km = 10;
                double tmp = (double)(1 - (double)km / 100) * tonggia;
                int tien = (int)tmp;
                TT.Text = tien.ToString() + " VND";
                

                string query = "UPDATE HOADONs SET TRIGIA = @trigia, KHUYENMAI = @khuyenmai WHERE SOHD = @sohd";
                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@sohd", SoHD.Text);
                command.Parameters.AddWithValue("@trigia", tien);
                command.Parameters.AddWithValue("@khuyenmai", km);
                command.ExecuteNonQuery();
                MessageBox.Show("Thêm hóa đơn thành công !", "THÔNG BÁO");
            }
        }

        private void xacnhanbtn_Click(object sender, RoutedEventArgs e)
        {
            HoaDonBH hoaDonBH = new HoaDonBH();
            hoaDonBH.TenKH.Text = TenKH.Text;
            hoaDonBH.sdt.Text = SDT.Text;
            hoaDonBH.ngay.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            hoaDonBH.sohd.Text = SoHD.Text;
            hoaDonBH.tt.Text = TT.Text;
            string query = "SELECT s1.MASP, s1.TENSP,  s1.GIA, ct.SL, ct.SOHD, (s1.GIA * ct.SL) AS THANHTIEN FROM SANPHAMs AS s1 INNER JOIN CTHDs AS ct ON s1.MASP = ct.MASP WHERE SOHD = @sohd";
            DataTable dataTable = Sql_select(query);
            hoaDonBH.dtInHoaDon.ItemsSource = dataTable.DefaultView;
            hoaDonBH.ShowDialog();
            
            
        }
    }
}
