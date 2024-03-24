using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for QLNH.xaml
    /// </summary>
    public partial class QLNH : Page
    {
        public QLNH()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                string query = "SELECT MAPN ,MAND , NGAYNHAP FROM PHIEUNHAPs ";
                DataTable dataTable = DBConnect.DataProvider.Instance.Sql_select(query);
                dtNhapHang.ItemsSource = dataTable.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textSearch.Text != "")
                {
                    DataTable dataTable = DBConnect.DataProvider.Instance.SearchNV(textSearch.Text);
                    dtNhapHang.ItemsSource = dataTable.DefaultView;
                }
                else
                    LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Phieu_nhap_hang phieu = new Phieu_nhap_hang();
            phieu.MaND.Text = MainWindow.user.MAND;
            phieu.ShowDialog();
        }
    }
}
