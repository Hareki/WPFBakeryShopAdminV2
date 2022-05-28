using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WPFBakeryShopAdminV2.Converters
{
    public class LargeUserImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string url = value as string;
            if (url == null) return null;

            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.CacheOption = BitmapCacheOption.OnLoad;
            source.UriSource = new Uri(url, UriKind.RelativeOrAbsolute);

            //    source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            source.EndInit();
            return source;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
