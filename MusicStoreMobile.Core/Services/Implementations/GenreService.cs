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
    public class GenreService : IGenreService
    {
        private readonly IRestClient _restClient;
        private readonly IAuthorizedUserService _authorizedUserService;

        private const string _ipServerPort = Constants.IpServerPort;
        private const string _apiControllerRoutePrefix = "v1/genre";

        public GenreService(IRestClient restClient, IAuthorizedUserService authorizedUserService)
        {
            _restClient = restClient;
            _authorizedUserService = authorizedUserService;
        }

        public async Task<ServiceResult<GenreModel>> Get(string id)
        {
            var serviceResult = new ServiceResult<GenreModel>();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}{_apiControllerRoutePrefix}/Get" + "?id=" + id;

                var restServiceResult = await _restClient.MakeApiCall<GenreModel>
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

        public async Task<ServiceResult<List<GenreModel>>> GetMany(int skip, int take)
        {
            var serviceResult = new ServiceResult<List<GenreModel>>();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}{_apiControllerRoutePrefix}/GetMany" + "?skip=" + skip + "&take=" + take;

                var restServiceResult = await _restClient.MakeApiCall<List<GenreModel>>
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

        public async Task<ServiceResult<GenreModel>> Add(GenreModel model)
        {
            var serviceResult = new ServiceResult<GenreModel>();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}{_apiControllerRoutePrefix}/Add";

                var restServiceResult = await _restClient.MakeApiCall<GenreModel>
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

        public async Task<ServiceResult<GenreModel>> Update(GenreModel model)
        {
            var serviceResult = new ServiceResult<GenreModel>();

            var getAuthorizedUserServiceResult = await _authorizedUserService.Get();

            if (getAuthorizedUserServiceResult.Success)
            {
                var authorizedUser = getAuthorizedUserServiceResult.Result;

                var restUrl = $"{_ipServerPort}{_apiControllerRoutePrefix}/Update";

                var restServiceResult = await _restClient.MakeApiCall<GenreModel>
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
    }
}
