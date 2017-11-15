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
                Task.Run(() =>
                {
                    Libtorrent.Libtorrent.Create();
                    try
                    {
                        var downloadDirectory = Android.OS.Environment.GetExternalStoragePublicDirectory(string.Empty).AbsolutePath;

                        //отримувати торрент з сервера
                        var torrentInfo = new TorrentLinkModel();

                        var torrent = Libtorrent.Libtorrent.AddTorrentFromBytes(downloadDirectory, torrentInfo.Torrent);
                        if (torrent == -1) throw new ArgumentNullException();

                        Libtorrent.Libtorrent.StartTorrent(torrent);
                        while (Libtorrent.Libtorrent.TorrentStatus(torrent) == Libtorrent.Libtorrent.StatusDownloading)
                        {
                            Task.Delay(1000).GetAwaiter().GetResult();
                            var bytesCompleted = Libtorrent.Libtorrent.TorrentBytesCompleted(torrent);
                            var bytesLength = 1L;
                            if (bytesCompleted > 0)
                            {
                                bytesLength = Libtorrent.Libtorrent.TorrentBytesLength(torrent);
                            }
                            var progress = (((double)((long)((bytesCompleted) / ((double)bytesLength) * 10000))) / 100);
                        }
                        //ставити на паузу, не роздавати
                        Libtorrent.Libtorrent.StopTorrent(torrent);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        Libtorrent.Libtorrent.Close();
                    }
                });
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