using Acr.UserDialogs;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using MusicStoreMobile.Core.Services.Interfaces;
using Nito.AsyncEx;
using MvvmCross.FieldBinding;
using System;
using MvvmCross.Plugins.Validation.ForFieldBinding;
using MusicStoreMobile.Core.Controls;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Converters;
using System.Globalization;
using System.Collections.Generic;
using MusicStoreMobile.Core.Helpers.Implementations;
using MvvmCross.Platform;
using MusicStoreMobile.Core.ViewModels.Navigation;
using MusicStoreMobile.Core.ViewModelResults;
using System.Threading;
using MusicStoreMobile.Core.ViewModels.Preferences;
using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.ViewModels.Main;

namespace MusicStoreMobile.Core.ViewModels.Auth
{
    public class LoginViewModel : BaseViewModel<ApplicationUserModel>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ITopNavigationViewModelService _topNavigationViewModelService;
        private readonly IBottomNavigationViewModelService _bottomNavigationViewModelService;

        private readonly IAuthService _authService;
        private readonly IUserDialogs _userDialogs;

        private readonly IValidationHelper _validationHelper;
        
        public LoginViewModel(IMvxNavigationService navigationService, IAuthService authService, IUserDialogs userDialogs, IValidator validator, IBottomNavigationViewModelService bottomNavigationViewModelService, ITopNavigationViewModelService topNavigationViewModelService)
        {
            _navigationService = navigationService;
            _topNavigationViewModelService = topNavigationViewModelService;
            _bottomNavigationViewModelService = bottomNavigationViewModelService;

            _authService = authService;
            _userDialogs = userDialogs;

            _validationHelper = new ValidationHelper(validator, this, Errors.Value, (propertyName) => { FocusName.Value = propertyName; });

            ValidateEmailCommand = new MvxCommand(() => _validationHelper.Validate(() => Email));
            ValidatePasswordCommand = new MvxCommand(() => _validationHelper.Validate(() => Password));

            InitValidationCommand = new MvxCommand(() => {
                _validationHelper.ResetValidation();
            });

            LogInCommand = new MvxCommand(() => 
            {
                if (!IsTaskExecutedValueConverter.Convert(LogInTask.Value))
                {
                    LogInTask.Value = NotifyTaskCompletion.Create(AttemptLogInAsync);
                }
            });

			ShowRegistrationViewModelCommand = new MvxAsyncCommand(async () => 
            {
                await _navigationService.Navigate<RegistrationViewModel>();
            });
        }

        // MvvmCross Lifecycle
        public override void Prepare(ApplicationUserModel parameter)
        {
            if (parameter != null)
            {
                Email.Value = parameter.Email;
                Password.Value = parameter.Password;

                Task.Run(async () => 
                {
                    await Task.Delay(5000);
                    LogInCommand?.Execute(null);
                });
            }
        }

        public override void Start()
        {
            base.Start();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            _topNavigationViewModelService.Close();
            _bottomNavigationViewModelService.Close();
        }

        // MVVM Properties

        [Validators.NCFieldRequired("{0} is Required")]
        [Validators.NCFieldRegex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", "The value of {0} is incorrect")]
        public readonly INC<string> Email = new NC<string>("");

        [Validators.NCFieldRequired("{0} is Required")]
        [Validators.NCFieldStringLength(32, "The Length of {0} must between {1} and {2}", MinimumLength = 8)]
        public readonly INC<string> Password = new NC<string>("");

        public readonly INC<string> FocusName = new NC<string>("");

        public readonly INC<INotifyTaskCompletion> LogInTask = new NC<INotifyTaskCompletion>();

        public INC<ObservableDictionary<string, string>> Errors = new NC<ObservableDictionary<string, string>>(new ObservableDictionary<string, string>());

        // MVVM Commands

        public IMvxCommand LogInCommand { get; private set; }

        public IMvxCommand ShowRegistrationViewModelCommand { get; private set; }

        public IMvxCommand ValidateEmailCommand { get; private set; }

        public IMvxCommand ValidatePasswordCommand { get; private set; }

        public IMvxCommand InitValidationCommand { get; private set; }

        // Private methods

        private async Task AttemptLogInAsync()
        {
            Email.Value = Email.Value?.Trim();
            Password.Value = Password.Value?.Trim();

            if (_validationHelper.Validate())
            {
                _userDialogs.ShowLoading("Login");

                var serviceResult = await _authService.Login(Email.Value, Password.Value);
                if (serviceResult.Success)
                {
                    ClearStack.Execute(null);
                    await _navigationService.Navigate<HomeViewModel>();
                    await _bottomNavigationViewModelService.Show(new BottomNavigationViewModel.PrepareModel() { CheckedItem = Enums.BottomNavigationViewCheckedItemType.Home });

                    _userDialogs.HideLoading();
                }
                else
                {
                    _userDialogs.HideLoading();

                    await _userDialogs.AlertAsync(new AlertConfig
                    {
                        Title = "Login failed",
                        Message = serviceResult.Error.Description,
                        OkText = "OK"
                    });

                    InitValidationCommand.Execute(null);
                }
            }
        }
    }
}
