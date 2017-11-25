using Android.Runtime;
using MusicStoreMobile.Core.ViewModels;
using MvvmCross.Droid.Views.Attributes;
using MusicStoreMobile.Core.ViewModels.Empty;

namespace MusicStoreMobile.Droid.Views.Empty
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.top_navigation_frame, false)]
    [Register(nameof(EmptyTopNavigationView))]
    public class EmptyTopNavigationView : BaseFragment<EmptyTopNavigationViewModel>
    {
        protected override int FragmentId => Resource.Layout.EmptyView;
    }
}