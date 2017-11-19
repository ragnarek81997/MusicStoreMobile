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
    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>
    {
        public async Task Init(string parameter)
        {
            if (!string.IsNullOrEmpty(parameter))
            {
                IMvxJsonConverter serializer;
                if (!Mvx.TryResolve(out serializer))
                {
                    throw new MvxIoCResolveException("There is no implementation of IMvxJsonConverter registered. You need to use the MvvmCross Json plugin or create your own implementation of IMvxJsonConverter.");
                }

                var deserialized = serializer.DeserializeObject<TParameter>(parameter);
                Prepare(deserialized);
                await Initialize();
            }
        }

        public abstract void Prepare(TParameter parameter);
    }

    public abstract class BaseViewModelResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult>
    {
        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        public override void ViewDestroy()
        {
            if (CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();
            base.ViewDestroy();
        }
    }

    public abstract class BaseViewModel<TParameter, TResult> : BaseViewModelResult<TResult>, IMvxViewModel<TParameter, TResult>
    {
        public abstract void Prepare(TParameter parameter);
    }
}
