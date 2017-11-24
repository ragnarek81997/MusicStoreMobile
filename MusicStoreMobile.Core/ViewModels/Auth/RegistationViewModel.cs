using Acr.UserDialogs;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using MusicStoreMobile.Core.Services.Interfaces;
using Nito.AsyncEx;
using MusicStoreMobile.Core.Models;
using System;
using MvvmCross.FieldBinding;
using MusicStoreMobile.Core.Controls;
using MvvmCross.Plugins.Validation.ForFieldBinding;
using System.Collections.Generic;
using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Converters;
using MusicStoreMobile.Core.Helpers.Implementations;
using MusicStoreMobile.Core.ViewModelResults;

namespace MusicStoreMobile.Core.ViewModels.Auth
{
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAuthService _authService;
        private readonly IUserDialogs _userDialogs;

        private readonly IValidationHelper _validationHelper;

        //private readonly ILocationService _locationService;
        private readonly MvvmCross.Plugins.Validation.IMvxToastService _toastService;

        public RegistrationViewModel(IMvxNavigationService navigationService, IAuthService authService, IUserDialogs userDialogs, IValidator validator/*, ILocationService locationService*/, MvvmCross.Plugins.Validation.IMvxToastService toastService)
        {
            _navigationService = navigationService;
            _authService = authService;
            _userDialogs = userDialogs;
            _toastService = toastService;

            _validationHelper = new ValidationHelper(validator, this, Errors.Value, (propertyName) => { FocusName.Value = propertyName; });

            //_locationService = locationService;

            ValidateEmailCommand = new MvxCommand(() => _validationHelper.Validate(() => Email));
            ValidateFirstNameCommand = new MvxCommand(() => _validationHelper.Validate(() => FirstName));
            ValidateLastNameCommand = new MvxCommand(() => _validationHelper.Validate(() => LastName));
            ValidatePasswordCommand = new MvxCommand(() => _validationHelper.Validate(() => Password));
            ValidateBirthDateCommand = new MvxCommand(() => _validationHelper.Validate(() => BirthDate));

            RegisterCommand = new MvxCommand( () => 
            {
                if (!IsTaskExecutedValueConverter.Convert(RegisterTask.Value))
                {
                    RegisterTask.Value = NotifyTaskCompletion.Create(AttemptRegisterAsync);
                }
            });

            ChangeBirthDateCommand = new MvxAsyncCommand( async () => {
                var datePromptResult = await _userDialogs.DatePromptAsync(new DatePromptConfig() { MinimumDate = new DateTime(1900, 1, 1), MaximumDate = DateTime.Now, SelectedDate = BirthDate.Value ?? new DateTime?(new DateTime(1990, 1, 1)) , OkText = "OK", CancelText = "Cancel" });
                if (datePromptResult.Ok)
                {
                    BirthDate.Value = new DateTime?(datePromptResult.SelectedDate);
                }
                else
                {
                    BirthDate.Value = null;
                }
                ValidateBirthDateCommand.Execute(null);
            });

            InitValidationCommand = new MvxCommand(() => {
                _validationHelper.ResetValidation();
            });
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        public override async Task Initialize()
        {
            //try
            //{
            //    var location = await _locationService.GetCurrentLocation();
            //    _toastService.DisplayMessage("Latitude: " + location.Latitude + "; " + "Longitude: " + location.Longitude + ".");
            //}
            //catch (Exception ex)
            //{
            //    _toastService.DisplayError(ex.Message);
            //}
        }

        // MVVM Properties

        [Validators.NCFieldRequired("{0} is Required")]
        [Validators.NCFieldRegex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", "The value of {0} is incorrect")]
        public readonly INC<string> Email = new NC<string>("");

        [Validators.NCFieldRequired("{0} is Required")]
        [Validators.NCFieldStringLength(16, "The Length of {0} must between {1} and {2}", MinimumLength = 4)]
        public readonly INC<string> FirstName = new NC<string>("");

        [Validators.NCFieldRequired("{0} is Required")]
        [Validators.NCFieldStringLength(16, "The Length of {0} must between {1} and {2}", MinimumLength = 4)]
        public readonly INC<string> LastName = new NC<string>("");

        [Validators.NCFieldRequired("{0} is Required")]
        [Validators.NCFieldStringLength(32, "The Length of {0} must between {1} and {2}", MinimumLength = 8)]
        public readonly INC<string> Password = new NC<string>("");

        [Validators.NCFieldRequired("{0} is Required")]
        public readonly INC<DateTime?> BirthDate = new NC<DateTime?>(new DateTime?());

        public readonly INC<string> FocusName = new NC<string>("");

        public readonly INC<INotifyTaskCompletion> RegisterTask = new NC<INotifyTaskCompletion>();

        public INC<ObservableDictionary<string, string>> Errors = new NC<ObservableDictionary<string, string>>(new ObservableDictionary<string, string>());


        // MVVM Commands

        public IMvxCommand RegisterCommand { get; private set; }

        public IMvxCommand ValidateEmailCommand { get; private set; }
        public IMvxCommand ValidateFirstNameCommand { get; private set; }
        public IMvxCommand ValidateLastNameCommand { get; private set; }
        public IMvxCommand ValidatePasswordCommand { get; private set; }
        public IMvxCommand ValidateBirthDateCommand { get; private set; }

        public IMvxAsyncCommand ChangeBirthDateCommand { get; private set; }

        public IMvxCommand InitValidationCommand { get; private set; }

        // Private methods

        private async Task AttemptRegisterAsync()
        {
            Email.Value = Email.Value?.Trim();
            FirstName.Value = FirstName.Value?.Trim();
            LastName.Value = LastName.Value?.Trim();
            Password.Value = Password.Value?.Trim();

            if (_validationHelper.Validate())
            {
                var user = new ApplicationUserModel() { Email = Email.Value, FirstName = FirstName.Value, LastName = LastName.Value, Password = Password.Value, BirthDate = BirthDate.Value };
                var serviceResult = await _authService.Register(user);
                if (serviceResult.Success)
                {
                    await _navigationService.Navigate<LoginViewModel, ApplicationUserModel>(new ApplicationUserModel() { Email = Email.Value, Password = Password.Value });
                }
                else
                {
                    await _userDialogs.AlertAsync(new AlertConfig
                    {
                        Title = "Registration failed",
                        Message = serviceResult.Error.Description,
                        OkText = "OK"
                    });

                    InitValidationCommand.Execute(null);
                }
            }
        }
    }
}
