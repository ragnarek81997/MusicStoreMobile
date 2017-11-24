using MusicStoreMobile.Core.Enums;
using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.ViewModels.Main;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels.Navigation
{

    public class BottomNavigationViewModel : BaseViewModel<BottomNavigationViewModel.PrepareModel>
    {
        public class PrepareModel
        {
            public BottomNavigationViewCheckedItemType CheckedItem { get; set; }
        }

        private readonly IMvxNavigationService _navigationService;

        public BottomNavigationViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowHomeViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<HomeViewModel>());
            ShowSearchViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<SearchViewModel>());
            ShowLibraryViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LibraryViewModel>());
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void Prepare(PrepareModel parameter)
        {
            if(parameter != null)
            {
                CheckedItem.Value = parameter.CheckedItem;
            }
        }

        // MVVM Properties

        public readonly INC<BottomNavigationViewCheckedItemType> CheckedItem = new NC<BottomNavigationViewCheckedItemType>(BottomNavigationViewCheckedItemType.None);

        // MVVM Commands
        public IMvxAsyncCommand ShowHomeViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowSearchViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowLibraryViewModelCommand { get; private set; }
        // Private methods

    }
}
