using System.Globalization;
using Microsoft.Maui.Controls;

namespace AplicacionAvisosEscolares.Converters
{
    public class NoLeidoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}