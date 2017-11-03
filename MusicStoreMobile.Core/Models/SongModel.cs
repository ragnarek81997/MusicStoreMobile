using System;
namespace MusicStoreMobile.Core.Models
{
    public class SongModel : BaseEntity
    {
        public string Name { get; set; }
        public string ArtistId { get; set; }
        public string AlbumId { get; set; }
        public string GenreId { get; set; }
        public string Url { get; set; }
    }
}
