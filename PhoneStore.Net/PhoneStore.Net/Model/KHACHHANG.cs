using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Net.Model
{
    public partial class KHACHHANG
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {

            this.HOADONs = new HashSet<HOADON>();

        }


        public string MAKH { get; set; }

        public string HOTEN { get; set; }

        public string GIOITINH { get; set; }

        public string DCHI { get; set; }

        public string SDT { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<HOADON> HOADONs { get; set; }

    }
}
