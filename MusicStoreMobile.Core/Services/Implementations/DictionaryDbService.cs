using Akavache;
using MusicStoreMobile.Core.Enums;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using MusicStoreMobile.Core.Helpers.Implementations;
using MusicStoreMobile.Core.Helpers.Interfaces;

namespace MusicStoreMobile.Core.Services.Implementations
{
    public class DictionaryDbService : IDictionaryDbService
    {
        private readonly IDictionaryBlobCache _dictionaryBlobCache;
        public DictionaryDbService(IDictionaryBlobCache dictionaryBlobCache)
        {
            _dictionaryBlobCache = dictionaryBlobCache;
        }

        public async Task<ServiceResult> CheckDb()
        {
            var serviceResult = new ServiceResult();

            var allKeysResult = await GetAllKeys();

            serviceResult.Success = allKeysResult.Success && allKeysResult.Result.Any();

            return serviceResult;
        }

        public async Task<ServiceResult<IEnumerable<string>>> GetAllKeys()
        {
            return await Task.Run(()=> { 
                var serviceResult = new ServiceResult<IEnumerable<string>>();
                try
                {
                    var result = _dictionaryBlobCache.GetAllKeys();
                    serviceResult.Success = true;
                    serviceResult.Result = result;
                }
                catch (Exception ex)
                {
                    serviceResult.Success = false;
                    serviceResult.Error.Description = ex.Message;
                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult<T>> GetObject<T>(string token)
        {
            return await Task.Run(() => {
                var serviceResult = new ServiceResult<T>();
                try
                {
                    var result = _dictionaryBlobCache.GetObject<T>(token);
                    serviceResult.Success = true;
                    serviceResult.Result = result;
                }
                catch (Exception ex)
                {
                    serviceResult.Success = false;
                    serviceResult.Error.Description = ex.Message;
                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult> RemoveObject(string token)
        {
            return await Task.Run(() => {
                var serviceResult = new ServiceResult();
                try
                {
                    _dictionaryBlobCache.Invalidate(token);
                    serviceResult.Success = true;
                }
                catch (Exception ex)
                {
                    serviceResult.Success = false;
                    serviceResult.Error.Description = ex.Message;
                }

                return serviceResult;
            });
        }

        public async Task<ServiceResult> SaveObject<T>(T obj, string token)
        {
            return await Task.Run(() => {
                var serviceResult = new ServiceResult();
                try
                {
                    _dictionaryBlobCache.InsertObject(token, obj);
                    serviceResult.Success = true;
                }
                catch (Exception ex)
                {
                    serviceResult.Success = false;
                    serviceResult.Error.Description = ex.Message;
                }

                return serviceResult;
                });
        }
    }
}
