using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OlycksRapporteringV2.MAUI.Converters
{
    public class StatusToIconConverter : IValueConverter
    {

        public object Convert(object  value, Type targetType, object paramter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                "Approved" => "✅",
                "Denied" => "❌",
                _ => "⏳"  // Pending
            };
        }
        public object ConvertBack(object value, Type targetType, object paramter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
