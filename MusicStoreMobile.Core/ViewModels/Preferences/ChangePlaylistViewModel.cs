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
using System.Collections.ObjectModel;
using System.Linq;

namespace MusicStoreMobile.Core.ViewModels.Preferences
{
    public class ChangePlaylistViewModel : BaseViewModel<PlaylistModel>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ITopNavigationViewModelService _topNavigationViewModelService;
        private readonly IBottomNavigationViewModelService _bottomNavigationViewModelService;

        private readonly IUserDialogs _userDialogs;

        private readonly IValidationHelper _validationHelper;

        private readonly ISongService _songService;
        private readonly IPlaylistService _playlistService;

        private readonly IAuthorizedUserService _authorizedUserService;
        

        public ChangePlaylistViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, IValidator validator, ISongService songService, IPlaylistService playlistService, IAuthorizedUserService authorizedUserService, IBottomNavigationViewModelService bottomNavigationViewModelService, ITopNavigationViewModelService topNavigationViewModelService)
        {
            _navigationService = navigationService;
            _topNavigationViewModelService = topNavigationViewModelService;
            _bottomNavigationViewModelService = bottomNavigationViewModelService;

            _userDialogs = userDialogs;

            _validationHelper = new ValidationHelper(validator, this, Errors.Value, (propertyName) => { FocusName.Value = propertyName; });

            _songService = songService;
            _playlistService = playlistService;
            _authorizedUserService = authorizedUserService;

            ValidateNameCommand = new MvxCommand(() => _validationHelper.Validate(() => Name));

            ValidateSongsCommand = new MvxCommand(() => _validationHelper.Validate(() => Songs));

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

            _songsTokenParentObject = new TokenParentHelper(new MvxCommand<TokenViewModel>((_) => 
            {
                Songs.Value?.Remove(_ as TokenViewModel<SongModel>);
                ValidateSongsCommand.Execute(null);
            }));
        }

        // MvvmCross Lifecycle
        public override void Prepare(PlaylistModel parameter)
        {
            if (parameter != null)
            {
                Id.Value = parameter.Id;
                Name.Value = parameter.Name;
                OwnerId.Value = parameter.OwnerId;

                Songs.Value = new ObservableCollection<TokenViewModel<SongModel>>();

                foreach (var item in parameter.Songs)
                {
                    Songs.Value.Add(new TokenViewModel<SongModel>(item, _songsTokenParentObject));
                }
            }
        }

        public async override Task Initialize()
        {
            if (string.IsNullOrWhiteSpace(Id.Value))
            {
                var authorizedUserResult = await _authorizedUserService.Get();
                if (authorizedUserResult.Success)
                {
                    OwnerId.Value = authorizedUserResult.Result.Id;
                }
            }
            await base.Initialize();
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
                Title = (string.IsNullOrWhiteSpace(Id.Value) ? "Add" : "Update") + " playlist",
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

        public readonly INC<string> OwnerId = new NC<string>("");

        public readonly INC<string> FocusName = new NC<string>("");

        public readonly INC<INotifyTaskCompletion> ChangeTask = new NC<INotifyTaskCompletion>();

        public INC<ObservableDictionary<string, string>> Errors = new NC<ObservableDictionary<string, string>>(new ObservableDictionary<string, string>());

        #region Songs
        private readonly ITokenParentHelper _songsTokenParentObject;

        private string _songsCurrentTextHint;
        public string SongsCurrentTextHint
        {
            get
            {
                return _songsCurrentTextHint;
            }
            set
            {
               
                if (value == "")
                {
                    _songsCurrentTextHint = null;
                    SetSongsSuggestions();
                    return;
                }
                else
                {
                    _songsCurrentTextHint = value;
                }

                if (_songsCurrentTextHint.Trim().Length < 2)
                {
                    SetSongsSuggestions();
                    return;
                }

                Task.Run(async()=> 
                {
                    var searchResult = await _songService.GetMany(_songsCurrentTextHint, 0, 5);

                    InvokeOnMainThread(()=> 
                    {
                        if (searchResult.Success)
                        {
                            SetSongsSuggestions(searchResult.Result);
                        }
                        else
                        {
                            SetSongsSuggestions();
                        }
                    });
                });
            }
        }

        private object _songsSelectedObject;
        public object SongsSelectedObject
        {
            get
            {
                return _songsSelectedObject;
            }
            private set
            {
                _songsSelectedObject = value;
                if (_songsSelectedObject is SongModel)
                {
                    var item = new TokenViewModel<SongModel>(_songsSelectedObject as SongModel, _songsTokenParentObject);
                    if(!Songs.Value.Any(_=>_.Object.Id == item.Object.Id))
                        Songs.Value.Add(item);

                    _songsSelectedObject = null;
                    SongsArticle.Value = string.Empty;

                    ValidateSongsCommand.Execute(null);
                }
                RaisePropertyChanged(() => SongsSelectedObject);
            }
        }

        public INC<ObservableCollection<SongModel>> SongsSuggestions = new NC<ObservableCollection<SongModel>>(new ObservableCollection<SongModel>());

        public INC<string> SongsArticle = new NC<string>("");

        [Validators.NCFieldObservableCollectionRequired(50, "The Count of {0} must between {1} and {2}", MinimumCount = 0)]
        public INC<ObservableCollection<TokenViewModel<SongModel>>> Songs = new NC<ObservableCollection<TokenViewModel<SongModel>>>(new ObservableCollection<TokenViewModel<SongModel>>());

        private void SetSongsSuggestions(List<SongModel> param = null)
        {
            SongsSuggestions.Value = param == null || param.Count == 0 ? new ObservableCollection<SongModel>() : new ObservableCollection<SongModel>(param);
        }
        #endregion

        // MVVM Commands

        public IMvxCommand ChangeCommand { get; private set; }

        public IMvxCommand ValidateNameCommand { get; private set; }

        public IMvxCommand ValidateSongsCommand { get; private set; }

        public IMvxCommand InitValidationCommand { get; private set; }

        // Private methods

        private async Task AttemptChangeAsync()
        {
            Name.Value = Name.Value?.Trim();

            if (_validationHelper.Validate())
            {
                _userDialogs.ShowLoading((string.IsNullOrWhiteSpace(Id.Value) ? "Add" : "Update") + " playlist");

                var serviceResult = new ServiceResult<PlaylistResultModel>();

                if (string.IsNullOrWhiteSpace(Id.Value))
                {
                    serviceResult = await _playlistService.Add(new PlaylistResultModel() { Id = Id.Value, Name = Name.Value, OwnerId = OwnerId.Value, Songs = Songs.Value.Select(_ => _.Object.Id).ToList() });
                }
                else
                {
                    serviceResult = await _playlistService.Update(new PlaylistResultModel() { Id = Id.Value, Name = Name.Value, OwnerId = OwnerId.Value, Songs = Songs.Value.Select(_ => _.Object.Id).ToList() });
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
