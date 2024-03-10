using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;

namespace PhoneStore.Net.View
{
    public partial class AddEmployee : Window
    {
        private SQLiteConnection con;
        private string databaseName = "..\\..\\bin\\Debug\\QLDT.db";
        public AddEmployee()
        {
            InitializeComponent();
            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            con = new SQLiteConnection($"Data Source = {databaseName}; Version=3;");
            con.Open();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO NGUOIDUNGs(MAND, TENND, NGSINH, SDT, DIACHI, MAIL, QTV, TTND) VALUES(@mand, @ten, @ngaysinh, @sdt, @diachi, @mail, @qtv, @ttnd)";
            SQLiteCommand command = new SQLiteCommand(query, con);
            command.Parameters.AddWithValue("@mand", MaND.Text);
            command.Parameters.AddWithValue("@ten", TenND.Text);
            command.Parameters.AddWithValue("@ngaysinh", NS.SelectedDate);
            command.Parameters.AddWithValue("@sdt", SDT.Text);
            command.Parameters.AddWithValue("@diachi", DC.Text);
            command.Parameters.AddWithValue("@mail", Mail.Text);
            command.Parameters.AddWithValue("@qtv", true);
            command.Parameters.AddWithValue("@ttnd", true);
            command.ExecuteNonQuery();
            MaND.Text = "";
            TenND.Text = "";
            NS.SelectedDate = null;
            SDT.Text = "";
            DC.Text = "";
            Mail.Text = "";
        }
    }
}

