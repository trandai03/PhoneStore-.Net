using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PhoneStore.Net.Controller;
using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
namespace PhoneStore.Net.View
{
    public partial class Nhap_hoa_don : Window
    {
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        
        public Nhap_hoa_don()
        {
            InitializeComponent();
            ConnectToDatabase();
            LoadData();
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
                string query = "SELECT s1.MASP, s1.TENSP, s1.SIZE, s1.GIA, ct.SL, ct.SOHD, (s1.GIA * ct.SL) AS THANHTIEN FROM SANPHAMs AS s1 INNER JOIN CTHDs AS ct ON s1.MASP = ct.MASP";
                DataTable dataTable = Sql_select(query);
                dtHoaDon.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private List<NGUOIDUNG> List_ND()
        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            string query = "SELECT * FROM NGUOIDUNGs";
            SQLiteCommand cmd = new SQLiteCommand(query, _con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            List<NGUOIDUNG> NDs = new List<NGUOIDUNG>();
            while (reader.Read())
            {
                NDs.Add(new NGUOIDUNG()
                {
                    MAND = reader.GetString(0),
                    TENND = reader.GetString(1),
                    NGSINH = reader.GetDateTime(2),
                    GIOITINH = reader.GetString(3),
                    SDT = reader.GetString(4),
                    DIACHI = reader.GetString(5),
                    USERNAME = reader.GetString(6),
                    PASS = reader.GetString(7),
                    QTV = reader.GetBoolean(8),
                    TTND = reader.GetBoolean(9),
                    MAIL = reader.GetString(11),
                });
            }
            _con.Close();
            return NDs;
        }

        private List<SANPHAM> List_SP()
        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            string query = "SELECT * FROM SANPHAMs";
            SQLiteCommand cmd = new SQLiteCommand(query, _con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            List<SANPHAM> SPs = new List<SANPHAM>();
            while (reader.Read())
            {
                SPs.Add(new SANPHAM()
                {
                    MASP = reader.GetString(0),
                    TENSP = reader.GetString(1),
                    GIA = reader.GetInt32(2),
                    SL = reader.GetInt32(5),
                    LOAISP = reader.GetString(6),
                    SIZE = reader.GetString(7),
                }) ;
            }
            _con.Close();
            return SPs;
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
        bool check(string m, Nhap_hoa_don p)
        {
            string query = "SELECT COUNT(*) FROM HOADONs WHERE SOHD = @sohd";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.AddWithValue("@sohd", p.SoHD.Text);

            int dem = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.ExecuteNonQuery();
            if (dem > 0) return true;
            else return false;
        }
        string rdma(Nhap_hoa_don p)
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "HD" + rand.Next(0, 10000).ToString();
            } while (check(ma, p));
            return ma;
        }
        private void addbtn_Click(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO CTHDs(SOHD, MASP, SL) VALUES(@sohd, @masp, @sl)";
            SQLiteCommand command = new SQLiteCommand(query, con);
            command.Parameters.AddWithValue("@sohd", SoHD.Text);
            command.Parameters.AddWithValue("@masp", MaSP.Text);
            command.Parameters.AddWithValue("@sl", SL.Text);
            MessageBox.Show("Them thanh cong");
            command.ExecuteNonQuery();
            LoadData();
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ttbtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm sản phẩm mới ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (h == MessageBoxResult.Yes)
            {
                string query = "INSERT INTO HOADONs(SOHD, MAND, MAKH, NGHD, TRIGIA, KHUYENMAI) VALUES(@sohd, @mand, @makh, @nghd, @dongia, @km)";
                SQLiteCommand command = new SQLiteCommand(query, con);
                command.Parameters.AddWithValue("@sohd", SoHD.Text);
                command.Parameters.AddWithValue("@mand", MaND.Text);
                command.Parameters.AddWithValue("@makh", MaKH.Text);
                command.Parameters.AddWithValue("@nghd", NgayHD.Text);
                command.Parameters.AddWithValue("@dongia", DG.Text);
                command.Parameters.AddWithValue("@km", km.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Thêm hóa đơn thành công !", "THÔNG BÁO");
            }
        }

        
    }
}
