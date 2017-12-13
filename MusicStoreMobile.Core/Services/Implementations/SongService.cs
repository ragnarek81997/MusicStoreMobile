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
    public class SongService : ISongService
    {
        private readonly IRestClient _restClient;
        private readonly IAuthorizedUserService _authorizedUserService;

        private const string _ipServerPort = Constants.IpServerPort;
        private const string _apiControllerRoutePrefix = "v1/song";

        public SongService(IRestClient restClient, IAuthorizedUserService authorizedUserService)
        {
            _restClient = restClient;
            _authorizedUserService = authorizedUserService;
        }

        public async Task<ServiceResult<SongModel>> Get(string id)
        {
            var serviceResult = new ServiceResult<SongModel>();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}{_apiControllerRoutePrefix}/Get" + "?id=" + id;

                var restServiceResult = await _restClient.MakeApiCall<SongModel>
                (
                    restUrl,
                    HttpMethod.Get,
                    accessToken: authorizedUser.AccessToken
                );

                if (!restServiceResult.Success)
                {
                    serviceResult.Error = restServiceResult.Error;
                }
                else
                {
                    serviceResult.Success = true;
                    serviceResult.Result = restServiceResult.Result;
                }
            }
            else
            {
                serviceResult.Error = getAuthorizedUserServiceResult.Error;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<List<SongModel>>> GetMany(int skip, int take)
        {
            var serviceResult = new ServiceResult<List<SongModel>>();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}{_apiControllerRoutePrefix}/GetMany" + "?skip=" + skip + "&take=" + take;

                var restServiceResult = await _restClient.MakeApiCall<List<SongModel>>
                (
                    restUrl,
                    HttpMethod.Get,
                    accessToken: authorizedUser.AccessToken
                );

                if (!restServiceResult.Success)
                {
                    serviceResult.Error = restServiceResult.Error;
                }
                else
                {
                    serviceResult.Success = true;
                    serviceResult.Result = restServiceResult.Result;
                }
            }
            else
            {
                serviceResult.Error = getAuthorizedUserServiceResult.Error;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<SongResultModel>> Add(SongResultModel model)
        {
            var serviceResult = new ServiceResult<SongResultModel>();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}{_apiControllerRoutePrefix}/Add";

                var restServiceResult = await _restClient.MakeApiCall<SongResultModel>
                (
                    restUrl,
                    HttpMethod.Post,
                    data: model,
                    accessToken: authorizedUser.AccessToken
                );

                if (!restServiceResult.Success)
                {
                    serviceResult.Error = restServiceResult.Error;
                }
                else
                {
                    serviceResult.Success = true;
                    serviceResult.Result = restServiceResult.Result;
                }
            }
            else
            {
                serviceResult.Error = getAuthorizedUserServiceResult.Error;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<SongResultModel>> Update(SongResultModel model)
        {
            var serviceResult = new ServiceResult<SongResultModel>();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}{_apiControllerRoutePrefix}/Update";

                var restServiceResult = await _restClient.MakeApiCall<SongResultModel>
                (
                    restUrl,
                    HttpMethod.Post,
                    data: model,
                    accessToken: authorizedUser.AccessToken
                );

                if (!restServiceResult.Success)
                {
                    serviceResult.Error = restServiceResult.Error;
                }
                else
                {
                    serviceResult.Success = true;
                    serviceResult.Result = restServiceResult.Result;
                }
            }
            else
            {
                serviceResult.Error = getAuthorizedUserServiceResult.Error;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<List<SongModel>>> GetMany(string searchQuery, int skip, int take)
        {
            var serviceResult = new ServiceResult<List<SongModel>>();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}{_apiControllerRoutePrefix}/GetMany" + "?searchQuery=" + searchQuery + "&skip=" + skip + "&take=" + take;

                var restServiceResult = await _restClient.MakeApiCall<List<SongModel>>
                (
                    restUrl,
                    HttpMethod.Get,
                    accessToken: authorizedUser.AccessToken
                );

                if (!restServiceResult.Success)
                {
                    serviceResult.Error = restServiceResult.Error;
                }
                else
                {
                    serviceResult.Success = true;
                    serviceResult.Result = restServiceResult.Result;
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
