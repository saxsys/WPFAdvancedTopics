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
            var number = value as double?;

            if (number == null)
            {
                return Brushes.White;
            }

            if (number >= 0)
            {
                return Brushes.Green;
            }
            else
            {
                return Brushes.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}