using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V7.AppCompat;
using MusicStoreMobile.Core.ViewModels.Auth;
using Android.Widget;
using MusicStoreMobile.Droid.Helpers;
using MvvmCross.Binding.BindingContext;
using Android.Runtime;
using MvvmCross.Droid.Support.V4;
using Android.OS;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Support.V7.App;
using MusicStoreMobile.Core.ViewModels;
using MvvmCross.Droid.Views.Attributes;
using MusicStoreMobile.Droid.Controls;

namespace MusicStoreMobile.Droid.Views.Auth
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register(nameof(LoginView))]
    public class LoginView : BaseFragment<LoginViewModel>
    {
        private EditText emailInput;
        private EditText passwordInput;

        private FocusablePropertyHelper _focusablePropertyHelper;

        protected override int FragmentId => Resource.Layout.LoginView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _focusablePropertyHelper = new FocusablePropertyHelper(ParentActivity);
            this.CreateBinding(_focusablePropertyHelper)
                        .For(h => h.FocussedName)
                        .To<LoginViewModel>(x => x.FocusName)
                        .OneWay()
                        .Apply();

            emailInput = view.FindViewById<EditText>(Resource.Id.email_input);
            if (emailInput != null)
            {
                emailInput.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
                {
                    if (!e.HasFocus && !emailInput.HasFocus)
                    {
                        ViewModel.ValidateEmailCommand.Execute(null);
                    }
                };
            }

            passwordInput = view.FindViewById<EditText>(Resource.Id.password_input);
            if (passwordInput != null)
            {
                passwordInput.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
                {
                    if (!e.HasFocus && !passwordInput.HasFocus)
                    {
                        ViewModel.ValidatePasswordCommand.Execute(null);
                    }
                };
            }

            return view;
        }

        private void LogIn()
        {
            ViewModel.LogInCommand.Execute(null);
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnDestroy()
        {
            emailInput?.Dispose();
            passwordInput?.Dispose();

            base.OnDestroy();
        }

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Enter)
            {
                LogIn();
                return true;
            }

            return base.OnKeyUp(keyCode, e);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}

