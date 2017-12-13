using MusicStoreMobile.Core.Controls;
using MusicStoreMobile.Core.Converters;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.Models.Link;
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
    public class LinkViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public LinkViewModel(LinkModel param)
        {
            _navigationService = Mvx.Resolve<IMvxNavigationService>();

            Id.Value = param.Id;
            Owner.Value = param.Owner;
            MimeType.Value = param.MimeType;
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        // MVVM Properties

        public readonly INC<string> Id = new NC<string>("");
        public readonly INC<ApplicationUserModel> Owner = new NC<ApplicationUserModel>(new ApplicationUserModel());
        public readonly INC<string> MimeType = new NC<string>("");

        // MVVM Commands

        // Private methods


    }
}