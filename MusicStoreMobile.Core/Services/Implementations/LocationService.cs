//using MusicStoreMobile.Core.Services.Interfaces;
//using MvvmCross.Platform.Core;
//using MvvmCross.Plugins.Location;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using MusicStoreMobile.Core.Models;
//using System.Threading.Tasks;

//namespace MusicStoreMobile.Core.Services.Implementations
//{
//    public class LocationService : ILocationService
//    {
//        private const string Tag = "LocationService";

//        private readonly object _lock = new object();

//        private static readonly TimeSpan timeOut = TimeSpan.FromSeconds(10);

//        private TaskCompletionSource<MvxCoordinates> taskSource;

//        private readonly IMvxLocationWatcher _locationWatcher;
//        private readonly IMvxMainThreadDispatcher _dispatcher;
//        private readonly ITraceService _traceService;

//        public LocationService(IMvxLocationWatcher locationWatcher, IMvxMainThreadDispatcher dispatcher, ITraceService traceService)
//        {
//            _locationWatcher = locationWatcher;
//            _dispatcher = dispatcher;
//            _traceService = traceService;

//            this._locationWatcher.OnPermissionChanged += PermissionChanged;
//        }

//        public MvxCoordinates GetLastSeenLocation()
//        {
//            var location = _locationWatcher.LastSeenLocation;
//            return location?.Coordinates;
//        }

//        public Task<MvxCoordinates> GetCurrentLocation()
//        {
//            Task<MvxCoordinates> task;
//            bool startWatcher = false;

//            lock (_lock)
//            {
//                if (taskSource == null)
//                {
//                    taskSource = new TaskCompletionSource<MvxCoordinates>();
//                    startWatcher = true;
//                }
//                task = taskSource.Task;
//            }

//            if (startWatcher)
//            {
//                StartWatcher();
//            }
//            return task;
//        }

//        private void StartWatcher()
//        {
//            _traceService.Info(Tag, "StartWatcher");

//            _dispatcher.RequestMainThreadAction(() =>
//            {
//                if (_locationWatcher.Started)
//                {
//                    _traceService.Warn(Tag, "Watcher already started!");
//                    return;
//                }
//                //new MvxLocationOptions() { Accuracy = MvxLocationAccuracy.Coarse }
//                _locationWatcher.Start(new MvxLocationOptions() { Accuracy = MvxLocationAccuracy.Fine, TrackingMode = MvxLocationTrackingMode.Background, MovementThresholdInM = 50 },
//                    location =>
//                    {
//                        _locationWatcher.Stop();

//                        OnSuccess(location.Coordinates);
//                    },
//                    error =>
//                    {
//                        _locationWatcher.Stop();

//                        OnError(new LocationException(error.Code));
//                    });

//                Task.Delay(timeOut).ContinueWith(_ => OnTimeout());
//            });
//        }

//        private void OnTimeout()
//        {
//            _traceService.Info(Tag, "Timeout");

//            TaskCompletionSource<MvxCoordinates> source;
//            lock (_lock)
//            {
//                source = taskSource;
//                taskSource = null;
//            }
//            source?.SetException(new LocationException(MvxLocationErrorCode.Timeout));

//            if (_locationWatcher.Started)
//            {
//                _locationWatcher.Stop();
//            }
//        }

//        private void OnSuccess(MvxCoordinates coordinates)
//        {
//            _traceService.Info(Tag, "Updated: " + coordinates);

//            TaskCompletionSource<MvxCoordinates> source;
//            lock (_lock)
//            {
//                source = taskSource;
//                taskSource = null;
//            }
//            source?.SetResult(coordinates);
//        }

//        private void OnError(LocationException error)
//        {
//            _traceService.Warn(Tag, "Error: " + error.Code.ToString());

//            TaskCompletionSource<MvxCoordinates> source;
//            lock (_lock)
//            {
//                source = taskSource;
//                taskSource = null;
//            }
//            source?.SetException(error);
//        }

//        private void PermissionChanged(object sender, MvxValueEventArgs<MvxLocationPermission> e)
//        {
//            _traceService.Warn(Tag, "Permission changed: " + e.Value.ToString());

//            if (e.Value != MvxLocationPermission.Denied)
//            {
//                return;
//            }

//            // Process only if denied

//            TaskCompletionSource<MvxCoordinates> source;
//            lock (_lock)
//            {
//                source = taskSource;
//                taskSource = null;
//            }
//            source?.SetException(new LocationException(MvxLocationErrorCode.PermissionDenied));
//        }

//        public class LocationException : Exception
//        {
//            public LocationException(MvxLocationErrorCode code):base("MvxLocationError: " + code.ToString() )
//            {
//                Code = code;
//            }
//            public MvxLocationErrorCode Code { get; private set; }
//        }
//    }
//}
