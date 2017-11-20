using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Services.Interfaces;
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
        private readonly IDictionaryDbService _dbService;
        public NavigationFragmentManager(IMvxNavigationService navigationService, IDictionaryDbService dbService)
        {
            _navigationService = navigationService;
            _dbService = dbService;
        }

        public async Task<TResult> Navigate<TViewModel, TParameter, TResult>(TParameter param, IMvxBundle presentationBundle = null) where TViewModel : BaseViewModel<TParameter, TResult>
        {
            //await this.Close<TViewModel>();
            var cts = new CancellationTokenSource();

            await _dbService.SaveObject<CancellationTokenSource>(cts, Constants.DbTokens.NavigationFragmentManager + typeof(TViewModel).Name);
        
            return await _navigationService.Navigate<TViewModel, TParameter, TResult>(param, presentationBundle: presentationBundle, cancellationToken: cts.Token);
        }
        public async Task<TResult> Navigate<TViewModel, TResult>(IMvxBundle presentationBundle = null) where TViewModel : BaseViewModelResult<TResult>
        {
            await this.Close<TViewModel>();
            var cts = new CancellationTokenSource();

            await _dbService.SaveObject<CancellationTokenSource>(cts, Constants.DbTokens.NavigationFragmentManager + typeof(TViewModel).Name);

            return await _navigationService.Navigate<TViewModel, TResult>(presentationBundle: presentationBundle, cancellationToken: cts.Token);
        }

        public async Task Close<TViewModel>() where TViewModel : BaseViewModel
        {
            CancellationTokenSource cts = null;
            try
            {
                var ctsResult = await _dbService.GetObject<CancellationTokenSource>(Constants.DbTokens.NavigationFragmentManager + typeof(TViewModel).Name);
                if (ctsResult.Success)
                {
                    cts = ctsResult.Result;
                }
                await _dbService.RemoveObject(Constants.DbTokens.NavigationFragmentManager + typeof(TViewModel).Name);
            }
            catch (Exception)
            {
            }
            cts?.Cancel();
        }
    }
}
