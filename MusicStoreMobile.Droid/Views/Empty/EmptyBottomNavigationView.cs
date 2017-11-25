using Android.Runtime;
using MusicStoreMobile.Core.ViewModels;
using MvvmCross.Droid.Views.Attributes;
using MusicStoreMobile.Core.ViewModels.Empty;

namespace MusicStoreMobile.Droid.Views.Empty
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.bottom_navigation_frame, false)]
    [Register(nameof(EmptyBottomNavigationView))]
    public class EmptyBottomNavigationView : BaseFragment<EmptyBottomNavigationViewModel>
    {
        protected override int FragmentId => Resource.Layout.EmptyView;
    }
}