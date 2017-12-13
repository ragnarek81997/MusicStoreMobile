using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Models
{
    public class PlaylistModel : BaseEntity
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [StringLength(24, MinimumLength = 24)]
        public string ArtId { get; set; }

        public virtual ApplicationUserModel Owner { get; set; }
        public virtual ICollection<SongModel> Songs { get; set; }
    }
}
