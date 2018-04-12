using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Janus
{
    public class OutputConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value as string;
            if (data == null)
            {
                return value;
            }

            if (!double.TryParse(data, out var number))
            {
                return Brushes.White;
            }

            return number >= 0 ? Brushes.Green : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}