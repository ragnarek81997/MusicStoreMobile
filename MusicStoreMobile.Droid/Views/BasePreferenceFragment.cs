using System;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MusicStoreMobile.Droid.Controls;
using Android.Runtime;
using MusicStoreMobile.Droid.Helpers;
using MvvmCross.Droid.Support.V7.Preference;

namespace MusicStoreMobile.Droid.Views
{
    public abstract class BasePreferenceFragment : MvxPreferenceFragmentCompat, IKeyEventListener, IOnBackPressedListener
    {
        public MvxAppCompatActivity ParentActivity
        {
            get
            {
                return (MvxAppCompatActivity)Activity;
            }
        }

        protected BasePreferenceFragment()
        {
            RetainInstance = true;
        }

        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            this.AddPreferencesFromResource(PreferenceId);
        }

        public override void OnPause()
        {
            base.OnPause();
        }

        protected abstract int PreferenceId { get; }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
        }

        #region IKeyEventListener events
        public virtual bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            return true;
        }

        public virtual bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            return true;
        }

        public virtual bool OnKeyLongPress([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            return true;
        }

        public virtual bool OnKeyMultiple([GeneratedEnum] Keycode keyCode, int repeatCount, KeyEvent e)
        {
            return true;
        }

        public virtual bool OnKeyShortcut([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            return true;
        }

        #endregion IKeyEventListener events

        public virtual void OnBackPressed()
        {
            KeyboardUtils.HideKeyboard(ParentActivity);
        }
    }

    public abstract class BasePreferenceFragment<TViewModel> : BasePreferenceFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
