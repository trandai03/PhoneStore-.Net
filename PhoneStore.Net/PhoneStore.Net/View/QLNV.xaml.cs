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
        string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
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
            addEmployee.MaND.Text = rdma(addEmployee);
            addEmployee.ShowDialog();
            LoadData();
        }
        bool check(string m, AddEmployee p)
        {
            SQLiteConnection con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            con.Open();
            string checkExistQuery = "SELECT COUNT(*) FROM NGUOIDUNGs WHERE MAND = @mand";
            SQLiteCommand checkExistCommand = new SQLiteCommand(checkExistQuery, con);
            checkExistCommand.Parameters.AddWithValue("@mand", p.MaND.Text);

            int dem = Convert.ToInt32(checkExistCommand.ExecuteScalar());
            checkExistCommand.ExecuteNonQuery();
            if (dem > 0) return true;
            else return false;
        }
        string rdma(AddEmployee p)
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "ND" + rand.Next(0, 10000).ToString();
            } while (check(ma, p));
            return ma;
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
