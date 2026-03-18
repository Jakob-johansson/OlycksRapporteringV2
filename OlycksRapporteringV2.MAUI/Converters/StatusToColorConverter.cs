using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting;
using System.Text;

namespace OlycksRapporteringV2.MAUI.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targeType, object paramter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                "Approved" => Color.FromArgb("#1A3A1A"),
                "Denied" => Color.FromArgb("#3A1A1A"),
                _ => Color.FromArgb("#2A2D38")
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();

    }
}
