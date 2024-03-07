using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Net.DBClass
{
    internal class DBConnect
    {
        string chuoiketnoi = string.Format(@"Data Source={0}\QLDT.db;Version=3;", System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug")));
        public DataTable Sql_select(string sql_querry)
        {
            SQLiteConnection _con = new SQLiteConnection(chuoiketnoi);
            _con.Open();
            DataTable dt = new DataTable();
            SQLiteCommand cmd = new SQLiteCommand(sql_querry, _con);
            SQLiteDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            _con.Close();
            return dt;
        }
        public DBConnect() { }
    }
}
