using System;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MusicStoreMobile.Core.ViewModels.Auth;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModels;

namespace MusicStoreMobile.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        private readonly IMvxNavigationService _mvxNavigationService;
        private readonly IAuthService _authService;

        public AppStart(IMvxNavigationService mvxNavigationService, IAuthService authService)
        {
            _mvxNavigationService = mvxNavigationService;
            _authService = authService;
        }

        public void Start(object hint = null)
        {
            _mvxNavigationService.Navigate<MainViewModel>();
        }
    }
}
