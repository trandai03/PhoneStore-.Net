using MaterialDesignThemes.Wpf;
using PhoneStore.Net.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using static PhoneStore.Net.View.QLSP;

namespace PhoneStore.Net.View
{
    public partial class Detail_product : Window
    {
        public string TenSPValue { get; set; }
        public string GiaSPValue { get; set; }
        public string MotaValue { get; set; }
        public string SLSPValue { get; set; }
        public string LoaiSPValue { get; set; }
        public string SizeValue { get; set; }
        public string MaSPValue { get; set; }
        public string HinhSPVALUE {  get; set; }

        public string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        public string tmp {  get; set; }
        public Detail_product()
        {
            InitializeComponent();
        }
        public void UpdateData()
        {
            TenSP.Text = TenSPValue;
            GiaSP.Text = GiaSPValue;
            Mota.Text = MotaValue;
            SLSP.Text = SLSPValue;
            LoaiSP.Text = LoaiSPValue;
            Size.Text = SizeValue;
            MaSP.Text = MaSPValue;
            tmp = _localLink + HinhSPVALUE;
            Uri fileUri = new Uri(tmp);
            HinhAnh.Source = new BitmapImage(fileUri);
        }
    }
       
}
