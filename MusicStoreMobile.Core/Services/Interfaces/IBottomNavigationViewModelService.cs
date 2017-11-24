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
    public interface IBottomNavigationViewModelService
    {
        Task<ServiceResult<BottomNavigationViewModel>> Get();
        Task Show(BottomNavigationViewModel.PrepareModel parameter);
        Task Close();
        Task<ServiceResult> CheckItem(BottomNavigationViewCheckedItemType checkedItem);
    }
}
