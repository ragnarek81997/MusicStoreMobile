using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Interfaces
{
    public interface IGenreService
    {
        Task<ServiceResult<GenreModel>> Get(string id);
        Task<ServiceResult<List<GenreModel>>> GetMany(int skip, int take);
        Task<ServiceResult<GenreModel>> Add(GenreModel model);
        Task<ServiceResult<GenreModel>> Update(GenreModel model);
    }
}
