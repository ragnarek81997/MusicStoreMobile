using MusicStoreMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModelResults
{
    public class SongResultModel : SongModel
    {
        public new ICollection<string> Links { get; set; }
        [Required]
        public new ICollection<string> Artists { get; set; }
        [Required]
        public new ICollection<string> Genres { get; set; }
    }
}
