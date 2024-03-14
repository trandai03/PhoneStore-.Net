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
using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
using PhoneStore.Net.View;
namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for QLNV.xaml
    /// </summary>
    /// 
    public partial class QLNV : Page
    {
        public QLNV()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                string query = "SELECT TENND , GIOITINH , DIACHI , SDT  , MAIL FROM NGUOIDUNGs";
                DataTable dataTable = DBConnect.DataProvider.Instance.Sql_select(query);
                dtNhanVien.ItemsSource = dataTable.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textSearch.Text != "")
                {
                    DataTable dataTable = DBConnect.DataProvider.Instance.SearchNV(textSearch.Text);
                    dtNhanVien.ItemsSource = dataTable.DefaultView;
                }
                else
                    LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
    }
   
}
