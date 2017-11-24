using Acr.UserDialogs;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.ViewModels.Auth;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MusicStoreMobile.Core.ViewModels.Preferences;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthService _authService;
        private readonly INavigationViewModelManager _navigationViewModelManager;
        private readonly ITopNavigationViewModelService _topNavigationViewModelService;
        

        public MainViewModel(IMvxNavigationService navigationService, IAuthService authService, INavigationViewModelManager navigationViewModelManager, ITopNavigationViewModelService topNavigationViewModelService)
        {
            _navigationService = navigationService;
            _authService = authService;
            _navigationViewModelManager = navigationViewModelManager;
            _topNavigationViewModelService = topNavigationViewModelService;

            ShowNavigationTopViewModelCommand = new MvxAsyncCommand(async () => await Task.Run(()=>{ }));
            ShowAudioPlayerViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<AudioPlayerViewModel>());
            ShowNavigationBottomViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<BottomNavigationViewModel>());
            ShowContentViewModelCommand = new MvxAsyncCommand(async () => 
            {
                var serviceResult = await _authService.Authorize();
                if (!serviceResult.Success)
                {
                    await _navigationViewModelManager.Close<AudioPlayerViewModel>();
                    await _navigationViewModelManager.Close<BottomNavigationViewModel>();

                    await _topNavigationViewModelService.Close();
                    await _navigationService.Navigate<LoginViewModel>();
                }
                else
                {
                    await _navigationService.Navigate<PreferencesViewModel>();
                    await _topNavigationViewModelService.Show(new TopNavigationViewModel.PrepareModel() { Title = "Preferences", IsSearch = true, HomeIconType = Enums.TopNavigationViewIconType.Back, ActionIconType = Enums.TopNavigationViewIconType.Preferences, ActionIconCommand = new MvxCommand<string>(
                        (query)=> 
                        {

                        })
                    });
                    
                    await _navigationService.Navigate<BottomNavigationViewModel>();
                }
            });
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        public override void ViewDestroy()
        {
            base.ViewDestroy();
        }

        // MVVM Properties

        // MVVM Commands
        public IMvxAsyncCommand ShowNavigationTopViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowAudioPlayerViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowNavigationBottomViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowContentViewModelCommand { get; private set; }

        // Private methods
    }
}
