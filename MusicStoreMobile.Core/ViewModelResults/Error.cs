using System;
using MusicStoreMobile.Core.Enums;

namespace MusicStoreMobile.Core.ViewModelResults
{
    public class Error
    {
		public ErrorStatusCode Code { get; set; }
		public string Description { get; set; }
    }
}
