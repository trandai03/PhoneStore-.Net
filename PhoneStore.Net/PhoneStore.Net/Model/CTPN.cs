using PhoneStore.Net.DBClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Net.Model
{
    public partial class CTPN
    {

        public int MAPN { get; set; }

        public string MASP { get; set; }

        public int SL { get; set; }



        public virtual PHIEUNHAP PHIEUNHAP { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }

    }

}


