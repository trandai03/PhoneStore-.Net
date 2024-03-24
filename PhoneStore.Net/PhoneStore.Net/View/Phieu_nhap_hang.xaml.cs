using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PhoneStore.Net.View
{
    public partial class Phieu_nhap_hang : Window
    {
        string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public int tongtien {  get; set; }
        public Phieu_nhap_hang()
        {
            InitializeComponent();
            LoadSANPHAMs();
            MaPN.Text = GetMAPNFromDB().ToString();
            LoadData();
        }

        private void LoadSANPHAMs()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                // Insert SanPham
                string querySP = "SELECT * FROM SANPHAMs";
                using (SQLiteCommand command = new SQLiteCommand(querySP, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<SANPHAM> spLst = new List<SANPHAM>();
                            while (reader.Read())
                            {
                                SANPHAM sp = new SANPHAM();
                                sp.MASP = reader.GetString(0);
                                sp.TENSP = reader.GetString(1);
                                sp.SIZE = reader.GetString(7);
                                sp.SL = reader.GetInt32(5);
                                spLst.Add(sp);
                            }
                            SP.ItemsSource = spLst;
                        }
                        else
                        {
                            Console.WriteLine("Không có dữ liệu để đọc.");
                        }
                    }
                }

                connection.Close();
            }
        }
        public void LoadData()
        {
            try
            {
                string query = "SELECT s1.MASP, s1.TENSP, s1.SIZE, ct.SL, (ct.SL * @dongia) AS THANHTIEN FROM SANPHAMs AS s1 INNER JOIN CTPNs AS ct ON s1.MASP = ct.MASP WHERE MAPN = @mapn";
                DataTable dataTable = Sql_select(query);
                dtNhaphang.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private DataTable Sql_select(string sql_querry)
        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            DataTable dt = new DataTable();
            SQLiteCommand cmd = new SQLiteCommand(sql_querry, _con);
            cmd.Parameters.AddWithValue("@mapn", MaPN.Text);
            cmd.Parameters.AddWithValue("@dongia", DG.Text);
            SQLiteDataReader reader = cmd.ExecuteReader();
            Console.WriteLine(reader.ToString());
            dt.Load(reader);
            _con.Close();
            return dt;
        }
        private int GetMAPNFromDB()
        {
            int nextId = 0;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();

                string query = "SELECT MAX(MAPN) FROM PHIEUNHAPs;";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        nextId = Convert.ToInt32(result) + 1;
                    }
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            return nextId;
        }

        private void InsertPNH()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();

                // Insert PhieuNhap
                string queryPN = $"INSERT INTO PHIEUNHAPs (MAPN, MAND, NGAYNHAP) VALUES (@MAPN, @MAND, @NGAYNHAP)";
                using (SQLiteCommand command = new SQLiteCommand(queryPN, connection))
                {
                    command.Parameters.AddWithValue("@MAPN", int.Parse(MaPN.Text));
                    command.Parameters.AddWithValue("@MAND", MaND.Text);
                    command.Parameters.AddWithValue("@NGAYNHAP", DateTime.Now);
                    int rowsAffected = command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        bool check_MAPN(string s)
        {

            foreach (PHIEUNHAP x in DBConnect.DataProvider.Instance.List_PN())
            {
                if (x.MAPN.ToString() == s)
                {
                    return true;
                }
            }
            return false;
        }
        private void InsertToDB(object sender, RoutedEventArgs e)
        {
            if(check_MAPN(MaPN.Text) == false)
            {
                InsertPNH();
            }
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string sql = "INSERT INTO CTPNs(MAPN, MASP, SL) VALUES(@mapn, @masp, @sl)";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@mapn", MaPN.Text);
                    command.Parameters.AddWithValue("@masp", ((SANPHAM)SP.SelectedItem).MASP);
                    command.Parameters.AddWithValue("@sl", int.Parse(SL.Text));
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm thành công !!", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Thêm không thành công !!", "Thông báo");
                    }
                }
                tongtien += int.Parse(SL.Text) * int.Parse(DG.Text);
                TT.Text = tongtien.ToString();
                LoadSANPHAMs();
                LoadData();
                SL.Text = ""; DG.Text = "";
                connection.Close();
            }
        }

        private void RemoveByName(object sender, RoutedEventArgs e)
        {
            DataRowView selectedNhapHang = (DataRowView)dtNhaphang.SelectedItem;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string querySP = "DELETE FROM CTPNs WHERE MASP = @masp;";
                using (SQLiteCommand command = new SQLiteCommand(querySP, connection))
                {
                    command.Parameters.AddWithValue("@masp", selectedNhapHang["MASP"].ToString());
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa thành công !!", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công !!", "Thông báo");
                    }
                }
                tongtien -= int.Parse(selectedNhapHang["THANHTIEN"].ToString());
                TT.Text = tongtien.ToString();
                LoadSANPHAMs();
                LoadData();
                connection.Close();
            }
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nhập thành công !!", "Thông báo");
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string sql = "DELETE FROM CTPNs WHERE MAPN = @mapn";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@mapn", MaPN.Text);
                    cmd.ExecuteNonQuery();
                }
                string querySP = "DELETE FROM PHIEUNHAPs WHERE MAPN = @mapn";
                using (SQLiteCommand command = new SQLiteCommand(querySP, connection))
                {
                    command.Parameters.AddWithValue("@mapn", MaPN.Text);
                    command.ExecuteNonQuery();
                }
                
                LoadSANPHAMs();
                LoadData();
                connection.Close();
            }
            this.Close();
        }

        private void dtHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
