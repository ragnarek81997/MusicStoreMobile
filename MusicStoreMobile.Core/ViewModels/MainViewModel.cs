using Acr.UserDialogs;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.ViewModels.Auth;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Threading;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthService _authService;
        private readonly INavigationFragmentManager _navigationFragmentManager;

        public MainViewModel(IMvxNavigationService navigationService, IAuthService authService, INavigationFragmentManager navigationFragmentManager)
        {
            _navigationService = navigationService;
            _authService = authService;
            _navigationFragmentManager = navigationFragmentManager;

            ShowNavigationTopViewModelCommand = new MvxAsyncCommand(async () => await Task.Run(()=>{ }));
            ShowNavigationLeftViewModelCommand = new MvxAsyncCommand(async () => await Task.Run(() => { }));
            ShowNavigationBottomViewModelCommand = new MvxAsyncCommand(async () => await Task.Run(()=>{ }));
            ShowContentViewModelCommand = new MvxAsyncCommand(async () => 
            {
                var serviceResult = await _authService.Authorize();
                if (!serviceResult.Success)
                {
                    await _navigationService.Navigate<LoginViewModel>();
                }
                else
                {
                    await _navigationFragmentManager.Navigate<AudioPlayerViewModel, DestructionResult>();
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
        public IMvxAsyncCommand ShowNavigationLeftViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowNavigationBottomViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowContentViewModelCommand { get; private set; }

        // Private methods
    }
}
