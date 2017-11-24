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
using MusicStoreMobile.Droid.Extensions;
using MvvmCross.Core.ViewModels;

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

        private IMenuItem _actionMenuItem;

        public bool IsShowTitle { get; set; }

        private bool _isSearch;
        public bool IsSearch
        {
            get
            {
                return _isSearch;
            }
            set
            {
                _isSearch = value;

                var searchView = (_actionMenuItem?.ActionView as Android.Support.V7.Widget.SearchView);
                if (searchView != null)
                {
                    searchView.MaxWidth = int.MaxValue;
                    searchView.SetOnSearchClickListener(new OnSearchClickListener(this));
                    searchView.SetOnCloseListener(new OnCloseListener(this));
                    searchView.SetOnQueryTextListener(new OnQueryTextListener(this));
                }
            }
        }

        private class OnQueryTextListener : Java.Lang.Object, SearchView.IOnQueryTextListener
        {
            private TopNavigationView _topNavigationView;
            public OnQueryTextListener(TopNavigationView topNavigationView)
            {
                _topNavigationView = topNavigationView;
            }

            public bool OnQueryTextChange(string newText)
            {
                (_topNavigationView._actionIconPropertyHelper.Action as MvxCommand<string>)?.Execute(newText);
                return true;
            }

            public bool OnQueryTextSubmit(string query)
            {
                (_topNavigationView._actionIconPropertyHelper.Action as MvxCommand<string>)?.Execute(query);
                return true;
            }
        }

        private class OnCloseListener : Java.Lang.Object, SearchView.IOnCloseListener
        {
            private TopNavigationView _topNavigationView;
            public OnCloseListener(TopNavigationView topNavigationView)
            {
                _topNavigationView = topNavigationView;
            }
            public bool OnClose()
            {
                if (_topNavigationView != null)
                {
                    _topNavigationView.ViewModel.IsShowTitle.Value = true;
                    _topNavigationView.ParentActivity?.SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
                    _topNavigationView.ParentActivity?.SupportActionBar?.SetDisplayShowHomeEnabled(true);
                }
                return false;
            }
        }

        private class OnSearchClickListener : Java.Lang.Object, View.IOnClickListener
        {
            private TopNavigationView _topNavigationView;
            public OnSearchClickListener(TopNavigationView topNavigationView)
            {
                _topNavigationView = topNavigationView;
            }
            public void OnClick(View v)
            {
                if (_topNavigationView != null)
                {
                    _topNavigationView.ViewModel.IsShowTitle.Value = false;
                    _topNavigationView.ParentActivity?.SupportActionBar?.SetDisplayHomeAsUpEnabled(false);
                    _topNavigationView.ParentActivity?.SupportActionBar?.SetDisplayShowHomeEnabled(false);
                }
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;

            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _topNavigationView = view.FindViewById<Toolbar>(Resource.Id.top_navigation_view);

            _homeIconPropertyHelper = new TopNavigationViewIconPropertyHelper();
            _actionIconPropertyHelper = new TopNavigationViewIconPropertyHelper();

            ParentActivity?.SetSupportActionBar(_topNavigationView);
            ParentActivity?.SupportActionBar?.SetDisplayShowTitleEnabled(false);

            ParentActivity?.SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
            ParentActivity?.SupportActionBar?.SetDisplayShowHomeEnabled(true);

            _homeIconPropertyHelper.OnTypeChanged -= HomeIconTypeChanged;
            _homeIconPropertyHelper.OnTypeChanged += HomeIconTypeChanged;
            HomeIconTypeChanged(_homeIconPropertyHelper, EventArgs.Empty);

            var bindingSet = this.CreateBindingSet<TopNavigationView, TopNavigationViewModel>();
            bindingSet.Bind(_homeIconPropertyHelper).For(h => h.Type).To(vm => vm.HomeIconType);
            bindingSet.Bind(_homeIconPropertyHelper).For(h => h.Action).To(vm => vm.HomeIconCommand);
            bindingSet.Bind(_actionIconPropertyHelper).For(h => h.Type).To(vm => vm.ActionIconType);
            bindingSet.Bind(_actionIconPropertyHelper).For(h => h.Action).To(vm => vm.ActionIconCommand);

            bindingSet.Bind(this).For(h => h.IsSearch).To(vm => vm.IsSearch);

            bindingSet.Apply();

            if (savedInstanceState == null)
            {
                
            }

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(ViewModel.IsSearch.Value ? Resource.Menu.top_navigation_search_menu : Resource.Menu.top_navigation_menu, menu);
            _actionMenuItem = menu.FindItem(Resource.Id.i_action);

            IsSearch = _isSearch;

            _actionIconPropertyHelper.OnTypeChanged -= ActionIconTypeChanged;
            _actionIconPropertyHelper.OnTypeChanged += ActionIconTypeChanged;
            ActionIconTypeChanged(_actionIconPropertyHelper, EventArgs.Empty);

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
                if (_isSearch)
                {
                    var searchView = (_actionMenuItem?.ActionView as Android.Support.V7.Widget.SearchView);
                    if (searchView != null)
                    {
                        var searchButton = searchView.FindViewById<Android.Widget.ImageView>(Resource.Id.search_button);
                        if (searchButton != null)
                        {
                            searchButton.SetImageDrawable(helper.MenuIcon);
                        }
                    }
                }
                else
                {
                    _actionMenuItem?.SetIcon(helper.MenuIcon);
                }
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