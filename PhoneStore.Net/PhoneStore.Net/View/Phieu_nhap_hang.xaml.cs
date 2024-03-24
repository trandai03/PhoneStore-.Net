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

        public Phieu_nhap_hang()
        {
            InitializeComponent();
            LoadSANPHAMs();
            MaPN.Text = GetMAPNFromDB().ToString();
            SL.Text = "1";
            DG.Text = "1";
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
                    command.Parameters.AddWithValue("@MAND", "1");
                    command.Parameters.AddWithValue("@NGAYNHAP", DateTime.Now.ToString());
                    int rowsAffected = command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void InsertToDB(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string querySP = "UPDATE SANPHAMs SET SL = SL + @ValueToAdd WHERE TENSP = @TENSP;";
                using (SQLiteCommand command = new SQLiteCommand(querySP, connection))
                {
                    command.Parameters.AddWithValue("@TENSP", ((SANPHAM)SP.SelectedItem).TENSP);
                    command.Parameters.AddWithValue("@ValueToAdd", int.Parse(SL.Text));
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Them thành công !!", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Them khong thành công !!", "Thông báo");
                    }
                }

                LoadSANPHAMs();
                connection.Close();
            }
        }

        private void UpdateByName(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string querySP = "UPDATE SANPHAMs SET GIA = @GIA, SL = @SL WHERE TENSP = @TENSP;";
                using (SQLiteCommand command = new SQLiteCommand(querySP, connection))
                {
                    command.Parameters.AddWithValue("@TENSP", ((SANPHAM)SP.SelectedItem).TENSP);
                    command.Parameters.AddWithValue("@GIA", int.Parse(DG.Text));
                    command.Parameters.AddWithValue("@SL", int.Parse(SL.Text));
                    command.ExecuteNonQuery();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sua thành công !!", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Sua khong thành công !!", "Thông báo");
                    }
                }

                LoadSANPHAMs();
                connection.Close();
            }
        }

        private void RemoveByName(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string querySP = "DELETE FROM SANPHAMs WHERE TENSP = @TENSP;";
                using (SQLiteCommand command = new SQLiteCommand(querySP, connection))
                {
                    command.Parameters.AddWithValue("@TENSP", ((SANPHAM)SP.SelectedItem).TENSP);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xoa thành công !!", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Xoa khong thành công !!", "Thông báo");
                    }
                }

                LoadSANPHAMs();
                connection.Close();
            }
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            InsertPNH();
            MessageBox.Show("Nhập thành công !!", "Thông báo");
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dtHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
