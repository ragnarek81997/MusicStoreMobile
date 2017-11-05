using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Models.Link
{
    public class TorrentInfoModel
    {
        public int PieceLength { get; set; }
        public string Pieces { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
    }
}
