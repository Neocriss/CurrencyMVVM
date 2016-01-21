using System;
using System.Globalization;
using Xamarin.Forms;

namespace CurrencyMVVM.Data.Utilities
{
    public class AddingTailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                double dValue = (double) value;
                return $"{dValue:F2} {parameter}";
            }
            else if (value is decimal)
            {
                decimal mValue = (decimal) value;
                return $"{mValue:F2} {parameter}";
            }
            else return $"{value} {parameter}";
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}