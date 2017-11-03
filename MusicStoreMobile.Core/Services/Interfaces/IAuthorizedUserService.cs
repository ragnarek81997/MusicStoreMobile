using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Interfaces
{
    public interface IAuthorizedUserService
    {
        Task<ServiceResult<ApplicationUserModel>> Get();
        Task<ServiceResult> Set(ApplicationUserModel authorizedUser);
        Task<ServiceResult> Remove();
    }
}
