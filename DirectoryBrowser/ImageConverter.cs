using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;
using System.Linq;

namespace DirectoryBrowser
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String path = value.ToString();

            DirectoryInfo info = new DirectoryInfo(path);

            var isDrive = Directory.GetLogicalDrives().Where(p => p.ToUpper().Equals(path.ToUpper())).FirstOrDefault();

            if (isDrive != null)
                return new BitmapImage(new Uri("pack://application:,,,/Images/Drive.png"));


            if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return new BitmapImage(new Uri("pack://application:,,,/Images/Folder.png"));
            }

            return new BitmapImage(new Uri("pack://application:,,,/Images/File.png"));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
