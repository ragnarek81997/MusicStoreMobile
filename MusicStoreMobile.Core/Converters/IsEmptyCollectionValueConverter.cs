using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using System.Collections;

namespace MusicStoreMobile.Core.Converters
{
    public class IsEmptyCollectionValueConverter : MvxValueConverter<IList, bool>
    {
        protected override bool Convert(IList value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null || value.Count == 0 ? true : false;
        }
    }
}
