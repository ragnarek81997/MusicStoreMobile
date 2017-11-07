using MusicStoreMobile.Core.Services.Implementations;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels
{
    public class TorrentViewModel : BaseViewModel
    {
        public TorrentViewModel()
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

        // MVVM Properties

        // MVVM Commands

        // Private methods

    }
}
