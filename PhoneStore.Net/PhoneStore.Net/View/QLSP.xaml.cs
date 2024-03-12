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
using System.Data;
using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
using static PhoneStore.Net.DBClass.DBConnect;
using System.Collections.ObjectModel;
using PhoneStore.Net.ViewModel;
using System.Reflection;
using Xamarin.Forms;

namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for QLSP.xaml
    /// </summary>
    public partial class QLSP 
    {
        SANPHAM sp = new SANPHAM();
        
        


        private void _LoadCsCommand(QLSP p)
        {

        }
        private void _SearchCommand(QLSP p)
        {

        }
        private void LoadData()
        {
            try
            {
                string query = "SELECT MASP, TENSP,GIA,SL,LOAISP,SIZE FROM SANPHAMs";
                DataTable dataTable = DBConnect.DataProvider.Instance.Sql_select(query);
                dtSanPham.ItemsSource = dataTable.DefaultView;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadData1()

        {
            string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
            _con.Open();
            string query = "SELECT MASP, TENSP,GIA,SL,LOAISP,SIZE FROM SANPHAMs";
            SQLiteCommand cmd = new SQLiteCommand(query,_con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            List<SANPHAM> SANPHAMS  = new List<SANPHAM>();
            while (reader.Read())
            {
                SANPHAMS.Add(new SANPHAM()
                {
                    MASP = reader.GetString(0),
                    TENSP = reader.GetString(1),
                    GIA = reader.GetInt32(2),
                    SL = reader.GetInt32(3),
                    LOAISP = reader.GetString(4),
                    SIZE = reader.GetString(5),
                });
            }

            dtSanPham.ItemsSource = SANPHAMS;
        }

     
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewProduct newProduct = new NewProduct();
            newProduct.ShowDialog();
            newProduct.MaSp.Text = rdma();
            LoadData();
        }
        bool check(string m)
        {
            foreach (SANPHAM temp in DataProvider.Instance.selectQLSP())
            {
                if (temp.MASP == m)
                    return true;
            }
            return false;
        }
        string rdma()
        {
            string ma;
            do
            {
                Random rand = new Random();
                ma = "PD" + rand.Next(0, 10000).ToString();
            } while (check(ma));
            return ma;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try{
                if (txbSearch.Text != "")
                {
                    DataTable dataTable = DBConnect.DataProvider.Instance.Sql_selectSearch(txbSearch.Text);
                    dtSanPham.ItemsSource = dataTable.DefaultView;
                }
                else
                    LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }

           
        }

        private void dtSanPham_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        
    } 
}

