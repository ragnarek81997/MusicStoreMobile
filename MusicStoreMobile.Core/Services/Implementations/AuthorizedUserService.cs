﻿using MusicStoreMobile.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStoreMobile.Core.Models;
using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.Enums;

namespace MusicStoreMobile.Core.Services.Implementations
{
    public class AuthorizedUserService : IAuthorizedUserService
    {
        private readonly IAkavacheDbService _dbService;

        private string _authorizedUserToken;

        public AuthorizedUserService(IAkavacheDbService dbService)
        {
            _dbService = dbService;

            _authorizedUserToken = Constants.DbTokens.AuthorizedUser;
        }

        public async Task<ServiceResult<ApplicationUserModel>> Get()
        {
            var serviceResult = new ServiceResult<ApplicationUserModel>();

            var dbServieResult = await _dbService.GetObject<ApplicationUserModel>(_authorizedUserToken, BlobCacheType.Secure);

            serviceResult.Success = dbServieResult.Success;

            if (serviceResult.Success)
            {
                serviceResult.Result = dbServieResult.Result;
            }
            else
            {
                serviceResult.Error = dbServieResult.Error;
            }

            return serviceResult;
        }

        public async Task<ServiceResult> Remove()
        {
            var serviceResult = new ServiceResult();

            var dbServieResult = await _dbService.RemoveObject(_authorizedUserToken, BlobCacheType.Secure);

            serviceResult.Success = dbServieResult.Success;

            if (!serviceResult.Success)
            {
                serviceResult.Error = dbServieResult.Error;
            }

            return serviceResult;
        }

        public async Task<ServiceResult> Set(ApplicationUserModel authorizedUser)
        {
            var serviceResult = new ServiceResult();

            var dbServieResult = await _dbService.SaveObject(authorizedUser, _authorizedUserToken, BlobCacheType.Secure);

            serviceResult.Success = dbServieResult.Success;

            if (!serviceResult.Success)
            {
                serviceResult.Error = dbServieResult.Error;
            }

            return serviceResult;
        }
    }
}
