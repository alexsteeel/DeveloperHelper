using System;
using System.Globalization;
using System.Windows.Data;

namespace ProjectManagementModule
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return targetType == typeof(bool)
                ? !(bool)value
                : throw new InvalidOperationException("The target must be boolean");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return targetType == typeof(bool)
                ? !(bool)value
                : throw new InvalidOperationException("The target must be boolean");
        }
    }
}
