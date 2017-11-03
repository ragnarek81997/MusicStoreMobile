using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<ApplicationUserModel>> Login(string email, string password);
        Task<ServiceResult> Logout();
        Task<ServiceResult> Authorize();
        Task<ServiceResult> Register(ApplicationUserModel applicationUser);
        Task<ServiceResult> ChangePassword(string currentPassword, string newPassword, string confirmedPassword);

    }
}
