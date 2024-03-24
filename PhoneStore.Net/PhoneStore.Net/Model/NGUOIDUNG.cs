using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhoneStore.Net.Model
{
    public partial class NGUOIDUNG
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGUOIDUNG()
        {
            TENND = "";
            GIOITINH = "";
            SDT = "";
            DIACHI = "";
            MAIL = "";
            NGSINH = DateTime.Now;
            this.HOADONs = new HashSet<HOADON>();
            this.PHIEUNHAPs = new HashSet<PHIEUNHAP>();
        }
        public string MAND { get; set; }

        public string TENND { get; set; }

        public Nullable<System.DateTime> NGSINH { get; set; }

        public string GIOITINH { get; set; }

        public string SDT { get; set; }

        public string DIACHI { get; set; }

        public string USERNAME { get; set; }

        public string PASS { get; set; }

        public bool QTV { get; set; }

        public bool TTND { get; set; }

        public string AVA { get; set; }

        public string MAIL { get; set; }



        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<HOADON> HOADONs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<PHIEUNHAP> PHIEUNHAPs { get; set; }

    }
}
