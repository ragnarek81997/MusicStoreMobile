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
    public class NavigationViewModelManager : INavigationViewModelManager
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IDictionaryDbService _dbService;
        public NavigationViewModelManager(IMvxNavigationService navigationService, IDictionaryDbService dbService)
        {
            _navigationService = navigationService;
            _dbService = dbService;
        }

        private string GetDbTokenName<TViewModel>(object viewModel) where TViewModel : IMvxViewModel
        {
            return Constants.DbTokens.NavigationViewModelManager + (viewModel == null ? typeof(TViewModel).Name : viewModel.GetType().Name);
        }

        public async Task<int> Close<TViewModel>(bool firstOrAll = false) where TViewModel : IMvxViewModel
        {
            var ctsResult = await _dbService.GetObject<List<IMvxViewModel>>(GetDbTokenName<TViewModel>(null));
            var viewModelsObject = ctsResult.Success ? ctsResult.Result : new List<IMvxViewModel>();

            var closedCounter = 0;

            foreach(var viewModel in viewModelsObject.ToList())
            {
                if (viewModel is null)
                    continue;

                closedCounter += 1;
               
                    await _navigationService.Close(viewModel);
                if (firstOrAll)
                    break;
            }
            return closedCounter;
        }

        public async Task OnAdd<TViewModel>(TViewModel viewModel) where TViewModel : IMvxViewModel
        {
            if ((viewModel as IMvxViewModel) is null)
                return;

            var ctsResult = await _dbService.GetObject<List<IMvxViewModel>>(GetDbTokenName<IMvxViewModel>(viewModel));
            var viewModelsObject = ctsResult.Success ? ctsResult.Result : new List<IMvxViewModel>();
            viewModelsObject.Add(viewModel);
            await _dbService.SaveObject<List<IMvxViewModel>>(viewModelsObject, GetDbTokenName<IMvxViewModel>(viewModel));
        }

        public async Task OnClose<TViewModel>(TViewModel viewModel) where TViewModel : IMvxViewModel
        {
            if ((viewModel as IMvxViewModel) is null)
                return;

            var ctsResult = await _dbService.GetObject<List<IMvxViewModel>>(GetDbTokenName<IMvxViewModel>(viewModel));
            var viewModelsObject = ctsResult.Success ? ctsResult.Result : new List<IMvxViewModel>();
            viewModelsObject.Remove(viewModel);
            await _dbService.SaveObject<List<IMvxViewModel>>(viewModelsObject, GetDbTokenName<IMvxViewModel>(viewModel));
        }
    }
}
