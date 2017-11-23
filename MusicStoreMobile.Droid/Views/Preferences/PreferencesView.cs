using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views.Attributes;
using MusicStoreMobile.Core.ViewModels;
using MusicStoreMobile.Core.ViewModels.Preferences;
using MvvmCross.Droid.Views.Fragments;
using Android.Support.V7.Preferences;
using MvvmCross.Binding.BindingContext;

namespace MusicStoreMobile.Droid.Views.Preferences
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register(nameof(PreferencesView))]
    public class PreferencesView : BasePreferenceFragment<PreferencesViewModel>
    {
        protected override int PreferenceId => Resource.Xml.PreferenceView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var serverPreference = (EditTextPreference)this.FindPreference("pref_server");

            var bindingSet = this.CreateBindingSet<PreferencesView, PreferencesViewModel>();
            //bindingSet.Bind(serverPreference)
            //    .For(v => v.Text)
            //    .To(vm => vm.ServerAddress);
            bindingSet.Apply();

            return view;
        }
    }
}