using Acr.UserDialogs;
using MusicStoreMobile.Core.Converters;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Models;
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
        private readonly IAuthService _authService;
        private readonly IUserDialogs _userDialogs;

        private readonly IMvxNavigationService _navigationService;
        private readonly ITopNavigationViewModelService _topNavigationViewModelService;
        private readonly IBottomNavigationViewModelService _bottomNavigationViewModelService;

        public PreferencesViewModel(IMvxNavigationService navigationService, IAuthService authService, IUserDialogs userDialogs, ITopNavigationViewModelService topNavigationViewModelService, IBottomNavigationViewModelService bottomNavigationViewModelService)
        {
            _navigationService = navigationService;
            _topNavigationViewModelService = topNavigationViewModelService;
            _bottomNavigationViewModelService = bottomNavigationViewModelService;

            _authService = authService;
            _userDialogs = userDialogs;


            ShowAddAlbumViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ChangeAlbumViewModel>());

            ShowAddArtistViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ChangeArtistViewModel>());

            ShowAddGenreViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ChangeGenreViewModel>());

            ShowAddPlaylistViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ChangePlaylistViewModel>());

            ShowAddSongViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<ChangeSongViewModel>());

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

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            _topNavigationViewModelService.Show(new TopNavigationViewModel.PrepareModel()
            {
                Title = "Preferences",
                HomeIconType = Enums.TopNavigationViewIconType.Back,
                HomeIconCommand = new MvxCommand(async () => await _navigationService.Close(this) )
            }
            );
            _bottomNavigationViewModelService.CheckItem(Enums.BottomNavigationViewCheckedItemType.None);
        }

        // MVVM Properties

        public readonly INC<INotifyTaskCompletion> LogOutTask = new NC<INotifyTaskCompletion>();

        // MVVM Commands
        public IMvxAsyncCommand ShowAddAlbumViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowAddArtistViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowAddGenreViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowAddPlaylistViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowAddSongViewModelCommand { get; private set; }

        public IMvxCommand LogOutCommand { get; private set; }

        // Private methods

        private async Task AttemptLogOutAsync()
        {
            _userDialogs.ShowLoading("Logout");
            var serviceResult = await _authService.Logout();

            await _topNavigationViewModelService.Close();
            await _bottomNavigationViewModelService.Close();

            ClearStack.Execute(null);
            await _navigationService.Navigate<LoginViewModel>();

            _userDialogs.HideLoading();
        }
    }
}
