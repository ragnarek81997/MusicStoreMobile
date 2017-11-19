using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Models.Link
{
    public abstract class LinkModel : BaseEntity
    {
        [Required]
        public ApplicationUserModel Owner { get; set; }
        [Required]
        public string MimeType { get; set; }
    }
}
