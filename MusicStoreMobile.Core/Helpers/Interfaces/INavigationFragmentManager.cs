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
    public interface INavigationFragmentManager
    {
        Task<TResult> Navigate<TViewModel, TParameter, TResult>(TParameter param, IMvxBundle presentationBundle = null) where TViewModel : BaseViewModel<TParameter, TResult>;
        Task<TResult> Navigate<TViewModel, TResult>(IMvxBundle presentationBundle = null) where TViewModel : BaseViewModelResult<TResult>;

        Task Close<TViewModel>() where TViewModel : BaseViewModel;
    }
}
