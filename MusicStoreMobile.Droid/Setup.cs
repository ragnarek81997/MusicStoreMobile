using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views;
using MusicStoreMobile.Core;
using MusicStoreMobile.Droid.MvxBindings;
using Acr.UserDialogs;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Plugins.Validation;
using MvvmCross.Plugins.Validation.Droid;
using Android.App;
using Plugin.MediaManager;
using MvvmCross.Platform.Logging;
using MvvmCross.Droid.Support.V7.Preference;
using MvvmCross.Droid.Views.Attributes;
using System;
using MusicStoreMobile.Core.Helpers.Interfaces;
using Android.Support.V7.Preferences;
using MusicStoreMobile.Droid.Helpers;
using System.Linq;

namespace MusicStoreMobile.Droid
{
    public class Setup : MvxAppCompatSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(FloatingActionButton).Assembly,
            typeof(Toolbar).Assembly,
            typeof(MvxRecyclerView).Assembly,
            typeof(MvxSwipeRefreshLayout).Assembly,
        };

        protected override void InitializePlatformServices()
        {
            base.InitializePlatformServices();

            Mvx.RegisterType<IMvxToastService>(() => new MvxAndroidToastService(ApplicationContext));
        }

        /// <summary>
        /// Fill the Binding Factory Registry with bindings from the support library.
        /// </summary>
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);

            MvxPreferenceSetupHelper.FillTargetFactories(registry);

            registry.RegisterFactory(new MvxCustomBindingFactory<SwipeRefreshLayout>("IsRefreshing", (swipeRefreshLayout) => new SwipeRefreshLayoutIsRefreshingTargetBinding(swipeRefreshLayout)));
            registry.RegisterCustomBindingFactory<Android.Widget.EditText>("LineColor", (view) => new ValidationEditTextViewTargetBinding(view));

            registry.RegisterCustomBindingFactory<Preference>("Click", (pref) => new MvxPreferenceClickTargetBinding(pref));
            
        }

        /// <summary>
        /// This is very important to override. The default view presenter does not know how to show fragments!
        /// </summary>
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new CustomMvxAppCompatViewPresenter(AndroidViewAssemblies, Mvx.Resolve<INavigationViewModelManager>());
        }

        //TODO: тимчаслве рішення для MVVMCROSS 5.4
        protected override MvxLogProviderType GetDefaultLogProviderType()
        {
            return MvxLogProviderType.None;
        }
        private class CustomMvxAppCompatViewPresenter : MvxAppCompatViewPresenter
        {
            private readonly INavigationViewModelManager _navigationViewModelManager;

            public CustomMvxAppCompatViewPresenter(IEnumerable<Assembly> androidViewAssemblies, INavigationViewModelManager navigationViewModelManager) : base(androidViewAssemblies)
            {
                _navigationViewModelManager = navigationViewModelManager;
            }

            protected override void ShowFragment(Type view, MvxFragmentPresentationAttribute attribute, MvxViewModelRequest request)
            {
                if (request is MvxViewModelInstanceRequest instanceRequest && instanceRequest.ViewModelInstance != null && attribute != null)
                {
                    _navigationViewModelManager.OnAdd(instanceRequest.ViewModelInstance, attribute.AddToBackStack);
                }
                base.ShowFragment(view, attribute, request);
            }

            protected override bool CloseFragment(IMvxViewModel viewModel, MvxFragmentPresentationAttribute attribute)
            {
                _navigationViewModelManager.OnClose(viewModel);
                return base.CloseFragment(viewModel, attribute);
            }
        }
    }
}
