using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace PhoneStore.Net.Controller
{
    internal class DatabaseService
    {
        SQLiteConnection conn;

        string USERQUERYSTRING = @"SELECT * FROM user WHERE username = $username AND password = $password";

        public DatabaseService()
        {
            conn = new SQLiteConnection("Data Source=C:\\phonestore.db");
            conn.Open();
        }

        public bool checkUser(string username, string password)
        {
            var command = conn.CreateCommand();
            command.CommandText = USERQUERYSTRING;
            command.Parameters.AddWithValue("$username", username);
            command.Parameters.AddWithValue("$password", password);

            var result = command.ExecuteScalar();

            if(result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
