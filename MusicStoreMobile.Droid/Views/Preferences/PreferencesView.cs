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
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(PreferencesView))]
    public class PreferencesView : BaseFragment<PreferencesViewModel>
    {
        protected override int FragmentId => Resource.Layout.PreferencesView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}