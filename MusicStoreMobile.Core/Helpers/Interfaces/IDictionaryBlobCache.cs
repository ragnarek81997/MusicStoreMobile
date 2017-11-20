using Akavache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Helpers.Interfaces
{
    public interface IDictionaryBlobCache
    {
        void Insert(string key, object data);

        object Get(string key);

        IEnumerable<string> GetAllKeys();

        void Invalidate(string key);

        void InvalidateAll();

        void InsertObject<T>(string key, T value);

        T GetObject<T>(string key);
    }
}
