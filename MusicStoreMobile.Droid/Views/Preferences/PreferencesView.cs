using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views.Attributes;
using MusicStoreMobile.Core.ViewModels;
using MusicStoreMobile.Core.ViewModels.Preferences;
using MvvmCross.Droid.Views.Fragments;
using Android.Support.V7.Preferences;
using MvvmCross.Binding.BindingContext;

namespace MusicStoreMobile.Droid.Views.Preferences
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register(nameof(PreferencesView))]
    public class PreferencesView : BasePreferenceFragment<PreferencesViewModel>
    {
        protected override int PreferenceId => Resource.Xml.PreferencesView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var addAlbumPreference = (Preference)this.FindPreference("add_album_pref");
            var addArtistPreference = (Preference)this.FindPreference("add_artist_pref");
            var addGenrePreference = (Preference)this.FindPreference("add_genre_pref");
            var addPlaylistPreference = (Preference)this.FindPreference("add_playlist_pref");
            var addSongPreference = (Preference)this.FindPreference("add_song_pref");
            var logOutPreference = (Preference)this.FindPreference("logout_pref");
            

            var bindingSet = this.CreateBindingSet<PreferencesView, PreferencesViewModel>();
            bindingSet.Bind(addAlbumPreference).For("Click").To(vm => vm.ShowAddAlbumViewModelCommand);
            bindingSet.Bind(addArtistPreference).For("Click").To(vm => vm.ShowAddArtistViewModelCommand);
            bindingSet.Bind(addGenrePreference).For("Click").To(vm => vm.ShowAddGenreViewModelCommand);
            bindingSet.Bind(addPlaylistPreference).For("Click").To(vm => vm.ShowAddPlaylistViewModelCommand);
            bindingSet.Bind(addSongPreference).For("Click").To(vm => vm.ShowAddSongViewModelCommand);
            bindingSet.Bind(logOutPreference).For("Click").To(vm => vm.LogOutCommand);
            bindingSet.Apply();

            return view;
        }
    }
}