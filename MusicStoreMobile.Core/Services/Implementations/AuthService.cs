using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.Rest.Interfaces;
using MusicStoreMobile.Core.Services.Interfaces;
using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace MusicStoreMobile.Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IRestClient _restClient;
        private readonly IAuthorizedUserService _authorizedUserService;

        private string _ipServerPort;

        public AuthService(IRestClient restClient, IAuthorizedUserService authorizedUserService)
        {
            _restClient = restClient;
            _authorizedUserService = authorizedUserService;

            _ipServerPort = Constants.IpServerPort;
        }

        public async Task<ServiceResult<ApplicationUserModel>> Login(string email, string password)
        {
            var serviceResult = new ServiceResult<ApplicationUserModel>();

            await Task.Delay(5000);

            serviceResult.Success = false;
            serviceResult.Error.Description = "12345";
            return serviceResult;

            var restUrl = $"{_ipServerPort}v1/token";

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("grant_type", "password"));
            keyValues.Add(new KeyValuePair<string, string>("username", email));
            keyValues.Add(new KeyValuePair<string, string>("password", password));
            var content = new FormUrlEncodedContent(keyValues);

            var restServiceResult = await _restClient.MakeApiCall<ApplicationUserModel>(restUrl, HttpMethod.Post, content);

            serviceResult.Success = restServiceResult.Success;

            if (restServiceResult.Success)
            {
                var authorizedUser = restServiceResult.Result;
                var setAuthorizedUserServiceResult = await _authorizedUserService.Set(authorizedUser);

                serviceResult.Success = setAuthorizedUserServiceResult.Success;

                if (setAuthorizedUserServiceResult.Success)
                {
                    serviceResult.Result = authorizedUser;
                }
                else
                {
                    serviceResult.Error = setAuthorizedUserServiceResult.Error;
                }
            }
            else
            {
                serviceResult.Error = restServiceResult.Error;
            }

            return serviceResult;
        }

        public async Task<ServiceResult> Logout()
        {
            var serviceResult = new ServiceResult();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            serviceResult.Success = getAuthorizedUserServiceResult.Success;

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}v1/account/Logout";

                var restServiceResult = await _restClient.MakeApiCall(restUrl, HttpMethod.Post, accessToken: authorizedUser.AccessToken);

                serviceResult.Success = restServiceResult.Success;

                if (restServiceResult.Success)
                {
                    var removeAuthorizedUserServiceResult = await _authorizedUserService.Remove();

                    serviceResult.Success = removeAuthorizedUserServiceResult.Success;

                    if (!removeAuthorizedUserServiceResult.Success)
                    {
                        serviceResult.Error = removeAuthorizedUserServiceResult.Error;
                    }
                }
                else
                {
                    serviceResult.Error = restServiceResult.Error;
                }
            }
            else
            {
                serviceResult.Error = getAuthorizedUserServiceResult.Error;
            }

            return serviceResult;
        }

        public async Task<ServiceResult> Authorize()
        {
            var serviceResult = new ServiceResult();

            await Task.Delay(500);
            serviceResult.Success = false;
            return serviceResult;

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            serviceResult.Success = getAuthorizedUserServiceResult.Success;

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;
                if (authorizedUser.AccessTokenExpires > DateTime.Now)
                {
                    serviceResult.Success = false;
                    serviceResult.Error.Description = "Token expired.";

                    var removeAuthorizedUserServiceResult = await _authorizedUserService.Remove();

                    if (!removeAuthorizedUserServiceResult.Success)
                    {
                        serviceResult.Error = removeAuthorizedUserServiceResult.Error;
                    }
                }
            }
            else
            {
                serviceResult.Error = getAuthorizedUserServiceResult.Error;
            }

            return serviceResult;
        }

        public async Task<ServiceResult> Register(ApplicationUserModel applicationUser)
        {
            var serviceResult = new ServiceResult();

            await Task.Delay(5000);

            serviceResult.Success = false;
            serviceResult.Error.Description = "12345";
            return serviceResult;


            var restUrl = $"{_ipServerPort}v1/account/register";

            var restServiceResult = await _restClient.MakeApiCall(restUrl, HttpMethod.Post);

            serviceResult.Success = restServiceResult.Success;

            if (!restServiceResult.Success)
            {
                serviceResult.Error = restServiceResult.Error;
            }

            return serviceResult;
        }

        public async Task<ServiceResult> ChangePassword(string currentPassword, string newPassword, string confirmedPassword)
        {
            var serviceResult = new ServiceResult();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            serviceResult.Success = getAuthorizedUserServiceResult.Success;

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}v1/account/changePassword";

                var restServiceResult = await _restClient.MakeApiCall<object>
                (
                    restUrl,
                    HttpMethod.Post,
                    data: new
                    {
                        OldPassword = currentPassword,
                        NewPassword = newPassword,
                        ConfirmPassword = confirmedPassword
                    },
                    accessToken: authorizedUser.AccessToken
                );

                serviceResult.Success = restServiceResult.Success;

                if (!restServiceResult.Success)
                {
                    serviceResult.Error = restServiceResult.Error;
                }
            }
            else
            {
                serviceResult.Error = getAuthorizedUserServiceResult.Error;
            }

            return serviceResult;
        }
    }
}
