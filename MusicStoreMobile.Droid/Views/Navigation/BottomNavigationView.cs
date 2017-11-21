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
using MusicStoreMobile.Core.ViewModels;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MvvmCross.Droid.Views.Attributes;

namespace MusicStoreMobile.Droid.Views.Navigation
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.bottom_navigation_frame, false)]
    [Register(nameof(BottomNavigationView))]
    public class BottomNavigationView : BaseFragment<BottomNavigationViewModel>
    {
        protected override int FragmentId => Resource.Layout.BottomNavigationView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            if (savedInstanceState == null)
            {
                
            }

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}