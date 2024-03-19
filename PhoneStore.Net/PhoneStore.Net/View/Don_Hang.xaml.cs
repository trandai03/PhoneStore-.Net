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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for Don_Hang.xaml
    /// </summary>
    public partial class Don_Hang : Page
    {
        string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public Don_Hang()
        {
            InitializeComponent();
            Language = System.Windows.Markup.XmlLanguage.GetLanguage("vi-VN");
            LoadData();
        }
        
        private void LoadData()
        {
            try
            {
                string query = "SELECT SOHD ,MAKH , NGHD , TRIGIA , KHUYENMAI    FROM HOADONs";
                DataTable dataTable = DBConnect.DataProvider.Instance.Sql_select(query);
                dtDonHang.ItemsSource = dataTable.DefaultView;
                Console.WriteLine(dataTable.DefaultView);
                MessageBox.Show(dataTable.DefaultView.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            
        }


        private void cbxChon1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            DataTable dataTable = DBConnect.DataProvider.Instance.FilterSP(cbxChon1.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last());
            dtDonHang.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txbSearch.Text != "")
                {
                    DataTable dataTable = DBConnect.DataProvider.Instance.searchDH(txbSearch.Text);
                    dtDonHang.ItemsSource = dataTable.DefaultView;
                }
                else
                    LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void cbxChon1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DataTable dataTable = DBConnect.DataProvider.Instance.FilterSP(cbxChon1.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last());
            dtDonHang.ItemsSource = dataTable.DefaultView;
            switch (cbxChon1.SelectedIndex.ToString())
            {

            }
        }
        private List<HOADON> List_HD()
        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            string query = "SELECT * FROM HOADONs";
            SQLiteCommand cmd = new SQLiteCommand(query, _con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            List<HOADON> HDs = new List<HOADON>();
            while (reader.Read())
            {
                HDs.Add(new HOADON()
                {
                    SOHD = reader.GetInt32(0),
                    NGHD = reader.GetDateTime(3),
                    TRIGIA = reader.GetInt32(4),
                });
            }
            _con.Close();
            return HDs;
        }
        string rdma_SOHD()
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = rand.Next(0, 10000).ToString();
            } while (check_SOHD(ma));
            return ma;
        }
        bool check_SOHD(string s)
        {

            foreach (HOADON x in List_HD())
            {
                if (x.SOHD.ToString() == s)
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
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Nhap_hoa_don nhap = new Nhap_hoa_don();
            nhap.MaKH.Text = rdma_MAKH();
            nhap.SoHD.Text = rdma_SOHD();
            nhap.ShowDialog();
        }
    }
}
