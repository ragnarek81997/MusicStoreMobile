using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Converters;
using Android.Support.V4.Content;
using Plugin.MediaManager.Abstractions.Enums;

namespace MusicStoreMobile.Droid.Converters
{
    public class MediaPlayerStatusValueConverter : MvxValueConverter<MediaPlayerStatus, bool>
    {
        protected override bool Convert(MediaPlayerStatus value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case MediaPlayerStatus.Stopped:
                case MediaPlayerStatus.Paused:
                case MediaPlayerStatus.Failed:
                    return false;
                case MediaPlayerStatus.Loading:
                case MediaPlayerStatus.Playing:
                case MediaPlayerStatus.Buffering:
                    return true;
            }
            return false;
        }
    }
}