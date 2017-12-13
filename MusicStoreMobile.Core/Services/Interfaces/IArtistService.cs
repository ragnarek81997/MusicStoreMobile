using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Interfaces
{
    public interface IArtistService
    {
        Task<ServiceResult<ArtistModel>> Get(string id);
        Task<ServiceResult<List<ArtistModel>>> GetMany(int skip, int take);
        Task<ServiceResult<List<ArtistModel>>> GetMany(string searchQuery, int skip, int take);
        Task<ServiceResult<ArtistModel>> Add(ArtistModel model);
        Task<ServiceResult<ArtistModel>> Update(ArtistModel model);
    }
}
