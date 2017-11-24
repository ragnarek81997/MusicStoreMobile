using MusicStoreMobile.Core.ViewModelResults;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels.Navigation
{

    public class BottomNavigationViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public BottomNavigationViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
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

        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }

        public override void ViewDestroy()
        {
            base.ViewDestroy();
        }

        // MVVM Properties

        // MVVM Commands

        // Private methods

    }
}
