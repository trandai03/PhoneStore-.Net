using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneStore.Net.DBClass;
using PhoneStore.Net;
namespace PhoneStore.Net.Model
{

    public partial  class SANPHAM
    {
        public virtual ICollection<CTHD> CTHDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<CTPN> CTPNs { get; set; }
        public SANPHAM()
        {

            this.CTHDs = new HashSet<CTHD>();

            this.CTPNs = new HashSet<CTPN>();

        }
        public string MASP { get; set; }

        public string TENSP { get; set; }

        public int GIA { get; set; }

        public string MOTA { get; set; }

        public string HINHSP { get; set; }

        public int SL { get; set; }

        public string LOAISP { get; set; }

        public string SIZE { get; set; }
        DBConnect db = new DBConnect();
        public DataTable hienThiSanPham()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT [MASP] , [TENSP] ,[GIA] , [SL] , [LOAISP], [SIZE] FROM SANPHAMs";
            dt = db.Sql_select(sql);
            return dt;
        }
    }

    
}
