using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Models
{
    public class PlaylistModel : BaseEntity
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public string ArtUrl { get; set; }
        public List<string> SongsIds { get; set; }
    }
}
