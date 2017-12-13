using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Interfaces
{
    public interface ISongService
    {
        Task<ServiceResult<SongModel>> Get(string id);
        Task<ServiceResult<List<SongModel>>> GetMany(string searchQuery, int skip, int take);
        Task<ServiceResult<List<SongModel>>> GetMany(int skip, int take);
        Task<ServiceResult<SongResultModel>> Add(SongResultModel model);
        Task<ServiceResult<SongResultModel>> Update(SongResultModel model);
    }
}
