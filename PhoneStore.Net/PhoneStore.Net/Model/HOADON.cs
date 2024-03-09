using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Net.Model
{
    public partial class HOADON
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOADON()
        {

            this.CTHDs = new HashSet<CTHD>();

        }


        public int SOHD { get; set; }

        public string MAND { get; set; }

        public string MAKH { get; set; }

        public System.DateTime NGHD { get; set; }

        public int TRIGIA { get; set; }

        public Nullable<int> KHUYENMAI { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<CTHD> CTHDs { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        //public virtual KHACHHANG KHACHHANG { get; set; }

    }
}
