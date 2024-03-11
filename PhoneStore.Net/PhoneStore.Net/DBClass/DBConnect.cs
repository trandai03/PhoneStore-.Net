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
        public class DataProvider 
        {
            string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
            SQLiteConnection _con = new SQLiteConnection();
            private static DataProvider instance;
            public static DataProvider Instance
            {
                get
                {
                    if (instance == null)
                        instance = new DataProvider();
                    return DataProvider.instance;
                }
                private set { DataProvider.instance = value; }
            }
            
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

            public bool checkUser(string username, string password)
            {
                SQLiteConnection _con = new SQLiteConnection($"Data Source={databaseName};Version=3;");
                _con.Open();
                string USERQUERYSTRING = @"SELECT * FROM NGUOIDUNGs WHERE USERNAME = $username AND PASS = $password";
                var command = _con.CreateCommand();
                command.CommandText = USERQUERYSTRING;
                command.Parameters.AddWithValue("$username", username);
                command.Parameters.AddWithValue("$password", password);

                var result = command.ExecuteScalar();
                _con.Close();

                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static string StartupPath { get; }
        
        
        
        
        public DBConnect() { }
    }
}
