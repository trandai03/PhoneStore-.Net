using MaterialDesignThemes.Wpf;
using PhoneStore.Net.Model;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public static UserSetting userSetting = new UserSetting();

        public HomePage()
        {
            InitializeComponent();
        }

<<<<<<< HEAD
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting(this);
            setting.Show();

        }

        public void RefreshDisplay()
        {
            this.NameDisplayTextBlock.Text = userSetting.Name;
            this.UserAvatar.Fill = new ImageBrush(userSetting.Image);
        }
=======
        
>>>>>>> ccfd2c635ddaa684b07c8b2fba9486d359775145
    }
}
