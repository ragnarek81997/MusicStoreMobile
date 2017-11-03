using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using Nito.AsyncEx;

namespace MusicStoreMobile.Core.Converters
{
    public class IsTaskExecutedValueConverter : MvxValueConverter<INotifyTaskCompletion, bool>
    {
        public static bool Convert(INotifyTaskCompletion value)
        {
            return value != null && value.IsNotCompleted;
        }
        protected override bool Convert(INotifyTaskCompletion value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value);
        }
    }
}
