using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OlycksRapporteringV2.MAUI.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object paramter, CultureInfo culture)
        
            => value is bool b && !b;
        public object ConvertBack(object value, Type targetType, object paramter, CultureInfo culture)
            => throw new NotImplementedException();

    }
}
