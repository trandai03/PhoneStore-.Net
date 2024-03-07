using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneStore.Net.DBClass;
namespace PhoneStore.Net.DBClass
{
    internal class sanPham
    {
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
            string sql = "select * from sanphams";
            dt = db.Sql_select(sql);
            return dt;
        }
    }
}
