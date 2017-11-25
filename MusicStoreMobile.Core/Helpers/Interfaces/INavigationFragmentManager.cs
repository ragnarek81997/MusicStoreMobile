using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Helpers.Interfaces
{
    public interface INavigationViewModelManager
    {
        Task<ServiceResult<List<IMvxViewModel>>> Get<TViewModel>() where TViewModel : IMvxViewModel;
        Task<int> Close<TViewModel>(bool firstOrAll = false) where TViewModel : IMvxViewModel;
        Task OnAdd<TViewModel>(TViewModel viewModel, bool addToBackStack) where TViewModel : IMvxViewModel;
        Task OnClose<TViewModel>(TViewModel viewModel) where TViewModel : IMvxViewModel;
    }
}
