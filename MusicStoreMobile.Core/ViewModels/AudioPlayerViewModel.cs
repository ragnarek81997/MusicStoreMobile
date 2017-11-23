using MusicStoreMobile.Core.Helpers.Interfaces;
using MusicStoreMobile.Core.Services.Implementations;
using MusicStoreMobile.Core.ViewModelResults;
using MvvmCross.Core.ViewModels;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModels
{
    public class AudioPlayerViewModel : BaseViewModel
    {
        public AudioPlayerViewModel()
        {
            mediaPlayer = CrossMediaManager.Current;

            MediaPlayer.StatusChanged += (sender, e) =>
            {
                RaisePropertyChanged(() => Status);
                RaisePropertyChanged(() => Position);
            };

            MediaPlayer.PlayingChanged += (sender, e) =>
            {
                RaisePropertyChanged(() => Position);
            };

            MediaPlayer.MediaQueue.QueueMediaChanged += (q_sender, q_e) => 
            {
                RaisePropertyChanged(() => PlayingText);
                if (CurrentTrack != null)
                {
                    CurrentTrack.MetadataUpdated += (m_sender, m_e) =>
                    {
                        RaisePropertyChanged(() => PlayingText);
                    };
                }
            };

            PlayPauseCommand = new MvxAsyncCommand(async () => await PlaybackController.PlayPause());
            SkipPreviousCommand = new MvxAsyncCommand(async () => await PlaybackController.PlayPreviousOrSeekToStart());
            SkipNextCommand = new MvxAsyncCommand(async () => await PlaybackController.PlayNext());
        }

        // MvvmCross Lifecycle
        public override void Start()
        {
            base.Start();
        }

        public override void ViewCreated()
        {
            base.ViewCreated();
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewDestroy()
        {
            base.ViewDestroy();
        }

        // MVVM Properties

        private readonly IMediaManager mediaPlayer;
        public IMediaManager MediaPlayer => mediaPlayer;

        public IMediaNotificationManager MediaNotificationManager => MediaPlayer.MediaNotificationManager;

        public IMediaQueue Queue => mediaPlayer.MediaQueue;

        public IMediaFile CurrentTrack => Queue.Current;

        public MediaPlayerStatus Status => mediaPlayer.Status;

        public string PlayingText
        {
            get
            {
                if (CurrentTrack == null || CurrentTrack.Metadata == null) return string.Empty;
                return CurrentTrack.Metadata.Artist + ((!string.IsNullOrWhiteSpace(CurrentTrack.Metadata.Artist) && !string.IsNullOrWhiteSpace(CurrentTrack.Metadata.Title))?(" - "):("")) + CurrentTrack.Metadata.Title;
            }
        }

        public int Duration => mediaPlayer.Duration.TotalSeconds > 0 ? Convert.ToInt32(mediaPlayer.Duration.TotalSeconds) : 0;

        public int Position
        {
            get
            {
                var result = (Duration > 0 ? ((int)((Convert.ToInt32(mediaPlayer.Position.TotalSeconds) / (double)Duration) * 1000)) : 0);
                return result;
            }
        }

        private IPlaybackController PlaybackController => MediaPlayer.PlaybackController;

        // MVVM Commands

        public IMvxCommand PlayPauseCommand { get; private set; }
        public IMvxCommand SkipPreviousCommand { get; private set; }
        public IMvxCommand SkipNextCommand { get; private set; }

        // Private methods

    }
}
