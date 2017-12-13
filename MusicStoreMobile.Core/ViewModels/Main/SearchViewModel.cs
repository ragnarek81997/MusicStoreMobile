using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MusicStoreMobile.Core.ViewModels.Preferences;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels.Main
{

    public class SearchViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ITopNavigationViewModelService _topNavigationViewModelService;
        private readonly IBottomNavigationViewModelService _bottomNavigationViewModelService;

        private readonly ISongService _songService;
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;
        private readonly IPlaylistService _playlistService;

        public SearchViewModel(IMvxNavigationService navigationService, ITopNavigationViewModelService topNavigationViewModelService, IBottomNavigationViewModelService bottomNavigationViewModelService, ISongService songService, IArtistService artistService, IAlbumService albumService, IPlaylistService playlistService)
        {
            _navigationService = navigationService;
            _topNavigationViewModelService = topNavigationViewModelService;
            _bottomNavigationViewModelService = bottomNavigationViewModelService;

            _songService = songService;
            _artistService = artistService;
            _albumService = albumService;
            _playlistService = playlistService;
    }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            _topNavigationViewModelService.Show(new TopNavigationViewModel.PrepareModel()
            {
                Title = "Search",
                ActionIconType = Enums.TopNavigationViewIconType.Search,
                IsSearch = true,
                ActionIconCommand = new MvxCommand<string>(_=> 
                {
                    if (SearchQuery.Value != _)
                    {
                        SearchQuery.Value = _;
                        if (!string.IsNullOrWhiteSpace(_))
                        {
                            Task.Run(async () =>
                            {
                                var songsResult = await _songService.GetMany(SearchQuery.Value, 0, 5);
                                if (songsResult.Success && SearchQuery.Value == _)
                                {
                                    Songs.Value = new ObservableCollection<SongViewModel>(songsResult.Result?.Select(s => new SongViewModel(s)));
                                }
                            });

                            Task.Run(async () =>
                            {
                                var artistsResult = await _artistService.GetMany(SearchQuery.Value, 0, 5);
                                if (artistsResult.Success && SearchQuery.Value == _)
                                {
                                    Artists.Value = new ObservableCollection<ArtistViewModel>(artistsResult.Result?.Select(a => new ArtistViewModel(a)));
                                }
                            });

                            Task.Run(async () =>
                            {
                                var albumsResult = await _albumService.GetMany(SearchQuery.Value, 0, 5);
                                if (albumsResult.Success && SearchQuery.Value == _)
                                {
                                    Albums.Value = new ObservableCollection<AlbumViewModel>(albumsResult.Result?.Select(a => new AlbumViewModel(a)));
                                }
                            });

                            Task.Run(async () =>
                            {
                                var playlistsResult = await _playlistService.GetMany(SearchQuery.Value, 0, 5);
                                if (playlistsResult.Success && SearchQuery.Value == _)
                                {
                                    Playlists.Value = new ObservableCollection<PlaylistViewModel>(playlistsResult.Result?.Select(p => new PlaylistViewModel(p)));
                                }
                            });
                        }
                        else
                        {
                            Songs.Value = new ObservableCollection<SongViewModel>();
                            Artists.Value = new ObservableCollection<ArtistViewModel>();
                            Albums.Value = new ObservableCollection<AlbumViewModel>();
                            Playlists.Value = new ObservableCollection<PlaylistViewModel>();
                        }
                    }
                }),
                SearchQuery = SearchQuery.Value
            });
            _bottomNavigationViewModelService.CheckItem(Enums.BottomNavigationViewCheckedItemType.Search);
        }

        // MVVM Properties

        public readonly INC<string> SearchQuery = new NC<string>("");

        public INC<ObservableCollection<SongViewModel>> Songs = new NC<ObservableCollection<SongViewModel>>(new ObservableCollection<SongViewModel>());
        public INC<ObservableCollection<ArtistViewModel>> Artists = new NC<ObservableCollection<ArtistViewModel>>(new ObservableCollection<ArtistViewModel>());
        public INC<ObservableCollection<AlbumViewModel>> Albums = new NC<ObservableCollection<AlbumViewModel>>(new ObservableCollection<AlbumViewModel>());
        public INC<ObservableCollection<PlaylistViewModel>> Playlists = new NC<ObservableCollection<PlaylistViewModel>>(new ObservableCollection<PlaylistViewModel>());

        // MVVM Commands

        // Private methods
    }
}
