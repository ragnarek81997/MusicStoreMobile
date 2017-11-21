using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels.Navigation
{

    public class BottomNavigationViewModel : BaseViewModelResult<DestructionResult>
    {
        public BottomNavigationViewModel()
        {
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

        public override void ViewDestroy()
        {
            base.ViewDestroy();
        }

        // MVVM Properties

        // MVVM Commands

        // Private methods

    }
}
