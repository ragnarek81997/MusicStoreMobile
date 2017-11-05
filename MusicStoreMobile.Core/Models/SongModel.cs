using MusicStoreMobile.Core.Models.Link;
using System;
using System.Collections.Generic;

namespace MusicStoreMobile.Core.Models
{
    public class SongModel : BaseEntity
    {
        public string Name { get; set; }
        public string ArtistId { get; set; }
        public string AlbumId { get; set; }
        public string GenreId { get; set; }
        public string ArtUrl { get; set; }
        public List<LinkModel> Links { get; set; }
    }
}
