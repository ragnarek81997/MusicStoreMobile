using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Converters
{
    public class DateTimeJsonConverter : IsoDateTimeConverter
    {
        public DateTimeJsonConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
