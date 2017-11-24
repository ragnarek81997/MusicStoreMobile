using System;
using System.Collections.Generic;
using System.Linq;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MusicStoreMobile.Core.ViewModels;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MvvmCross.Droid.Views.Attributes;
using Com.Ittianyu.Bottomnavigationviewex;
using MusicStoreMobile.Droid.Helpers;
using MvvmCross.Core.ViewModels;
using MusicStoreMobile.Core.Enums;
using MvvmCross.Binding.BindingContext;

namespace MusicStoreMobile.Droid.Views.Navigation
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.bottom_navigation_frame, false)]
    [Register(nameof(BottomNavigationView))]
    public class BottomNavigationView : BaseFragment<BottomNavigationViewModel>, Android.Support.Design.Widget.BottomNavigationView.IOnNavigationItemSelectedListener, Android.Support.Design.Widget.BottomNavigationView.IOnNavigationItemReselectedListener
    {
        protected override int FragmentId => Resource.Layout.BottomNavigationView;

        private BottomNavigationViewEx _bottomNavigationView;

        private BottomNavigationViewCheckedItemType _checkedItem;
        public BottomNavigationViewCheckedItemType CheckedItem
        {
            get
            {
                return _checkedItem;
            }
            set
            {
                _checkedItem = value;
                switch (_checkedItem)
                {
                    case BottomNavigationViewCheckedItemType.Home:
                        _bottomNavigationView?.Menu?.FindItem(Resource.Id.i_home)?.SetChecked(true);
                        break;
                    case BottomNavigationViewCheckedItemType.Search:
                        _bottomNavigationView?.Menu?.FindItem(Resource.Id.i_search)?.SetChecked(true);
                        break;
                    case BottomNavigationViewCheckedItemType.Library:
                        _bottomNavigationView?.Menu?.FindItem(Resource.Id.i_library)?.SetChecked(true);
                        break;
                    default:
                        _bottomNavigationView?.Menu?.FindItem(Resource.Id.i_unchecked)?.SetChecked(true);
                        break;
                }
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _bottomNavigationView = view.FindViewById<BottomNavigationViewEx>(Resource.Id.bottom_navigation_view);
            _bottomNavigationView?.SetOnNavigationItemSelectedListener(this);
            _bottomNavigationView?.SetOnNavigationItemReselectedListener(this);

            _bottomNavigationView?.Menu?.FindItem(Resource.Id.i_unchecked)?.SetChecked(true);
            var uncheckedItem = _bottomNavigationView?.FindViewById(Resource.Id.i_unchecked);
            if (uncheckedItem != null)
            {
                uncheckedItem.Visibility = ViewStates.Gone;
            }

            _bottomNavigationView?.EnableShiftingMode(false);

            var bindingSet = this.CreateBindingSet<BottomNavigationView, BottomNavigationViewModel>();
            bindingSet.Bind(this).For(h => h.CheckedItem).To(vm => vm.CheckedItem);
            bindingSet.Apply();

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
            _bottomNavigationView?.Dispose();
            base.OnDestroy();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.i_home:
                    ViewModel.ShowHomeViewModelCommand.Execute(null);
                    break;
                case Resource.Id.i_search:
                    ViewModel.ShowSearchViewModelCommand.Execute(null);
                    break;
                case Resource.Id.i_library:
                    ViewModel.ShowLibraryViewModelCommand.Execute(null);
                    break;
            }
            return true;
        }
        public void OnNavigationItemReselected(IMenuItem item)
        {
        }
    }
}