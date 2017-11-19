﻿using MusicStoreMobile.Core.Services.Implementations;
using MusicStoreMobile.Core.ViewModelResults;
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
    public class AudioPlayerViewModel : BaseViewModelResult<DestructionResult>
    {
        public AudioPlayerViewModel()
        {
            mediaPlayer = CrossMediaManager.Current;
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

        public override void ViewDestroy()
        {
            MediaNotificationManager.StopNotifications();
            base.ViewDestroy();
        }

        // MVVM Properties

        private readonly IMediaManager mediaPlayer;
        public IMediaManager MediaPlayer => mediaPlayer;

        public IMediaNotificationManager MediaNotificationManager => MediaPlayer.MediaNotificationManager;

        public IMediaQueue Queue => mediaPlayer.MediaQueue;

        public IMediaFile CurrentTrack => Queue.Current;

        public int Duration => mediaPlayer.Duration.TotalSeconds > 0 ? Convert.ToInt32(mediaPlayer.Duration.TotalSeconds) : 0;

        private bool _isSeeking = false;

        public bool IsSeeking
        {
            get
            {
                return _isSeeking;
            }
            set
            {
                // Put into an action so we can await the seek-command before we update the value. Prevents jumping of the progress-bar.
                var a = new Action(async () =>
                {
                    // When disable user-seeking, update the position with the position-value
                    if (value == false)
                    {
                        await mediaPlayer.Seek(TimeSpan.FromSeconds(Position));
                    }

                    _isSeeking = value;
                });
                a.Invoke();
            }
        }

        private int _position;

        public int Position
        {
            get
            {
                if (IsSeeking)
                    return _position;

                return mediaPlayer.Position.TotalSeconds > 0 ? Convert.ToInt32(mediaPlayer.Position.TotalSeconds) : 0;
            }
            set
            {
                _position = value;

                RaisePropertyChanged( ()=> Position);
            }
        }

        public int Downloaded => Convert.ToInt32(mediaPlayer.Buffered.TotalSeconds);

        public bool IsPlaying => mediaPlayer.Status == MediaPlayerStatus.Playing || mediaPlayer.Status == MediaPlayerStatus.Buffering;

        public MediaPlayerStatus Status => mediaPlayer.Status;

        public object Cover => mediaPlayer.MediaQueue.Current.Metadata.AlbumArt;

        public string PlayingText => $"Playing: {(Queue.Index + 1)} of {Queue.Count}";

        private IPlaybackController PlaybackController => MediaPlayer.PlaybackController;

        // MVVM Commands

        // Private methods

    }
}
