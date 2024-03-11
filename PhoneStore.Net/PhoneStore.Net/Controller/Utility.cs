using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhoneStore.Net.Controller
{
    internal class Utility
    {
        public static string ConvertToString(BitmapImage bitmapImage, string format)
        {
            if (bitmapImage == null)
                throw new ArgumentNullException(nameof(bitmapImage));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BitmapEncoder encoder = null;

                switch (format.ToLower())
                {
                    case "jpg":
                    case "jpeg":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case "png":
                        encoder = new PngBitmapEncoder();
                        break;
                    default:
                        throw new ArgumentException("Unsupported image format.");
                }

                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(memoryStream);

                byte[] bytes = memoryStream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }

        public static BitmapImage ConvertToBitmapImage(string imageString)
        {
            if (string.IsNullOrEmpty(imageString))
                throw new ArgumentNullException(nameof(imageString));

            byte[] bytes = Convert.FromBase64String(imageString);

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // Freeze the image for performance and thread safety
                return bitmapImage;
            }
        }
    }
}
