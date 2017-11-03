using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using MvvmCross.Platform.Platform;
using MusicStoreMobile.Core.Rest.Interfaces;
using MusicStoreMobile.Core.ViewModelResults;
using MusicStoreMobile.Core.Enums;
using System.Net.Http.Headers;
using Plugin.Connectivity;

namespace MusicStoreMobile.Core.Rest.Implementations
{
    public class RestClient : IRestClient
    {
        private readonly IMvxJsonConverter _jsonConverter;

        public RestClient(IMvxJsonConverter jsonConverter)
        {
            _jsonConverter = jsonConverter;
        }

        public async Task<ServiceResult<TResult>> MakeApiCall<TResult>(string url, HttpMethod method, object data = null, ByteArrayContent byteData = null, string accessToken = "") where TResult : class
        {
            var serviceResult = new ServiceResult<TResult>();

            if (CrossConnectivity.Current.IsConnected)
            {
                url = url.Replace("http://", "https://");

                using (var httpClient = new HttpClient(new NativeMessageHandler { UseCookies = false }))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = method })
                    {
                        // add content
                        if (method != HttpMethod.Get)
                        {
                            using (var multipartFormDataContent = new MultipartFormDataContent())
                            {
                                var json = _jsonConverter.SerializeObject(data);
                                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

								if (byteData != null)
								{
                                    multipartFormDataContent.Add(stringContent, "model");
                                    multipartFormDataContent.Add(byteData, "file", "file.data");
                                    request.Content = multipartFormDataContent;
                                }
                                else
                                {
                                    request.Content = stringContent;
                                }
							}
                        }

                        HttpResponseMessage response = new HttpResponseMessage();
                        try
                        {
                            response = await httpClient.SendAsync(request).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            serviceResult.Success = false;
                            serviceResult.Error.Description = ex.Message;
                            serviceResult.Error.Code = ErrorStatusCode.Empty;
                        }

                        var stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                var resultObject = _jsonConverter.DeserializeObject<TResult>(stringSerialized);

                                serviceResult.Success = true;
                                serviceResult.Result = resultObject;
                            }
                            catch (Exception ex)
                            {
                                serviceResult.Success = false;
                                serviceResult.Error.Description = ex.Message;
                                serviceResult.Error.Code = Enums.ErrorStatusCode.Empty;
                            }
                        }
                        else
                        {
                            serviceResult.Success = false;
                            serviceResult.Error.Description = stringSerialized;
                            serviceResult.Error.Code = (ErrorStatusCode)response.StatusCode;
                        }
                    }
                }
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.Error.Description = "Check internet connection.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult> MakeApiCall(string url, HttpMethod method, object data = null, ByteArrayContent byteData = null, string accessToken = "")
        {
            var serviceResult = new ServiceResult();

            var result = await MakeApiCall<object>(url, method, data, byteData, accessToken);

            serviceResult.Success = result.Success;
            if(!serviceResult.Success)
                serviceResult.Error = result.Error;

            return serviceResult;
        }
    }
}
