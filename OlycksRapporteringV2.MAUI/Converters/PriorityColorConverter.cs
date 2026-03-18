using Microsoft.Maui.Controls;
using OlycksRapporteringV2.Domain.Enums;
using System.Globalization;

namespace OlycksRapporteringV2.MAUI.Converters
{
    public class PriorityColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Priority priority)
            {
                return priority switch
                {
                    Priority.Critical => Color.FromArgb("#E83B3B"),
                    Priority.High => Color.FromArgb("#F5A623"),
                    Priority.Medium => Color.FromArgb("#4A9EFF"),
                    Priority.Low => Color.FromArgb("#4CAF50"),
                    _ => Color.FromArgb("#8A8F9E")
                };
            }
            return Color.FromArgb("#8A8F9E");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}