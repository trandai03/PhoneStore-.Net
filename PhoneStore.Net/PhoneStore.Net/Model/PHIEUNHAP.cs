using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Net.Model
{
    public partial class PHIEUNHAP
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUNHAP()
        {

            this.CTPNs = new HashSet<CTPN>();

        }


        public int MAPN { get; set; }

        public string MAND { get; set; }

        public System.DateTime NGAYNHAP { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<CTPN> CTPNs { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

    }
}
