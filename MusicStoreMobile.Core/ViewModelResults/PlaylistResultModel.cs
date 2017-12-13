using MusicStoreMobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModelResults
{
    public class PlaylistResultModel : PlaylistModel
    {
        public new ICollection<string> Songs { get; set; }
    }
}
