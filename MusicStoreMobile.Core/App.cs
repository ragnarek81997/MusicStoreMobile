using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MusicStoreMobile.Core.ViewModels.Auth;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.Services.Implementations;
using MusicStoreMobile.Core.ViewModels;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Client")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton(() => UserDialogs.Instance);

            Mvx.RegisterType<IValidator, Validator>();

            // register the appstart object
            RegisterNavigationServiceAppStart<MainViewModel>();
        }
    }
}
