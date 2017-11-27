using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Models
{
    public class GenreModel : BaseEntity
    {
        public string Name { get; set; }
    }
}
