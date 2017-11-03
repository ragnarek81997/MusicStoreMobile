using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MusicStoreMobile.Core.ViewModels;
using Android.Content.Res;
using Android.Widget;
using Android.Content;
using MvvmCross.Platform;
using MvvmCross.Core.Navigation;
using System.Net;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views.Attributes;
using Android.Runtime;
using MusicStoreMobile.Droid.Controls;
using MusicStoreMobile.Droid.Helpers;

namespace MusicStoreMobile.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Theme = "@style/AppTheme")]
    [IntentFilter(new[] { Android.Content.Intent.ActionView }, DataScheme = "mvx",
        Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    [IntentFilter(new[] { Android.Content.Intent.ActionView }, DataScheme = "https", DataHost = "github.com",
        Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainView);

            if (bundle == null)
            {
                ViewModel.ShowContentViewModelCommand.Execute(null);
                ViewModel.ShowNavigationTopViewModelCommand.Execute(null);
                ViewModel.ShowNavigationBottomViewModelCommand.Execute(null);
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            if (!Mvx.CanResolve<IMvxNavigationService>()) return;

            var url = WebUtility.UrlDecode(intent.DataString);

            Mvx.Resolve<IMvxNavigationService>().Navigate(url);
        }

        #region listeners events

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            GetCurrentFragment<IKeyEventListener>(Resource.Id.content_frame)?.OnKeyUp(keyCode, e);
            return base.OnKeyUp(keyCode, e);
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            GetCurrentFragment<IKeyEventListener>(Resource.Id.content_frame)?.OnKeyDown(keyCode, e);
            return base.OnKeyDown(keyCode, e);
        }

        public override bool OnKeyLongPress([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            GetCurrentFragment<IKeyEventListener>(Resource.Id.content_frame)?.OnKeyLongPress(keyCode, e);
            return base.OnKeyLongPress(keyCode, e);
        }

        public override bool OnKeyMultiple([GeneratedEnum] Keycode keyCode, int repeatCount, KeyEvent e)
        {
            GetCurrentFragment<IKeyEventListener>(Resource.Id.content_frame)?.OnKeyMultiple(keyCode, repeatCount, e);
            return base.OnKeyMultiple(keyCode, repeatCount, e);
        }

        public override bool OnKeyShortcut([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            GetCurrentFragment<IKeyEventListener>(Resource.Id.content_frame)?.OnKeyShortcut(keyCode, e);
            return base.OnKeyShortcut(keyCode, e);
        }

        public override void OnBackPressed()
        {
            GetCurrentFragment<IOnBackPressedListener>(Resource.Id.content_frame)?.OnBackPressed();
            base.OnBackPressed();
        }

        private T GetCurrentFragment<T>(int id) where T: class
        {
            var currentFragment = SupportFragmentManager.FindFragmentById(id);
            if (currentFragment != null && currentFragment is T)
            {
                return currentFragment as T;
            }
            return null;
        }

        #endregion listeners events

    }
}

