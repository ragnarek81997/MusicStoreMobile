﻿using MusicStoreMobile.Core.Models.Link;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreMobile.Core.Models
{
    public class SongModel : BaseEntity
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public AlbumModel Album { get; set; }
        [StringLength(24, MinimumLength = 24)]
        public string ArtId { get; set; }

        public ICollection<LinkModel> Links { get; set; }
        [Required]
        public ICollection<ArtistModel> Artists { get; set; }
        [Required]
        public ICollection<GenreModel> Genres { get; set; }
    }
}
