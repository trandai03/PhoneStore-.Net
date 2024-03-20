using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using Xamarin.Forms;
using static PhoneStore.Net.DBClass.DBConnect;

namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for Phieu_nhap_hang.xaml
    /// </summary>
    public partial class Phieu_nhap_hang : Window
    {
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";

        public Phieu_nhap_hang()
        {
            InitializeComponent();
        }
        bool check_MAPN(string s)
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
        string rdma_MAPN()
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "PN" + rand.Next(0, 10000).ToString();
            } while (check_MAPN(ma));
            return ma;
        }
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
        }
        private List<PHIEUNHAP> List_PN()
        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            string query = "SELECT MAPN FROM PHIEUNHAPs";
            SQLiteCommand cmd = new SQLiteCommand(query, _con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            List<PHIEUNHAP> PNs = new List<PHIEUNHAP>();
            while (reader.Read())
            {
                PNs.Add(new PHIEUNHAP()
                {
                    MAPN = reader.GetInt32(0),
                    
                    
                });
            }
            _con.Close();
            return PNs;
        }
        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
        }

        public void XacNhan(object sender, RoutedEventArgs e)
        {
            PHIEUNHAP phieu = new PHIEUNHAP();
            phieu.MAPN = int.Parse(MaPN.Text);
            phieu.MAND = "1";
            phieu.NGAYNHAP = DateTime.Now;
            DBConnect.DataProvider.Instance.NhapPhieu(phieu);
        }
    }
}
