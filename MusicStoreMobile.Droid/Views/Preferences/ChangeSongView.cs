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
using MusicStoreMobile.Core.ViewModels.Preferences;
using MusicStoreMobile.Core.Models;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using MusicStoreMobile.Droid.Extensions;
using Android.Content.Res;

namespace MusicStoreMobile.Droid.Views.Preferences
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(ChangeSongView))]
    public class ChangeSongView : BaseFragment<ChangeSongViewModel>
    {
        private EditText nameInput;
        private AutoCompleteTextView genresInput;
        private AutoCompleteTextView artistsInput;
        private FocusablePropertyHelper _focusablePropertyHelper;

        protected override int FragmentId => Resource.Layout.ChangeSongView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _focusablePropertyHelper = new FocusablePropertyHelper(ParentActivity);
            this.CreateBinding(_focusablePropertyHelper)
                        .For(h => h.FocussedName)
                        .To<ChangeGenreViewModel>(x => x.FocusName)
                        .OneWay()
                        .Apply();

            nameInput = view.FindViewById<EditText>(Resource.Id.name_input);
            if (nameInput != null)
            {
                nameInput.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
                {
                    if (!e.HasFocus && !nameInput.HasFocus)
                    {
                        ViewModel.ValidateNameCommand.Execute(null);
                    }
                };
            }

            genresInput = view.FindViewById<AutoCompleteTextView>(Resource.Id.genres_input);
            if (genresInput != null)
            {
                genresInput.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
                {
                    if (!e.HasFocus && !genresInput.HasFocus)
                    {
                        ViewModel.ValidateGenresCommand.Execute(null);
                    }
                };
            }

            artistsInput = view.FindViewById<AutoCompleteTextView>(Resource.Id.artists_input);
            if (artistsInput != null)
            {
                artistsInput.FocusChange += (object sender, View.FocusChangeEventArgs e) =>
                {
                    if (!e.HasFocus && !artistsInput.HasFocus)
                    {
                        ViewModel.ValidateArtistsCommand.Execute(null);
                    }
                };
            }

            return view;
        }

        private void Change()
        {
            ViewModel.ChangeCommand.Execute(null);
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnDestroy()
        {
            nameInput?.Dispose();

            base.OnDestroy();
        }

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Enter)
            {
                Change();
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

