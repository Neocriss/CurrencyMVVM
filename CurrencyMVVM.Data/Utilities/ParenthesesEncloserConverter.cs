﻿using System;
using System.Globalization;
using Xamarin.Forms;


namespace CurrencyMVVM.Data.Utilities
{
    public class ParenthesesEncloserConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value is decimal && (decimal)value == 0.0m)) return "...";

            return $"({value})";
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}