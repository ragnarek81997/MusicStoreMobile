using Akavache;
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
        public async Task<ServiceResult> CheckDb()
        {
            var serviceResult = new ServiceResult();

            var allKeysResult = await GetAllKeys();

            serviceResult.Success = allKeysResult.Success && allKeysResult.Result.Any();

            return serviceResult;
        }

        public async Task<ServiceResult<IEnumerable<string>>> GetAllKeys()
        {
            var serviceResult = new ServiceResult<IEnumerable<string>>();
            try
            {
                var result = await BlobCache.LocalMachine.GetAllKeys();
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

        public async Task<ServiceResult<T>> GetObject<T>(string token)
        {
            var serviceResult = new ServiceResult<T>();
            try
            {
                var result = await BlobCache.LocalMachine.GetObject<T>(token);
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

        public async Task<ServiceResult> RemoveObject(string token)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var result = await BlobCache.LocalMachine.Invalidate(token);
                serviceResult.Success = true;
            }
            catch (Exception ex)
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = ex.Message;
            }

            return serviceResult;
        }

        public async Task<ServiceResult> SaveObject<T>(T obj, string token)
        {
            var serviceResult = new ServiceResult();
            try
            {
                var result = await BlobCache.LocalMachine.InsertObject(token, obj);
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
