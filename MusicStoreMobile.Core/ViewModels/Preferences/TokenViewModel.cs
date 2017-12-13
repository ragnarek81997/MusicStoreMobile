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
    public class TokenViewModel : BaseViewModel
    {
        private readonly ITokenParentHelper _helper;

        public TokenViewModel(object param, ITokenParentHelper helper)
        {
            _helper = helper;

            Object = param;
            RaisePropertyChanged(() => Name);
        }

        public object Object { get; private set; }

        public string Name
        {
            get
            {
                return Object?.ToString();
            }
        }

        public IMvxCommand RemoveCommand
        {
            get
            {
                return new MvxCommand(() => _helper?.RemoveCommand?.Execute(this));
            }
        }
    }


    public class TokenViewModel<T> : TokenViewModel where T : class
    {
        public TokenViewModel(T param, ITokenParentHelper parent) : base(param, parent)
        {
            Object = param;
            RaisePropertyChanged(() => Name);
        }

        public new T Object { get; private set; }
    }
}