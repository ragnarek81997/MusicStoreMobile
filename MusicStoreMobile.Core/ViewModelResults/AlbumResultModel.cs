using MusicStoreMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModelResults
{
    public class AlbumResultModel : AlbumModel
    {
        [Required]
        public new ICollection<string> Artists { get; set; }
        [Required]
        public new ICollection<string> Genres { get; set; }

        public new ICollection<string> Songs { get; set; }
    }
}
