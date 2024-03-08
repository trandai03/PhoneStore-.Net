using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhoneStore.Net.DBClass
{
    internal class DBConnect
    {
  
        public static string StartupPath { get; }
        
        string databaseName ="QLDT.db";
        
        public DataTable Sql_select(string sql_querry)
        {
            SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
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
