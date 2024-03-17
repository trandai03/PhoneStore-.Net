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

namespace PhoneStore.Net.View
{
    public partial class Nhap_hoa_don : Page
    {
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public Nhap_hoa_don()
        {
            InitializeComponent();
            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
        }

        bool check(string m, Nhap_hoa_don p)
        {
            string query = "SELECT COUNT(*) FROM HOADONs WHERE SOHD = @sohd";
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.AddWithValue("@mand", p.SoHD.Text);

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

        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ttbtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
