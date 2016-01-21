using System;
using System.Globalization;
using Xamarin.Forms;


namespace CurrencyMVVM.Data.Utilities
{
    public class BoolenToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) return 0.0;

            bool boolen = (bool) value;
            return boolen ? 1.0 : 0.0;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double? d = value as double?;
            return d > 0.0;
        }
    }
}