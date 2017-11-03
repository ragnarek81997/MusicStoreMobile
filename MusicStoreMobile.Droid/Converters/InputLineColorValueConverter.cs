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

namespace MusicStoreMobile.Droid.Converters
{
    public class InputLineColorValueConverter : MvxValueConverter<string, Android.Content.Res.ColorStateList>
    {
        protected override Android.Content.Res.ColorStateList Convert(string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return ContextCompat.GetColorStateList(Application.Context, Resource.Color.input_color_valid);
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                return ContextCompat.GetColorStateList(Application.Context, Resource.Color.input_color_once);
            }
            else
            {
                return ContextCompat.GetColorStateList(Application.Context, Resource.Color.input_color_error);
            }
        }
    }
}