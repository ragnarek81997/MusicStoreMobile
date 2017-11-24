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
using MvvmCross.Droid.Support.V7.Preference.Target;
using Android.Support.V7.Preferences;
using static Android.Support.V7.Preferences.Preference;
using System.Windows.Input;

namespace MusicStoreMobile.Droid.MvxBindings
{
    public class MvxPreferenceClickTargetBinding : MvxPreferenceValueTargetBinding
    {
        public MvxPreferenceClickTargetBinding(Preference preference)
            : base(preference) { }

        protected override void SetValueImpl(object target, object value)
        {
            var t = target as Preference;
            if (t != null)
            {
                t.OnPreferenceClickListener = new OnPreferenceClickListener(value as ICommand);
            }
        }

        private class OnPreferenceClickListener : Java.Lang.Object, IOnPreferenceClickListener
        {
            public ICommand Command {get; private set;}
            public OnPreferenceClickListener(ICommand command)
            {
                Command = command;
            }
            public bool OnPreferenceClick(Preference preference)
            {
                Command?.Execute(null);
                return true;
            }
        }

    }
}