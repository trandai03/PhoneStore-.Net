using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using PhoneStore.Net.View;
using PhoneStore.Net.DBClass;
using Xamarin.Forms;
using static PhoneStore.Net.DBClass.DBConnect;
using System.Data;
using System.Collections.Specialized;

namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public class Result
    {
        private int _Hour;
        public int Hour { get => _Hour; set { _Hour = value; } }
        private int _SP;
        public int SP { get => _SP; set { _SP = value; } }
        public Result(int h = 0, int sp = 0)
        {
            Hour = h; SP = sp;
        }
    }

    public partial class HomePage
    {
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private string _DoanhThu;
        public string DoanhThu { get => _DoanhThu; set { _DoanhThu = value; } }
        public string SanPham { get; set; }
        public int SL { get; set; }
        public DateTime Ngay { get; set; }
        public ICommand LoadDoanhThu { get; set; }
        public ICommand LoadDon { get; set; }
        public ICommand LoadChart { get; set; }
        public ICommand LoadSP { get; set; }
        public List<Result> Data { get; set; }

        public HomePage()
        {
            LoadDoanhThu = new RelayCommand<HomePage>((_) => true, (_) => LoadDT());
            LoadDon = new RelayCommand<HomePage>((_) => true, (_) => SoDon());
            LoadSP = new RelayCommand<HomePage>((_) => true, (_) => SP());
            LoadChart = new RelayCommand<HomePage>((_) => true, (_) => LineChart());
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                SoDon();
                LoadDT();
                SP();
                LineChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        public void LineChart()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                var query = $"SELECT strftime('%H', HOADONs.NGHD) AS Hour, SUM(CTHDs.SL) AS TotalQuantity " +
                            $"FROM CTHDs " +
                            $"JOIN HOADONs ON CTHDs.SOHD = HOADONs.SOHD " +
                            $"WHERE HOADONs.NGHD = DATE('{DateTime.Now:yyyy-MM-dd}') " +
                            $"GROUP BY Hour";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    Data = new List<Result>();
                    while (reader.Read())
                    {
                        int hour = Convert.ToInt32(reader["Hour"]);
                        int quantity = Convert.ToInt32(reader["TotalQuantity"]);
                        Data.Add(new Result(hour, quantity));
                    }
                    reader.Close();
                    Chart.ItemsSource = Data;
                }
            }
        }


        /*public void LineChart()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                var query = $"SELECT CTHDs.SL, HOADONs.NGHD, CTHDs.MASP FROM CTHDs JOIN HOADONs ON CTHDs.SOHD = HOADONs.SOHD WHERE HOADONs.NGHD = DATE('{DateTime.Now:yyyy-MM-dd}')";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    Data = new List<Result>();
                    for (int h = 0; h <= 24; h++)
                    {
                        int value = 0;
                        while (reader.Read())
                        {
                            if (Convert.ToDateTime(reader["NGHD"]).Hour == h &&
                                Convert.ToDateTime(reader["NGHD"]).Day == DateTime.Now.Day &&
                                Convert.ToDateTime(reader["NGHD"]).Month == DateTime.Now.Month &&
                                Convert.ToDateTime(reader["NGHD"]).Year == DateTime.Now.Year)
                            {
                                value += Convert.ToInt32(reader["SL"]);
                            }
                        }
                        Result result = new Result(h, value);
                        Data.Add(result);
                    }
                    reader.Close();
                    Chart.ItemsSource = Data;
                }
            }
        }*/

        public void SoDon()
        {
            int count = 0;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM HOADONs WHERE DATE(HOADONs.NGHD) = DATE('{DateTime.Now:yyyy-MM-dd}')";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            DonNgay.Text = count.ToString();
        }

        public void LoadDT()
        {
            long total = 0;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string Query = $"SELECT SUM(TRIGIA) FROM HOADONs WHERE HOADONs.NGHD = DATE('{DateTime.Now:yyyy-MM-dd}')";

                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    object result = command.ExecuteScalar();
                    total = (result != DBNull.Value && result != null) ? Convert.ToInt64(result) : 0;
                    DoanhThu = (total != 0) ? total.ToString("#,###") + " VNĐ" : "0 VNĐ";
                    Dispatcher.Invoke(() => DTNgay.Text = DoanhThu);
                }
            }
        }

        public void SP()
        {
            int count = 0;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string Query = $"SELECT SUM(CTHDs.SL) FROM CTHDs JOIN HOADONs ON CTHDs.SOHD = HOADONs.SOHD WHERE DATE(HOADONs.NGHD) = DATE('{DateTime.Now:yyyy-MM-dd}')";

                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    object result = command.ExecuteScalar();
                    count = (result != DBNull.Value && result != null) ? Convert.ToInt32(result) : 0;
                }
            }
            SPNgay.Text = count.ToString();
        }
    }
}