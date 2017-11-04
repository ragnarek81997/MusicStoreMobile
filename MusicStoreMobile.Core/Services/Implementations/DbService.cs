using Akavache;
using MusicStoreMobile.Core.Enums;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Implementations
{
    class DbService : IDbService
    {
        #region get blobCache object
        private IBlobCache GetBlobCache(BlobCacheType blobCache = BlobCacheType.LocalMachine)
        {
            IBlobCache blobCacheObject = BlobCache.LocalMachine;
            switch (blobCache)
            {
                case BlobCacheType.InMemory:
                    blobCacheObject = BlobCache.InMemory;
                    break;
                case BlobCacheType.Secure:
                    blobCacheObject = BlobCache.Secure;
                    break;
                case BlobCacheType.UserAccount:
                    blobCacheObject = BlobCache.UserAccount;
                    break;
            }
            return blobCacheObject;
        }
        #endregion get blobCache object

        public async Task<ServiceResult> CheckDb(BlobCacheType blobCache = BlobCacheType.LocalMachine)
        {
            var serviceResult = new ServiceResult();

            var allKeysResult = await GetAllKeys(blobCache);

            serviceResult.Success = allKeysResult.Success && allKeysResult.Result.Any();

            return serviceResult;
        }

        public async Task<ServiceResult<IEnumerable<string>>> GetAllKeys(BlobCacheType blobCache = BlobCacheType.LocalMachine)
        {
            var serviceResult = new ServiceResult<IEnumerable<string>>();
            try
            {
                var result = await GetBlobCache(blobCache).GetAllKeys();
                serviceResult.Success = true;
                serviceResult.Result = result;
            }
            catch (Exception ex)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = ex.Message;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<T>> GetObject<T>(string token, BlobCacheType blobCache = BlobCacheType.LocalMachine)
        {
            var serviceResult = new ServiceResult<T>();
            try
            {
                var result = await GetBlobCache(blobCache).GetObject<T>(token);
                serviceResult.Success = true;
                serviceResult.Result = result;
            }
            catch (Exception ex)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = ex.Message;
            }

            return serviceResult;
        }

        public async Task<ServiceResult> RemoveObject(string token, BlobCacheType blobCache = BlobCacheType.LocalMachine)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var result = await GetBlobCache(blobCache).Invalidate(token);
                serviceResult.Success = true;
            }
            catch (Exception ex)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = ex.Message;
            }

            return serviceResult;
        }

        public async Task<ServiceResult> SaveObject<T>(T obj, string token, BlobCacheType blobCache = BlobCacheType.LocalMachine)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var result = await GetBlobCache(blobCache).InsertObject(token, obj);
                serviceResult.Success = true;
            }
            catch (Exception ex)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = ex.Message;
            }

            return serviceResult;
        }
    }
}
