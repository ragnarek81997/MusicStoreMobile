using MusicStoreMobile.Core.Enums;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.ViewModels;
using MusicStoreMobile.Core.ViewModels.Empty;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Implementations
{
    public class BottomNavigationViewModelService : IBottomNavigationViewModelService
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly INavigationViewModelManager _navigationViewModelManager;
        public BottomNavigationViewModelService(IMvxNavigationService navigationService, INavigationViewModelManager navigationViewModelManager)
        {
            _navigationService = navigationService;
            _navigationViewModelManager = navigationViewModelManager;
        }

        public async Task<ServiceResult<BottomNavigationViewModel>> Get()
        {
            var serviceResult = new ServiceResult<BottomNavigationViewModel>();
            var viewModelsResult = await _navigationViewModelManager.Get<BottomNavigationViewModel>();
            if (!viewModelsResult.Success)
            {
                serviceResult.Error = viewModelsResult.Error;
            }
            else
            {
                serviceResult.Result = viewModelsResult.Result.LastOrDefault() as BottomNavigationViewModel;
                if (serviceResult.Result == null)
                {
                    serviceResult.Error.Code = ErrorStatusCode.InvalidData;
                    serviceResult.Error.Description = "Bottom navigation is not shown.";
                }
                else
                {
                    serviceResult.Success = true;
                }
            }

            return serviceResult;
        }

        public async Task Show (BottomNavigationViewModel.PrepareModel parameter)
        {
            await _navigationService.Navigate<BottomNavigationViewModel, BottomNavigationViewModel.PrepareModel>(parameter);
        }

        public async Task Close()
        {
            await _navigationService.Navigate<EmptyBottomNavigationViewModel>();
        }

        public async Task<ServiceResult> CheckItem(BottomNavigationViewCheckedItemType checkedItem)
        {
            var serviceResult = new ServiceResult() { Success = true };

            var viewModelResult = await Get();
            if (viewModelResult.Success)
            {
                var viewModel = viewModelResult.Result;
                viewModel.CheckedItem.Value = checkedItem;
            }
            else
            {
                await this.Show(new BottomNavigationViewModel.PrepareModel() { CheckedItem = checkedItem });
            }
            return serviceResult;
        }
    }
}
