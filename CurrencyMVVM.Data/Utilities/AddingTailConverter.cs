using System;
using System.Globalization;
using Xamarin.Forms;

namespace CurrencyMVVM.Data.Utilities
{
    public class AddingTailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value} {parameter}";
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}