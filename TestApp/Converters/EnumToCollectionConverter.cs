using System;
using System.Globalization;
using System.Windows.Data;

namespace TestApp.Converters
{
    public class EnumToCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : Enum.GetValues(value.GetType());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}