using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.ViewModelResults
{
    public class FileContent : ByteArrayContent
    {
        public FileContent(byte[] contentBytes, string contentType) : base(contentBytes)
        {
            Headers.ContentType = new MediaTypeHeaderValue(contentType);
        }
    }
}
