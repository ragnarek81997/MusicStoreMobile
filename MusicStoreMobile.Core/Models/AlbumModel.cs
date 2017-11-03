using MusicStoreMobile.Core.Enums;
using System;
namespace MusicStoreMobile.Core.Models
{
    public class AlbumModel : BaseEntity
    {
        public string Name { get; set; }
        public string ArtUrl { get; set; }
        public string ArtistId { get; set; }
        public string GenreId { get; set; }
    }
}
