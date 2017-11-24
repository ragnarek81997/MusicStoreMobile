﻿using MusicStoreMobile.Core.Enums;
using MusicStoreMobile.Core.ViewModelResults;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels.Navigation
{

    public class TopNavigationViewModel : BaseViewModel<TopNavigationViewModel.PrepareModel>
    {
        public class PrepareModel
        {
            public string Title { get; set; }

            public TopNavigationViewIconType HomeIconType { get; set; }
            public IMvxCommand HomeIconCommand { get; set; }

            public TopNavigationViewIconType ActionIconType { get; set; }
            public IMvxCommand ActionIconCommand { get; set; }
        }

        private readonly IMvxNavigationService _navigationService;

        public TopNavigationViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
        }

        public override void ViewDestroy()
        {
            base.ViewDestroy();
        }

        public override void Prepare(PrepareModel parameter)
        {
            if (parameter != null) {
                Title.Value = parameter.Title;

                HomeIconType.Value = parameter.HomeIconType;
                HomeIconCommand = parameter.HomeIconCommand;

                ActionIconType.Value = parameter.ActionIconType;
                ActionIconCommand = parameter.ActionIconCommand;
            }
        }

        // MVVM Properties

        public readonly INC<string> Title = new NC<string>("");

        public readonly INC<TopNavigationViewIconType> HomeIconType = new NC<TopNavigationViewIconType>(TopNavigationViewIconType.None);
        public readonly INC<TopNavigationViewIconType> ActionIconType = new NC<TopNavigationViewIconType>(TopNavigationViewIconType.None);

        // MVVM Commands

        public IMvxCommand HomeIconCommand { get; set; }
        public IMvxCommand ActionIconCommand { get; set; }

        // Private methods

    }
}