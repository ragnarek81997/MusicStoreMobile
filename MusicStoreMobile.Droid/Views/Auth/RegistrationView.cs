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
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(RegistrationView))]
    public class RegistrationView : BaseFragment<RegistrationViewModel>
    {
        private EditText emailInput;
        private EditText firstNameInput;
        private EditText lastNameInput;
        private EditText passwordInput;
        private EditText birthDateInput;

        private FocusablePropertyHelper _focusablePropertyHelper;

        protected override int FragmentId => Resource.Layout.RegistrationView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

                _focusablePropertyHelper = new FocusablePropertyHelper(ParentActivity);
                this.CreateBinding(_focusablePropertyHelper)
                            .For(h => h.FocussedName)
                            .To<RegistrationViewModel>(x => x.FocusName)
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

            firstNameInput = view.FindViewById<EditText>(Resource.Id.first_name_input);
            if (firstNameInput != null)
            {
                firstNameInput.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
                {
                    if (!e.HasFocus && !firstNameInput.HasFocus)
                    {
                        ViewModel.ValidateFirstNameCommand.Execute(null);
                    }
                };
            }

            lastNameInput = view.FindViewById<EditText>(Resource.Id.last_name_input);
            if (lastNameInput != null)
            {
                lastNameInput.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
                {
                    if (!e.HasFocus && !lastNameInput.HasFocus)
                    {
                        ViewModel.ValidateLastNameCommand.Execute(null);
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

            birthDateInput = view.FindViewById<EditText>(Resource.Id.birth_date_input);
            if (birthDateInput != null)
            {
                birthDateInput.InputType = Android.Text.InputTypes.Null;
                birthDateInput.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
                {
                    if (e.HasFocus && birthDateInput.HasFocus)
                    {
                        ViewModel.ChangeBirthDateCommand.Execute(null);
                    }
                };
            }

            return view;
        }

        private void Register()
        {
            ViewModel.RegisterCommand.Execute(this);
        }

        public override void OnDestroy()
        {
            emailInput?.Dispose();
            firstNameInput?.Dispose();
            lastNameInput?.Dispose();
            passwordInput?.Dispose();

            //System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation.
            birthDateInput?.Dispose();

            base.OnDestroy();
        }

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Enter)
            {
                Register();
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

