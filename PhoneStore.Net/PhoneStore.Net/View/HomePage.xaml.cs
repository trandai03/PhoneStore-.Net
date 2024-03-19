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

namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage
    {
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private string _DoanhThu;
        public string DoanhThu { get => _DoanhThu; set { _DoanhThu = value; } }
        public ICommand LoadDoanhThu { get; set; }
        public ICommand LoadDon { get; set; }
        public ICommand LoadChart { get; set; }
        public ICommand LoadSP { get; set; }

        public HomePage()
        {
            LoadDoanhThu = new RelayCommand<HomePage>((_) => true, (_) => LoadDT());
            LoadDon = new RelayCommand<HomePage>((_) => true, (_) => SoDon());
            LoadSP = new RelayCommand<HomePage>((_) => true, (_) => _LoadSP());
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                SoDon();
                LoadDT();
                _LoadSP();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        public void SoDon()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string Query = $"SELECT COUNT(*) FROM HOADONs WHERE HOADONs.NGHD = DATE('{DateTime.Now:yyyy-MM-dd}')";

                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    Dispatcher.Invoke(() => DonNgay.Text = count.ToString());
                }
            }
        }

        public void LoadDT()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string Query = $"SELECT SUM(TRIGIA) FROM HOADONs WHERE HOADONs.NGHD = DATE('{DateTime.Now:yyyy-MM-dd}')";

                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    object result = command.ExecuteScalar();
                    long total = (result != DBNull.Value && result != null) ? Convert.ToInt64(result) : 0;
                    DoanhThu = (total != 0) ? total.ToString("#,###") + " VNĐ" : "0 VNĐ";
                    Dispatcher.Invoke(() => DTNgay.Text = DoanhThu);
                }
            }
        }

        public void _LoadSP()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databaseName};Version=3;"))
            {
                connection.Open();
                string Query = $"SELECT SUM(CTHDs.SL) FROM CTHDs JOIN HOADONs ON CTHDs.SOHD = HOADONs.SOHD WHERE HOADONs.NGHD = DATE('{DateTime.Now:yyyy-MM-dd}')";

                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    object result = command.ExecuteScalar();
                    int count = (result != DBNull.Value && result != null) ? Convert.ToInt32(result) : 0;
                    Dispatcher.Invoke(() => SPNgay.Text = count.ToString());
                }
            }
        }

    }
}
