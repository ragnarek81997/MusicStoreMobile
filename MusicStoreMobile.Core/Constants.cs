using System;

namespace MusicStoreMobile.Core
{
    public static class Constants
    {
        //public const string IpServerPort = "http://192.168.148.2:45455/";
        public const string IpServerPort = "http://musicstoreserver.azurewebsites.net/";

        //public const string SignalRHubUrl = "musicstoremobile-realtime";

        public static class DbTokens
        {
            public const string AuthorizedUser = "authorizedUser";
            public const string NavigationViewModelManager = "navigationViewModelManager";
        }
    }
}
