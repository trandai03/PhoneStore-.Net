using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhoneStore.Net.Model
{
    public class UserSetting
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public BitmapImage Image { get; set; }
        public UserSetting() {
            Name = string.Empty;
            Gender = string.Empty;
            DOB = DateTime.Now;
            PhoneNumber = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
            Image = new BitmapImage(new Uri("pack://application:,,,/Resource/avatar/GoldGoat.jpg"));
        }
    }
}
