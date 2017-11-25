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
using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Support.V7.App;
using Android.Views.InputMethods;

namespace MusicStoreMobile.Droid.Helpers
{
    public class FocusablePropertyHelper
    {
        private AppCompatActivity _activity;

        public FocusablePropertyHelper(AppCompatActivity activity)
        {
            var a = Android.App.Application.SynchronizationContext;

            _activity = activity;
        }

        // TODO - this should probably be a ViewModel-specific enum rather than a string
        private string _focussedName;
        public string FocussedName
        {
            get { return _focussedName; }
            set
            {
                _focussedName = value;
                var mapped = MapFocussedNameToControlName(_focussedName);
                var res = _activity.Resources.GetIdentifier(mapped, "id", _activity.PackageName);
                var view = _activity.FindViewById(res);
                if (view != null)
                {
                    view.RequestFocus();
                    if (string.IsNullOrWhiteSpace(_focussedName))
                    {
                        KeyboardUtils.HideKeyboard(_activity);
                    }
                    else
                    {
                        KeyboardUtils.ShowKeyboard(_activity);
                    }
                }
            }
        }

        private string MapFocussedNameToControlName(string value)
        {
            string focusId = value;
            if (string.IsNullOrWhiteSpace(value))
            {
                focusId = "main_container";
            }
            else
            {
                focusId = System.Text.RegularExpressions.Regex.Replace(focusId, "(?<=.)([A-Z])", "_$0", System.Text.RegularExpressions.RegexOptions.Compiled);
                focusId = focusId.ToLower() + "_input";
            }
            return focusId;
        }
    }
}