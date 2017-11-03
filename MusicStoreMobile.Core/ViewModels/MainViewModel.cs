using Acr.UserDialogs;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModels.Auth;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthService _authService;

        public MainViewModel(IMvxNavigationService navigationService, IAuthService authService)
        {
            _navigationService = navigationService;
            _authService = authService;

            ShowNavigationTopViewModelCommand = new MvxAsyncCommand(async () => await Task.Run(()=>{}));
            ShowNavigationBottomViewModelCommand = new MvxAsyncCommand(async () => await Task.Run(()=>{}));
            ShowContentViewModelCommand = new MvxAsyncCommand(async () => 
            {
                var serviceResult = await _authService.Authorize();
                if (!serviceResult.Success)
                {
                    await _navigationService.Navigate<LoginViewModel>();
                }
                else
                {
                    await Task.Run(() => { });
                }

                await _navigationService.Navigate<AudioPlayerViewModel>();
            });
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        // MVVM Properties

        // MVVM Commands
        public IMvxAsyncCommand ShowNavigationTopViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowNavigationBottomViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowContentViewModelCommand { get; private set; }

        // Private methods
    }
}
