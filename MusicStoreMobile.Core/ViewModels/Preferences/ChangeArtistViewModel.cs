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

namespace MusicStoreMobile.Core.ViewModels.Preferences
{
    public class ChangeArtistViewModel : BaseViewModel<ArtistModel>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ITopNavigationViewModelService _topNavigationViewModelService;
        private readonly IBottomNavigationViewModelService _bottomNavigationViewModelService;

        private readonly IUserDialogs _userDialogs;

        private readonly IValidationHelper _validationHelper;

        private readonly IArtistService _artistService;

        public ChangeArtistViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, IValidator validator, IArtistService artistService, IBottomNavigationViewModelService bottomNavigationViewModelService, ITopNavigationViewModelService topNavigationViewModelService)
        {
            _navigationService = navigationService;
            _topNavigationViewModelService = topNavigationViewModelService;
            _bottomNavigationViewModelService = bottomNavigationViewModelService;

            _userDialogs = userDialogs;

            _validationHelper = new ValidationHelper(validator, this, Errors.Value, (propertyName) => { FocusName.Value = propertyName; });

            _artistService = artistService;

            ValidateNameCommand = new MvxCommand(() => _validationHelper.Validate(() => Name));

            InitValidationCommand = new MvxCommand(() => {
                _validationHelper.ResetValidation();
            });

            ChangeCommand = new MvxCommand(() => 
            {
                if (!IsTaskExecutedValueConverter.Convert(ChangeTask.Value))
                {
                    ChangeTask.Value = NotifyTaskCompletion.Create(AttemptChangeAsync);
                }
            });
        }

        // MvvmCross Lifecycle
        public override void Prepare(ArtistModel parameter)
        {
            if (parameter != null)
            {
                Id.Value = parameter.Id;
                Name.Value = parameter.Name;
            }
        }

        public override void Start()
        {
            base.Start();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            _topNavigationViewModelService.Show(new TopNavigationViewModel.PrepareModel()
            {
                Title = (string.IsNullOrWhiteSpace(Id.Value) ? "Add" : "Update") + " artist",
                HomeIconType = Enums.TopNavigationViewIconType.Back,
                HomeIconCommand = new MvxCommand(async () => await _navigationService.Close(this)),
                ActionIconType = Enums.TopNavigationViewIconType.Done,
                ActionIconCommand = new MvxCommand<string>( (query) => ChangeCommand.Execute(null)),
            }
            );
            _bottomNavigationViewModelService.CheckItem(Enums.BottomNavigationViewCheckedItemType.None);
        }

        // MVVM Properties

        public readonly INC<string> Id = new NC<string>("");

        [Validators.NCFieldRequired("{0} is Required")]
        [Validators.NCFieldStringLength(40, "The Length of {0} must between {1} and {2}", MinimumLength = 3)]
        public readonly INC<string> Name = new NC<string>("");

        public readonly INC<string> FocusName = new NC<string>("");

        public readonly INC<INotifyTaskCompletion> ChangeTask = new NC<INotifyTaskCompletion>();

        public INC<ObservableDictionary<string, string>> Errors = new NC<ObservableDictionary<string, string>>(new ObservableDictionary<string, string>());

        // MVVM Commands

        public IMvxCommand ChangeCommand { get; private set; }

        public IMvxCommand ValidateNameCommand { get; private set; }

        public IMvxCommand InitValidationCommand { get; private set; }

        // Private methods

        private async Task AttemptChangeAsync()
        {
            Name.Value = Name.Value?.Trim();

            if (_validationHelper.Validate())
            {
                _userDialogs.ShowLoading((string.IsNullOrWhiteSpace(Id.Value) ? "Add" : "Update") + " artist");

                var serviceResult = new ServiceResult<ArtistModel>();

                if (string.IsNullOrWhiteSpace(Id.Value))
                {
                    serviceResult = await _artistService.Add(new ArtistModel() { Id = Id.Value, Name = Name.Value });
                }
                else
                {
                    serviceResult = await _artistService.Update(new ArtistModel() { Id = Id.Value, Name = Name.Value });
                }

                if (serviceResult.Success)
                {
                    await _navigationService.Close(this);
                    _userDialogs.HideLoading();
                }
                else
                {
                    _userDialogs.HideLoading();

                    await _userDialogs.AlertAsync(new AlertConfig
                    {
                        Title = (string.IsNullOrWhiteSpace(Id.Value) ? "Add" : "Update") + " failed",
                        Message = serviceResult.Error.Description,
                        OkText = "OK"
                    });

                    InitValidationCommand.Execute(null);
                }
            }
        }
    }
}
