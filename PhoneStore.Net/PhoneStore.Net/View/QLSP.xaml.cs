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

namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for QLSP.xaml
    /// </summary>
    public partial class QLSP : Page
    {
        SANPHAM sp = new SANPHAM();

        //string _localLink = ;
        public QLSP()
        {
            InitializeComponent();
            LoadData();
        }




        public void createConection()
        {
            //string _strConnect = "Data Source=./QLDT.db;Version=3;";

            //_con.ConnectionString = str;


        }

        public void closeConnection()
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
            string databaseName = "QLDT.db";
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

        private void ComboBox_SelectionChanged()
        {

        }

        

        private void EditNV(object sender, RoutedEventArgs e)
        {

        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewProduct newProduct = new NewProduct();
            newProduct.ShowDialog();
            LoadData();
        }
    } 
}

