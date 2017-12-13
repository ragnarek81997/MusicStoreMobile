using MusicStoreMobile.Core.Controls;
using MusicStoreMobile.Core.Converters;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModels.Auth;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using MvvmCross.Platform;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels.Preferences
{
    public class GenreViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public GenreViewModel(GenreModel param)
        {
            _navigationService = Mvx.Resolve<IMvxNavigationService>();

            Id.Value = param.Id;
            Name.Value = param.Name;
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        // MVVM Properties

        public readonly INC<string> Id = new NC<string>("");
        public readonly INC<string> Name = new NC<string>("");

        // MVVM Commands

        // Private methods


    }
}