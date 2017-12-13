using MusicStoreMobile.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreMobile.Core.Models
{
    public class AlbumModel : BaseEntity
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(24, MinimumLength = 24)]
        public string ArtId { get; set; }

        [Required]
        public virtual ICollection<ArtistModel> Artists { get; set; }
        [Required]
        public virtual ICollection<GenreModel> Genres { get; set; }
        public virtual ICollection<SongModel> Songs { get; set; }
    }
}
