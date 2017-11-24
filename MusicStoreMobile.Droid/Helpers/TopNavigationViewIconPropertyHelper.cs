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
using System.Windows.Input;
using MusicStoreMobile.Core.Enums;
using Android.Content.Res;
using MusicStoreMobile.Droid.Extensions;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace MusicStoreMobile.Droid.Helpers
{
    public class TopNavigationViewIconPropertyHelper
    {
        public Android.Graphics.Drawables.Drawable MenuIcon { get; private set; }
        public event EventHandler OnTypeChanged;

        public TopNavigationViewIconPropertyHelper()
        {
            MenuIcon = new ColorDrawable(Color.Transparent).Mutate();
        }

        private TopNavigationViewIconType _type;
        public TopNavigationViewIconType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;

                switch (_type)
                {
                    case TopNavigationViewIconType.Preferences:
                        MenuIcon = AppCompatExtensions.GetDrawable(Resource.Drawable.ic_settings).Mutate();
                        break;
                    case TopNavigationViewIconType.Back:
                        MenuIcon = AppCompatExtensions.GetDrawable(Resource.Drawable.ic_keyboard_arrow_left).Mutate();
                        break;
                    case TopNavigationViewIconType.Done:
                        MenuIcon = AppCompatExtensions.GetDrawable(Resource.Drawable.ic_done).Mutate();
                        break;
                    case TopNavigationViewIconType.Search:
                        MenuIcon = AppCompatExtensions.GetDrawable(Resource.Drawable.ic_search).Mutate();
                        break;
                    default:
                        MenuIcon = new ColorDrawable(Color.Transparent).Mutate();
                        break;
                }
                MenuIcon.SetColorFilter(AppCompatExtensions.GetColor(Resource.Color.Black), Android.Graphics.PorterDuff.Mode.Multiply);

                OnTypeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private ICommand _action;
        public ICommand Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }
    }
}