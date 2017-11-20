using MusicStoreMobile.Core.Enums;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Interfaces
{
    public interface IDictionaryDbService
    {
        Task<ServiceResult> CheckDb();
        Task<ServiceResult<IEnumerable<string>>> GetAllKeys();
        Task<ServiceResult<T>> GetObject<T>(string token);
        Task<ServiceResult> RemoveObject(string token);
        Task<ServiceResult> SaveObject<T>(T obj, string token);
    }
}
