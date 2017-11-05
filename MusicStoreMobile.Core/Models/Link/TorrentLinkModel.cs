using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Models.Link
{
    public class TorrentLinkModel : LinkModel
    {
        public TorrentInfoModel Info { get; set; }
        string Announce { get; set; }
    }
}
