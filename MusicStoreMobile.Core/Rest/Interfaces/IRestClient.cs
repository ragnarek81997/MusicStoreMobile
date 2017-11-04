using MusicStoreMobile.Core.ViewModelResults;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicStoreMobile.Core.Rest.Interfaces
{
    public interface IRestClient
    {
        Task<ServiceResult<TResult>> MakeApiCall<TResult>(string url, HttpMethod method, object data = null, string accessToken = "")
                        where TResult : class;

        Task<ServiceResult> MakeApiCall(string url, HttpMethod method, object data = null, string accessToken = "");
    }
}
