using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MusicStoreMobile.Core.ViewModels.Preferences;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels.Main
{

    public class SearchViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ITopNavigationViewModelService _topNavigationViewModelService;
        private readonly IBottomNavigationViewModelService _bottomNavigationViewModelService;

        public SearchViewModel(IMvxNavigationService navigationService, ITopNavigationViewModelService topNavigationViewModelService, IBottomNavigationViewModelService bottomNavigationViewModelService)
        {
            _navigationService = navigationService;
            _topNavigationViewModelService = topNavigationViewModelService;
            _bottomNavigationViewModelService = bottomNavigationViewModelService;
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

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            _topNavigationViewModelService.Show(new TopNavigationViewModel.PrepareModel()
            {
                Title = "Search",
                ActionIconType = Enums.TopNavigationViewIconType.Search,
                IsSearch = true
            });
            _bottomNavigationViewModelService.CheckItem(Enums.BottomNavigationViewCheckedItemType.Search);
        }

        // MVVM Properties

        // MVVM Commands

        // Private methods
    }
}
