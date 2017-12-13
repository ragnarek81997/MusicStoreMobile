using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.ViewModels.Preferences;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Helpers.Implementations
{
    public class TokenParentHelper : ITokenParentHelper
    {
        public TokenParentHelper(IMvxCommand<TokenViewModel> removeCommand)
        {
            RemoveCommand = removeCommand;
        }
        public IMvxCommand<TokenViewModel> RemoveCommand { get; private set; }
    }
}
