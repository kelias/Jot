/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Jarloo.Jot.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (string) value;

            var r = byte.Parse(color.Substring(0, 2), NumberStyles.HexNumber);
            var g = byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber);
            var b = byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber);

            return value == null ? null : new SolidColorBrush(Color.FromRgb(r, g, b));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}