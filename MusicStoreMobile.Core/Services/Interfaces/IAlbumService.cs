using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<ServiceResult<AlbumModel>> Get(string id);
        Task<ServiceResult<List<AlbumModel>>> GetMany(int skip, int take);
        Task<ServiceResult<List<AlbumModel>>> GetMany(string searchQuery, int skip, int take);
        Task<ServiceResult<AlbumResultModel>> Add(AlbumResultModel model);
        Task<ServiceResult<AlbumResultModel>> Update(AlbumResultModel model);
    }
}
