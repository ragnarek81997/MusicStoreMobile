using MusicStoreMobile.Core.Enums;
using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicStoreMobile.Core.Services.Interfaces
{
    public interface ITopNavigationViewModelService
    {
        Task<ServiceResult<TopNavigationViewModel>> Get();
        Task Show(TopNavigationViewModel.PrepareModel parameter);
        Task Close();
        Task<ServiceResult> SetTitle(string title);
        Task<ServiceResult> SetHomeIcon(TopNavigationViewIconType iconType, IMvxCommand iconCommand);
        Task<ServiceResult> SetActionIcon(TopNavigationViewIconType iconType, IMvxCommand iconCommand);
        Task<ServiceResult> SetActionIcon(TopNavigationViewIconType iconType, IMvxCommand<string> iconCommand);

    }
}
