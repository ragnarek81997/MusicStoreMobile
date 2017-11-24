using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MusicStoreMobile.Core.ViewModels;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MvvmCross.Droid.Views.Attributes;
using Android.Support.V7.Widget;
using MvvmCross.Binding.BindingContext;
using MusicStoreMobile.Droid.Helpers;
using MusicStoreMobile.Core.Enums;

namespace MusicStoreMobile.Droid.Views.Navigation
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.top_navigation_frame, false)]
    [Register(nameof(TopNavigationView))]
    public class TopNavigationView : BaseFragment<TopNavigationViewModel>
    {
        protected override int FragmentId => Resource.Layout.TopNavigationView;

        private Toolbar _topNavigationView;
        private TopNavigationViewIconPropertyHelper _homeIconPropertyHelper;
        private TopNavigationViewIconPropertyHelper _actionIconPropertyHelper;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;

            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _topNavigationView = view.FindViewById<Toolbar>(Resource.Id.top_navigation_view);

            _homeIconPropertyHelper = new TopNavigationViewIconPropertyHelper();
            _actionIconPropertyHelper = new TopNavigationViewIconPropertyHelper();

            ParentActivity?.SetSupportActionBar(_topNavigationView);
            ParentActivity?.SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
            ParentActivity?.SupportActionBar?.SetDisplayShowHomeEnabled(true);

            ParentActivity?.SupportActionBar?.SetHomeAsUpIndicator(_homeIconPropertyHelper.MenuIcon);
            _homeIconPropertyHelper.OnTypeChanged -= HomeIconTypeChanged;
            _homeIconPropertyHelper.OnTypeChanged += HomeIconTypeChanged;

            var bindingSet = this.CreateBindingSet<TopNavigationView, TopNavigationViewModel>();
            bindingSet.Bind(_homeIconPropertyHelper).For(h => h.Type).To(vm => vm.HomeIconType);
            bindingSet.Bind(_homeIconPropertyHelper).For(h => h.Action).To(vm => vm.HomeIconCommand);
            bindingSet.Bind(_actionIconPropertyHelper).For(h => h.Type).To(vm => vm.ActionIconType);
            bindingSet.Bind(_actionIconPropertyHelper).For(h => h.Action).To(vm => vm.ActionIconCommand);
            bindingSet.Apply();

            if (savedInstanceState == null)
            {
                
            }

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.top_navigation_menu, menu);
            menu.FindItem(Resource.Id.i_action).SetIcon(_actionIconPropertyHelper.MenuIcon);

            _actionIconPropertyHelper.OnTypeChanged -= ActionIconTypeChanged;
            _actionIconPropertyHelper.OnTypeChanged += ActionIconTypeChanged;

            base.OnCreateOptionsMenu(menu, inflater);
        }

        private void HomeIconTypeChanged(object sender, EventArgs e)
        {
            if (sender is TopNavigationViewIconPropertyHelper helper)
            {
                ParentActivity?.SupportActionBar.SetHomeAsUpIndicator(helper.MenuIcon);
            }
        }

        private void ActionIconTypeChanged(object sender, EventArgs e)
        {
            if (sender is TopNavigationViewIconPropertyHelper helper)
            {
                _topNavigationView.Menu.FindItem(Resource.Id.i_action).SetIcon(_actionIconPropertyHelper.MenuIcon);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    if (_homeIconPropertyHelper.Type != TopNavigationViewIconType.None)
                        _homeIconPropertyHelper.Action?.Execute(null);
                    break;
                case Resource.Id.i_action:
                    if (_actionIconPropertyHelper.Type != TopNavigationViewIconType.None)
                        _actionIconPropertyHelper.Action?.Execute(null);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnDestroy()
        {
            _topNavigationView?.Dispose();
            base.OnDestroy();
        }
    }
}