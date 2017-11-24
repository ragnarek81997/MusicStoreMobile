using MusicStoreMobile.Core.Enums;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.ViewModels;
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
    public class TopNavigationViewModelService : ITopNavigationViewModelService
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly INavigationViewModelManager _navigationViewModelManager;
        public TopNavigationViewModelService(IMvxNavigationService navigationService, INavigationViewModelManager navigationViewModelManager)
        {
            _navigationService = navigationService;
            _navigationViewModelManager = navigationViewModelManager;
        }

        public async Task<ServiceResult<TopNavigationViewModel>> Get()
        {
            var serviceResult = new ServiceResult<TopNavigationViewModel>();
            var viewModelsResult = await _navigationViewModelManager.Get<TopNavigationViewModel>();
            if (!viewModelsResult.Success)
            {
                serviceResult.Error = viewModelsResult.Error;
            }
            else
            {
                serviceResult.Result = viewModelsResult.Result.LastOrDefault() as TopNavigationViewModel;
                if (serviceResult.Result == null)
                {
                    serviceResult.Error.Code = ErrorStatusCode.InvalidData;
                    serviceResult.Error.Description = "Top navigation is not shown.";
                }
                else
                {
                    serviceResult.Success = true;
                }
            }

            return serviceResult;
        }

        public async Task Show (TopNavigationViewModel.PrepareModel parameter)
        {
            await _navigationService.Navigate<TopNavigationViewModel, TopNavigationViewModel.PrepareModel>(parameter);
        }

        public async Task Close()
        {
            await _navigationViewModelManager.Close<TopNavigationViewModel>();
        }

        public async Task<ServiceResult> SetTitle(string title)
        {
            var serviceResult = new ServiceResult();

            var viewModelResult = await Get();
            if(viewModelResult.Success)
            {
                var viewModel = viewModelResult.Result;

                viewModel.Title.Value = title;

                serviceResult.Success = true;
            }
            else
            {
                serviceResult.Success = false;
            }
            return serviceResult;
        }

        public async Task<ServiceResult> SetHomeIcon(TopNavigationViewIconType iconType, IMvxCommand iconCommand)
        {
            var serviceResult = new ServiceResult();

            var viewModelResult = await Get();
            if (viewModelResult.Success)
            {
                var viewModel = viewModelResult.Result;

                viewModel.HomeIconType.Value = iconType;
                viewModel.HomeIconCommand = iconCommand;

                serviceResult.Success = true;
            }
            else
            {
                serviceResult.Success = false;
            }
            return serviceResult;
        }

        public async Task<ServiceResult> SetActionIcon(TopNavigationViewIconType iconType, IMvxCommand iconCommand)
        {
            return await SetActionIcon(iconType, new MvxCommand(() => iconCommand?.Execute(null)));
        }
        public async Task<ServiceResult> SetActionIcon(TopNavigationViewIconType iconType, IMvxCommand<string> iconCommand)
        {
            var serviceResult = new ServiceResult();

            var viewModelResult = await Get();
            if (viewModelResult.Success)
            {
                var viewModel = viewModelResult.Result;

                viewModel.ActionIconType.Value = iconType;
                viewModel.ActionIconCommand = iconCommand;

                serviceResult.Success = true;
            }
            else
            {
                serviceResult.Success = false;
            }
            return serviceResult;
        }
    }
}
