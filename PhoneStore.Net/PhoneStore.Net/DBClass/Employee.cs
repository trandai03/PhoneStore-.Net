using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Net.DBClass
{
    internal class Employee
    {
        public string MAND { get; set; }

        public string TENND { get; set; }

        public DateTime NGSINH { get; set; }

        public string GIOITINH { get; set; }

        public string SDT { get; set; }

        public string DIACHI { get; set; }

        public string USERNAME { get; set; }

        public string PASS { get; set; }
        public bool QTV {  get; set; }
        public string AVA {  get; set; }
        public string MAIL {  get; set; }

        public Employee() { }
        DBConnect db = new DBConnect();
        public DataTable hienThiSanPham()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT [MAND] , [TENND] ,[NGSINH] , [GIOITINH] , [SDT], [SIZE], [DIACHI], [MAIL], [QTV] FROM NGUOIDUNGS";
            dt = db.Sql_select(sql);
            return dt;
        }
    }
}
