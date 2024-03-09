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
namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for QLSP.xaml
    /// </summary>
    public partial class QLSP : Page
    {
        SANPHAM sp =  new SANPHAM();

        //string _localLink = ;
        public QLSP()
        {
            InitializeComponent();
            loadThongTin();
        }



        
        public void createConection()
        {
            //string _strConnect = "Data Source=./QLDT.db;Version=3;";
            
            //_con.ConnectionString = str;
            
            
        }
        
        public void closeConnection()
        {
            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
        private void loadThongTin()
        {
            dtSanPham.Columns.Clear();
            dtSanPham.ItemsSource = sp.hienThiSanPham().DefaultView;
            

        }
    }
}
