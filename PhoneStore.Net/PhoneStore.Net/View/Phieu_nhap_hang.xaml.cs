using PhoneStore.Net.DBClass;
using PhoneStore.Net.Model;
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

namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for Phieu_nhap_hang.xaml
    /// </summary>
    public partial class Phieu_nhap_hang : Window
    {
        public Phieu_nhap_hang()
        {
            InitializeComponent();
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
        }

        public void XacNhan(object sender, RoutedEventArgs e)
        {
            PHIEUNHAP phieu = new PHIEUNHAP();
            phieu.MAPN = int.Parse(MaPN.Text);
            phieu.MAND = "1";
            phieu.NGAYNHAP = DateTime.Parse(Ngay.Text);
            DBConnect.DataProvider.Instance.NhapPhieu(phieu);
        }
    }
}
