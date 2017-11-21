using Android.App;
using Android.Graphics.Drawables;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MusicStoreMobile.Core.ViewModels;
using MvvmCross.Droid.Views.Attributes;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.Implementations;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MusicStoreMobile.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.audio_player_frame, false)]
    [Register(nameof(AudioPlayerView))]
    public class AudioPlayerView : BaseFragment<AudioPlayerViewModel>
    {
        protected override int FragmentId => Resource.Layout.AudioPlayerView;

        private TextView _audioPlayerTitleText;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _audioPlayerTitleText = view.FindViewById<TextView>(Resource.Id.audio_player_title_text);
            if(_audioPlayerTitleText != null)
            {
                //_audioPlayerTitleText.Selected = true;
            }

            if (savedInstanceState == null)
            {
                var rootDirectory = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath, ".MS");

                ViewModel.Queue.Clear();

                var mediaUrls =
                new[] {
                Path.Combine(rootDirectory, "01.mp3"),
                Path.Combine(rootDirectory, "02.mp3"),
                Path.Combine(rootDirectory, "03.mp3"),
                Path.Combine(rootDirectory, "04.mp3"),
                Path.Combine(rootDirectory, "05.mp3")
                };

                foreach (var mediaUrl in mediaUrls)
                {

                    //Local перевіряє наявність файлів, якщо не існує, видаляє з черги
                    //Remote не перевіряж наявність файлів, якщо не існує, намагається відтврити, якщо дійшла її черга
                    ViewModel.Queue.Add(new MediaFile(mediaUrl, Plugin.MediaManager.Abstractions.Enums.MediaFileType.Audio, Plugin.MediaManager.Abstractions.Enums.ResourceAvailability.Local));
                }

                //ViewModel.Queue.Repeat = RepeatType.RepeatAll;

                ParentActivity?.RunOnUiThread(() => ViewModel.RaiseAllPropertiesChanged());
                ParentActivity?.RunOnUiThread(() => ViewModel.MediaPlayer.Play(ViewModel.CurrentTrack));
            }

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnDestroy()
        {
            _audioPlayerTitleText?.Dispose();
            base.OnDestroy();
        }
    }
}