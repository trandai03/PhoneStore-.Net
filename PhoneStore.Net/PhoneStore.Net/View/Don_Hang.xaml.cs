using PhoneStore.Net.DBClass;
using System;
using System.Collections.Generic;
using System.Data;
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
        public Don_Hang()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT SOHD ,MAKH , NGHD , TRIGIA , KHUYENMAI    FROM HOADONs";
                DataTable dataTable = DBConnect.DataProvider.Instance.Sql_select(query);
                dtDonHang.ItemsSource = dataTable.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
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

        
    }
}
