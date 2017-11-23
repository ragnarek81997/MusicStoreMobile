using MvvmCross.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels.Preferences
{
    public class PreferencesViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public PreferencesViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        // MVVM Properties

        // MVVM Commands

        // Private methods

    }
}
