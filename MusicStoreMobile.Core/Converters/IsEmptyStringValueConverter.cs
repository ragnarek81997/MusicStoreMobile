using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace MusicStoreMobile.Core.Converters
{
    public class IsEmptyStringValueConverter : MvxValueConverter<string, bool>
    {
        protected override bool Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
