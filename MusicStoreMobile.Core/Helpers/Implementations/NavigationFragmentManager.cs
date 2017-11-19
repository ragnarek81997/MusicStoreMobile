using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.ViewModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Helpers.Implementations
{
    public class NavigationFragmentManager : INavigationFragmentManager
    {
        private readonly IMvxNavigationService _navigationService;
        private static Dictionary<Type, CancellationTokenSource> _viewModels = new Dictionary<Type, CancellationTokenSource>();
        public NavigationFragmentManager(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task<TResult> Navigate<TViewModel, TParameter, TResult>(TParameter param, IMvxBundle presentationBundle = null) where TViewModel : BaseViewModel<TParameter, TResult>
        {
            //await this.Close<TViewModel>();
            var cts = new CancellationTokenSource();

            _viewModels.Add(typeof(TViewModel), cts);
        
            return await _navigationService.Navigate<TViewModel, TParameter, TResult>(param, presentationBundle: presentationBundle, cancellationToken: cts.Token);
        }
        public async Task<TResult> Navigate<TViewModel, TResult>(IMvxBundle presentationBundle = null) where TViewModel : BaseViewModelResult<TResult>
        {
            await this.Close<TViewModel>();
            var cts = new CancellationTokenSource();

            _viewModels[typeof(TViewModel)] = cts;

            return await _navigationService.Navigate<TViewModel, TResult>(presentationBundle: presentationBundle, cancellationToken: cts.Token);
        }

        public async Task Close<TViewModel>() where TViewModel : BaseViewModel
        {
            await Task.Run(() =>
            {
                CancellationTokenSource cts = null;
                try
                {
                    cts = _viewModels[typeof(TViewModel)];
                    _viewModels.Remove(typeof(TViewModel));
                }
                catch (Exception)
                {
                }
                cts?.Cancel();
            });
        }
    }
}
