using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.App;
using MvvmCross.Droid.Views;

namespace MusicStoreMobile.Droid
{
    [Activity(
        MainLauncher = true,
        Icon = "@drawable/icon",
        Theme = "@style/Theme.Splash",
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            UserDialogs.Init(this);

			var dialogStyle = Resource.Style.acr_dialog_theme;
			AlertConfig.DefaultAndroidStyleId = dialogStyle;
			DatePromptConfig.DefaultAndroidStyleId = dialogStyle;
        }
    }
}