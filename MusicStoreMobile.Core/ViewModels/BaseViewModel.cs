using System;
using MvvmCross.Core.ViewModels;
using MusicStoreMobile.Core.Resources;
using System.Threading.Tasks;
using MvvmCross.Platform.Exceptions;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;

namespace MusicStoreMobile.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected BaseViewModel()
        {
        }

        public string this[string index] => Strings.ResourceManager.GetString(index);

        public IMvxCommand ClearStack { get; set; }
    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>
    {
        protected BaseViewModel()
        {
        }
        public abstract void Prepare(TParameter parameter);
    }
}
