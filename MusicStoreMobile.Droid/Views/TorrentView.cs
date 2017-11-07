using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MusicStoreMobile.Core.ViewModels;
using MvvmCross.Droid.Views.Attributes;
using System.IO;
using Libtorrent;
using MusicStoreMobile.Droid.Helpers;
using MusicStoreMobile.Core.Models.Link;
using System.Threading.Tasks;

namespace MusicStoreMobile.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    [Register(nameof(TorrentView))]
    public class TorrentView : BaseFragment<TorrentViewModel>
    {
        protected override int FragmentId => Resource.Layout.TorrentView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            if (savedInstanceState == null)
            {
                //при старті сервісу
                Libtorrent.Libtorrent.Create();

                var downloadDirectory = Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath;

                //отримувати торрент з сервера
                var torrentInfo = new TorrentLinkModel();
                var torrent = Libtorrent.Libtorrent.AddTorrentFromBytes(downloadDirectory, torrentInfo.Torrent);

                Libtorrent.Libtorrent.StartTorrent(torrent);
                while (Libtorrent.Libtorrent.TorrentStatus(torrent) == Libtorrent.Libtorrent.StatusDownloading)
                {
                    Task.Delay(1000).GetAwaiter().GetResult();
                    var progress = (((double)((long)((Libtorrent.Libtorrent.TorrentBytesCompleted(torrent)) / ((double)Libtorrent.Libtorrent.TorrentBytesLength(torrent)) * 10000))) / 100);
                }
                //ставити на паузу, не роздавати
                Libtorrent.Libtorrent.StopTorrent(torrent);

                //при завершенні сервісу 
                Libtorrent.Libtorrent.Close();
            }

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}