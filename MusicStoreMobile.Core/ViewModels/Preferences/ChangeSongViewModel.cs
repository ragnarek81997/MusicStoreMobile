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
    public class ChangeSongViewModel : BaseViewModel<SongModel>
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ITopNavigationViewModelService _topNavigationViewModelService;
        private readonly IBottomNavigationViewModelService _bottomNavigationViewModelService;

        private readonly IUserDialogs _userDialogs;

        private readonly IValidationHelper _validationHelper;

        private readonly ISongService _songService;
        private readonly IGenreService _genreService;
        private readonly IArtistService _artistService;

        public ChangeSongViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, IValidator validator, ISongService songService, IGenreService genreService, IArtistService artistService, IBottomNavigationViewModelService bottomNavigationViewModelService, ITopNavigationViewModelService topNavigationViewModelService)
        {
            _navigationService = navigationService;
            _topNavigationViewModelService = topNavigationViewModelService;
            _bottomNavigationViewModelService = bottomNavigationViewModelService;

            _userDialogs = userDialogs;

            _validationHelper = new ValidationHelper(validator, this, Errors.Value, (propertyName) => { FocusName.Value = propertyName; });

            _songService = songService;
            _genreService = genreService;
            _artistService = artistService;

            ValidateNameCommand = new MvxCommand(() => _validationHelper.Validate(() => Name));

            ValidateGenresCommand = new MvxCommand(() => _validationHelper.Validate(() => Genres));

            ValidateArtistsCommand = new MvxCommand(() => _validationHelper.Validate(() => Artists));

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

            _genresTokenParentObject = new TokenParentHelper(new MvxCommand<TokenViewModel>((_) => 
            {
                Genres.Value?.Remove(_ as TokenViewModel<GenreModel>);
                ValidateGenresCommand.Execute(null);
            }));

            _artistsTokenParentObject = new TokenParentHelper(new MvxCommand<TokenViewModel>((_) =>
            {
                Artists.Value?.Remove(_ as TokenViewModel<ArtistModel>);
                ValidateArtistsCommand.Execute(null);
            }));
        }

        // MvvmCross Lifecycle
        public override void Prepare(SongModel parameter)
        {
            if (parameter != null)
            {
                Id.Value = parameter.Id;
                Name.Value = parameter.Name;

                Genres.Value = new ObservableCollection<TokenViewModel<GenreModel>>();

                foreach (var item in parameter.Genres)
                {
                    Genres.Value.Add(new TokenViewModel<GenreModel>(item, _genresTokenParentObject));
                }

                Artists.Value = new ObservableCollection<TokenViewModel<ArtistModel>>();

                foreach (var item in parameter.Artists)
                {
                    Artists.Value.Add(new TokenViewModel<ArtistModel>(item, _genresTokenParentObject));
                }
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
                Title = (string.IsNullOrWhiteSpace(Id.Value) ? "Add" : "Update") + " song",
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

        #region Genres
        private readonly ITokenParentHelper _genresTokenParentObject;

        private string _genresCurrentTextHint;
        public string GenresCurrentTextHint
        {
            get
            {
                return _genresCurrentTextHint;
            }
            set
            {
               
                if (value == "")
                {
                    _genresCurrentTextHint = null;
                    SetGenresSuggestions();
                    return;
                }
                else
                {
                    _genresCurrentTextHint = value;
                }

                if (_genresCurrentTextHint.Trim().Length < 2)
                {
                    SetGenresSuggestions();
                    return;
                }

                Task.Run(async()=> 
                {
                    var searchResult = await _genreService.GetMany(_genresCurrentTextHint, 0, 5);

                    InvokeOnMainThread(()=> 
                    {
                        if (searchResult.Success)
                        {
                            SetGenresSuggestions(searchResult.Result);
                        }
                        else
                        {
                            SetGenresSuggestions();
                        }
                    });
                });
            }
        }

        private object _genresSelectedObject;
        public object GenresSelectedObject
        {
            get
            {
                return _genresSelectedObject;
            }
            private set
            {
                _genresSelectedObject = value;
                if (_genresSelectedObject is GenreModel)
                {
                    var item = new TokenViewModel<GenreModel>(_genresSelectedObject as GenreModel, _genresTokenParentObject);
                    if(!Genres.Value.Any(_=>_.Object.Id == item.Object.Id))
                        Genres.Value.Add(item);

                    _genresSelectedObject = null;
                    GenresArticle.Value = string.Empty;

                    ValidateGenresCommand.Execute(null);
                }
                RaisePropertyChanged(() => GenresSelectedObject);
            }
        }

        public INC<ObservableCollection<GenreModel>> GenresSuggestions = new NC<ObservableCollection<GenreModel>>(new ObservableCollection<GenreModel>());

        public INC<string> GenresArticle = new NC<string>("");

        [Validators.NCFieldObservableCollectionRequired(50, "The Count of {0} must between {1} and {2}", MinimumCount = 1)]
        public INC<ObservableCollection<TokenViewModel<GenreModel>>> Genres = new NC<ObservableCollection<TokenViewModel<GenreModel>>>(new ObservableCollection<TokenViewModel<GenreModel>>());

        private void SetGenresSuggestions(List<GenreModel> param = null)
        {
            GenresSuggestions.Value = param == null || param.Count == 0 ? new ObservableCollection<GenreModel>() : new ObservableCollection<GenreModel>(param);
        }
        #endregion

        #region Artists
        private readonly ITokenParentHelper _artistsTokenParentObject;

        private string _artistsCurrentTextHint;
        public string ArtistsCurrentTextHint
        {
            get
            {
                return _artistsCurrentTextHint;
            }
            set
            {

                if (value == "")
                {
                    _artistsCurrentTextHint = null;
                    SetArtistsSuggestions();
                    return;
                }
                else
                {
                    _artistsCurrentTextHint = value;
                }

                if (_artistsCurrentTextHint.Trim().Length < 2)
                {
                    SetArtistsSuggestions();
                    return;
                }

                Task.Run(async () =>
                {
                    var searchResult = await _artistService.GetMany(_artistsCurrentTextHint, 0, 5);

                    InvokeOnMainThread(() =>
                    {
                        if (searchResult.Success)
                        {
                            SetArtistsSuggestions(searchResult.Result);
                        }
                        else
                        {
                            SetArtistsSuggestions();
                        }
                    });
                });
            }
        }

        private object _artistsSelectedObject;
        public object ArtistsSelectedObject
        {
            get
            {
                return _artistsSelectedObject;
            }
            private set
            {
                _artistsSelectedObject = value;
                if (_artistsSelectedObject is ArtistModel)
                {
                    var item = new TokenViewModel<ArtistModel>(_artistsSelectedObject as ArtistModel, _artistsTokenParentObject);
                    if (!Artists.Value.Any(_ => _.Object.Id == item.Object.Id))
                        Artists.Value.Add(item);

                    _artistsSelectedObject = null;
                    ArtistsArticle.Value = string.Empty;

                    ValidateArtistsCommand.Execute(null);
                }
                RaisePropertyChanged(() => ArtistsSelectedObject);
            }
        }

        public INC<ObservableCollection<ArtistModel>> ArtistsSuggestions = new NC<ObservableCollection<ArtistModel>>(new ObservableCollection<ArtistModel>());

        public INC<string> ArtistsArticle = new NC<string>("");

        [Validators.NCFieldObservableCollectionRequired(50, "The Count of {0} must between {1} and {2}", MinimumCount = 1)]
        public INC<ObservableCollection<TokenViewModel<ArtistModel>>> Artists = new NC<ObservableCollection<TokenViewModel<ArtistModel>>>(new ObservableCollection<TokenViewModel<ArtistModel>>());

        private void SetArtistsSuggestions(List<ArtistModel> param = null)
        {
            ArtistsSuggestions.Value = param == null || param.Count == 0 ? new ObservableCollection<ArtistModel>() : new ObservableCollection<ArtistModel>(param);
        }
        #endregion


        // MVVM Commands

        public IMvxCommand ChangeCommand { get; private set; }

        public IMvxCommand ValidateNameCommand { get; private set; }

        public IMvxCommand ValidateGenresCommand { get; private set; }

        public IMvxCommand ValidateArtistsCommand { get; private set; }

        public IMvxCommand InitValidationCommand { get; private set; }

        // Private methods

        private async Task AttemptChangeAsync()
        {
            Name.Value = Name.Value?.Trim();

            if (_validationHelper.Validate())
            {
                _userDialogs.ShowLoading((string.IsNullOrWhiteSpace(Id.Value) ? "Add" : "Update") + " song");

                var serviceResult = new ServiceResult<SongResultModel>();

                if (string.IsNullOrWhiteSpace(Id.Value))
                {
                    serviceResult = await _songService.Add(new SongResultModel() { Id = Id.Value, Name = Name.Value, Artists = Artists.Value.Select(_ => _.Object.Id).ToList(), Genres = Genres.Value.Select(_ => _.Object.Id).ToList() });
                }
                else
                {
                    serviceResult = await _songService.Update(new SongResultModel() { Id = Id.Value, Name = Name.Value, Artists = Artists.Value.Select(_ => _.Object.Id).ToList(), Genres = Genres.Value.Select(_ => _.Object.Id).ToList() });
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
