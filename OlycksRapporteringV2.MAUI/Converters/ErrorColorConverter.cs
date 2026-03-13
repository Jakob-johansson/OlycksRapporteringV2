using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Maui.Controls;

namespace OlycksRapporteringV2.MAUI.Converters
{
    public class ErrorColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool hasError && hasError)
                ? Color.FromArgb("#E83B3B") // En röd bakgrund om man matar in fel i någon entry
                : Color.FromArgb("#1A1D27");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        
            => throw new NotImplementedException();
        
    }
}

