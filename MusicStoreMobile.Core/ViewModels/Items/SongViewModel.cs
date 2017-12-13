using MusicStoreMobile.Core.Controls;
using MusicStoreMobile.Core.Converters;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModels.Auth;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using MvvmCross.Platform;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels.Preferences
{
    public class SongViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public SongViewModel(SongModel param)
        {
            _navigationService = Mvx.Resolve<IMvxNavigationService>();

            Id.Value = param.Id;
            Name.Value = param.Name;
            ArtId.Value = param.ArtId;

            Links.Value = new ObservableCollection<LinkViewModel>(param.Links?.Select(_ => new LinkViewModel(_)));
            Genres.Value = new ObservableCollection<GenreViewModel>(param.Genres?.Select(_ => new GenreViewModel(_)));
            Artists.Value = new ObservableCollection<ArtistViewModel>(param.Artists?.Select(_ => new ArtistViewModel(_)));

            Artists.Changed += (object sender, EventArgs e) => RaisePropertyChanged(() => Description);
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        // MVVM Properties

        public readonly INC<string> Id = new NC<string>("");
        public readonly INC<string> Name = new NC<string>("");
        public readonly INC<string> ArtId = new NC<string>("");

        public readonly INC<ObservableCollection<LinkViewModel>> Links = new NC<ObservableCollection<LinkViewModel>>(new ObservableCollection<LinkViewModel>());
        public readonly INC<ObservableCollection<GenreViewModel>> Genres = new NC<ObservableCollection<GenreViewModel>>(new ObservableCollection<GenreViewModel>());
        public readonly INC<ObservableCollection<ArtistViewModel>> Artists = new NC<ObservableCollection<ArtistViewModel>>(new ObservableCollection<ArtistViewModel>());

        public string Description
        {
            get
            {
                return string.Join(", ", Artists.Value?.Select(_ => _.Name.Value));
            }
        }

        // MVVM Commands

        // Private methods


    }
}