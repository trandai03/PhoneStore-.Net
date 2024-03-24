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
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            
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

            foreach (HOADON x in DBConnect.DataProvider.Instance.List_HD())
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
            nhap.MaND.Text = MainWindow.user.MAND;
            nhap.ShowDialog();
            LoadData();
        }

        private void cbxChon1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxChon1.SelectedIndex != 0)
            {
                
                DataTable dataTable = DBConnect.DataProvider.Instance.FilterDH(cbxChon1.SelectedIndex);
                dtDonHang.ItemsSource = dataTable.DefaultView;
            }
            else
            {
                LoadData();
            }
        }

        
    }
}
