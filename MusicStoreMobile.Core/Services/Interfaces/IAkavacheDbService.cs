using MusicStoreMobile.Core.Enums;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Interfaces
{
    public interface IAkavacheDbService
    {
        Task<ServiceResult> CheckDb(BlobCacheType blobCache = BlobCacheType.LocalMachine);
        Task<ServiceResult<IEnumerable<string>>> GetAllKeys(BlobCacheType blobCache = BlobCacheType.LocalMachine);
        Task<ServiceResult<T>> GetObject<T>(string token, BlobCacheType blobCache = BlobCacheType.LocalMachine);
        Task<ServiceResult> RemoveObject(string token, BlobCacheType blobCache = BlobCacheType.LocalMachine);
        Task<ServiceResult> SaveObject<T>(T obj, string token, BlobCacheType blobCache = BlobCacheType.LocalMachine);
    }
}
