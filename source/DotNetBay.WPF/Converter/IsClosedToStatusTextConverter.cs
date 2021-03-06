using System;
using System.Globalization;
using System.Windows.Data;

namespace DotNetBay.WPF.Converter
{
    public class IsClosedToStatusTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool) value)
                return "Closed";
            
            return "Valid";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((String) value).Equals("Closed"))
                return true;
            
            return false;
        }
    }
}