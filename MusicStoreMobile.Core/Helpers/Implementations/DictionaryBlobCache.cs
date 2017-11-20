using Akavache;
using MusicStoreMobile.Core.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace MusicStoreMobile.Core.Helpers.Implementations
{
    public class DictionaryBlobCache : IDictionaryBlobCache
    {
        private Dictionary<string, object> cache = new Dictionary<string, object>();

        public void Insert(string key, object data)
        {
            lock (cache)
            {
                try
                {
                    cache[key] = data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public object Get(string key)
        {
            lock (cache)
            {
                try
                {
                    return cache[key];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<string> GetAllKeys()
        {
            lock (cache)
            {
                return cache.Select(x => x.Key).ToList();
            }
        }

        public void Invalidate(string key)
        {
            lock (cache)
            {
                cache.Remove(key);
            }
        }

        public void InvalidateAll()
        {
            lock (cache)
            {
                cache.Clear();
            }
        }


        public void InsertObject<T>(string key, T value)
        {
            lock (cache)
            {
                try
                {
                    Insert(key, (object)value);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public T GetObject<T>(string key)
        {
            lock (cache)
            {
                try
                {
                    return (T)(Get(key));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
