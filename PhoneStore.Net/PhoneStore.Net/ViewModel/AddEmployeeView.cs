using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data.SQLite;
using PhoneStore.Net.View;
using PhoneStore.Net.DBClass;
using System.Data;

namespace PhoneStore.Net.ViewModel
{
    public class AddEmployeeView
    {
        public string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
        private string _linkaddimage;
        public string linkaddimage { get => _linkaddimage; set { _linkaddimage = value; } }
        public ICommand AddNDCommand { get; set; }
        public ICommand AddImage { get; set; }
        public ICommand Closewd { get; set; }
        public ICommand Minimizewd { get; set; }

       
        public AddEmployeeView()
        {
            linkaddimage = _localLink + "/Resource/Image/addava.png";
            AddNDCommand = new RelayCommand<AddEmployee>((p) => true, (p) => _AddND(p));
            AddImage = new RelayCommand<ImageBrush>((p) => true, (p) => _AddImage(p));
            Closewd = new RelayCommand<AddEmployee>((p) => true, (p) => Close(p));
            Minimizewd = new RelayCommand<AddEmployee>((p) => true, (p) => Minimize(p));
            
        }
        
        void Close(AddEmployee p)
        {
            linkaddimage = _localLink + "/Resource/Image/addava.png";
            p.Close();
        }
        void Minimize(AddEmployee p)
        {
            p.WindowState = WindowState.Minimized;
        }
        void _AddImage(ImageBrush img)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";

            if (open.ShowDialog() == true)
            {
                if (open.FileName != "")
                    linkaddimage = open.FileName;
            };
            Uri fileUri = new Uri(linkaddimage);
            img.ImageSource = new BitmapImage(fileUri);
        }
        void _AddND(AddEmployee p)
        {
            MessageBoxResult h = System.Windows.MessageBox.Show("Bạn muốn thêm người dùng ?", "THÔNG BÁO", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
           
            
           
        }
    }
}
