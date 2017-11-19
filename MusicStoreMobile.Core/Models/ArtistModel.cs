using System;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreMobile.Core.Models
{
    public class ArtistModel : BaseEntity
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(24, MinimumLength = 24)]
        public string ArtId { get; set; }
    }
}
