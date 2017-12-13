using MusicStoreMobile.Core.ViewModels.Preferences;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Helpers.Interfaces
{
    public interface ITokenParentHelper
    {
        IMvxCommand<TokenViewModel> RemoveCommand { get; }
    }
}
