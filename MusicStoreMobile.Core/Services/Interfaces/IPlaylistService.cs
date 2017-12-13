using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Interfaces
{
    public interface IPlaylistService
    {
        Task<ServiceResult<PlaylistModel>> Get(string id);
        Task<ServiceResult<List<PlaylistModel>>> GetMany(int skip, int take);
        Task<ServiceResult<List<PlaylistModel>>> GetMany(string searchQuery, int skip, int take);
        Task<ServiceResult<PlaylistResultModel>> Add(PlaylistResultModel model);
        Task<ServiceResult<PlaylistResultModel>> Update(PlaylistResultModel model);
    }
}
