using Acr.UserDialogs;
using MusicStoreMobile.Core.Converters;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModels.Auth;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels.Preferences
{
    public class PreferencesViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthService _authService;
        private readonly IUserDialogs _userDialogs;

        private readonly INavigationViewModelManager _navigationViewModelManager;
        private readonly ITopNavigationViewModelService _topNavigationViewModelService;

        public PreferencesViewModel(IMvxNavigationService navigationService, IAuthService authService, IUserDialogs userDialogs, INavigationViewModelManager navigationViewModelManager, ITopNavigationViewModelService topNavigationViewModelService)
        {
            _navigationService = navigationService;
            _authService = authService;
            _userDialogs = userDialogs;

            _navigationViewModelManager = navigationViewModelManager;
            _topNavigationViewModelService = topNavigationViewModelService;

            ShowAddAlbumViewModelCommand = new MvxCommand(() => 
            {

            });
            ShowAddArtistViewModelCommand = new MvxCommand(() =>
            {

            });
            ShowAddGenreViewModelCommand = new MvxCommand(() => 
            {

            });
            ShowAddPlaylistViewModelCommand = new MvxCommand(() => 
            {

            });
            ShowAddSongViewModelCommand = new MvxCommand(() => 
            {

            });

            LogOutCommand = new MvxCommand( () =>
            {
                if (!IsTaskExecutedValueConverter.Convert(LogOutTask.Value))
                {
                    LogOutTask.Value = NotifyTaskCompletion.Create(AttemptLogOutAsync);
                }
            });
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        // MVVM Properties

        public readonly INC<INotifyTaskCompletion> LogOutTask = new NC<INotifyTaskCompletion>();

        // MVVM Commands
        public IMvxCommand ShowAddAlbumViewModelCommand { get; private set; }
        public IMvxCommand ShowAddArtistViewModelCommand { get; private set; }
        public IMvxCommand ShowAddGenreViewModelCommand { get; private set; }
        public IMvxCommand ShowAddPlaylistViewModelCommand { get; private set; }
        public IMvxCommand ShowAddSongViewModelCommand { get; private set; }

        public IMvxCommand LogOutCommand { get; private set; }

        // Private methods

        private async Task AttemptLogOutAsync()
        {
            _userDialogs.ShowLoading("Logout");
            var serviceResult = await _authService.Logout();

            await _navigationViewModelManager.Close<AudioPlayerViewModel>();
            await _navigationViewModelManager.Close<BottomNavigationViewModel>();

            await _topNavigationViewModelService.Close();
            await _navigationService.Navigate<LoginViewModel>();

            _userDialogs.HideLoading();
        }
    }
}
