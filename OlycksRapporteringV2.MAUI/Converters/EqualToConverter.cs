using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;

namespace OlycksRapporteringV2.MAUI.Converters
{
    public class EqualToConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var current = value?.ToString() ?? "";
            var target = parameter?.ToString() ?? "";

            return current == target
                ? Color.FromArgb("#E83B3B")
                : Color.FromArgb("#2A2D38");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
           => throw new NotImplementedException();

    }
}
