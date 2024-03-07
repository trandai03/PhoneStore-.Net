using PhoneStore.Net.Controller;
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

namespace PhoneStore.Net.View
{
    /// <summary>
    /// Interaction logic for Dang_Nhap.xaml
    /// </summary>
    public partial class Dang_Nhap : Window
    {
        public Dang_Nhap()
        {
            InitializeComponent();
        }

        private void DangNhapClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            //DatabaseService db = new DatabaseService();

            /*
             if(db.checkUser(username, password))
            {
                HomePage homepage = new HomePage();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }
             */
            if (username == "admin")
            {
                HomePage homePage = new HomePage();
            }
        }
        }
    }
