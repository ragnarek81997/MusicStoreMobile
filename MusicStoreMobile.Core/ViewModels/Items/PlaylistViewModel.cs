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
    public class PlaylistViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public PlaylistViewModel(PlaylistModel param)
        {
            _navigationService = Mvx.Resolve<IMvxNavigationService>();

            Id.Value = param.Id;
            Owner.Value = param.Owner;
            Name.Value = param.Name;
            ArtId.Value = param.ArtId;

            Songs.Value = new ObservableCollection<SongViewModel>(param.Songs?.Select(_ => new SongViewModel(_)));

            Owner.Changed += (object sender, EventArgs e) => RaisePropertyChanged(() => Description);
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        // MVVM Properties

        public readonly INC<string> Id = new NC<string>("");
        public readonly INC<ApplicationUserModel> Owner = new NC<ApplicationUserModel>(new ApplicationUserModel());
        public readonly INC<string> Name = new NC<string>("");
        public readonly INC<string> ArtId = new NC<string>("");

        public readonly INC<ObservableCollection<SongViewModel>> Songs = new NC<ObservableCollection<SongViewModel>>(new ObservableCollection<SongViewModel>());
        public string Description
        {
            get
            {
                return "Owner: \"" + Owner.Value?.FirstName + " " + Owner.Value?.LastName + "\"";
            }
        }

        // MVVM Commands

        // Private methods


    }
}