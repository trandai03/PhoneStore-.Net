using PhoneStore.Net.DBClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Net.Model
{
    public partial class CTHD
    {

        public int SOHD { get; set; }

        public string MASP { get; set; }

        public int SL { get; set; }



        public virtual HOADON HOADON { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }

    }

}


