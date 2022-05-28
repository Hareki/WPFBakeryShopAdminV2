using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFBakeryShopAdminV2.Converters
{
    public class VisToZIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility vis = (Visibility)value;
            if (vis == Visibility.Hidden || vis == Visibility.Collapsed)
            {
                return 0;
            }
            else
            {
                return 10;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
