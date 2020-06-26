using System;
using System.Net.Http;
using System.Threading.Tasks;
using ImageSearch.DataModel;
using ImageSearch.Common;
using ImageSearch.DataModel.Contracts;

namespace ImageSearch.ServiceComponent.Utilities
{
    /// <summary>
    /// Rest client to make GET API call
    /// </summary>
    public static class HttpRestAPIHelper
    {
        private static HttpClient client = new HttpClient();

        /// <summary>
        /// Rest GET call
        /// </summary>
        /// <param name="uri">fully formatted URL</param>
        /// <returns></returns>
        public static async Task<IHttpAPIResponse> Get(string uri)
        {
            HTTPAPIResponse httpResp = new HTTPAPIResponse();
            ErrorCodes errorCode = ErrorCodes.NoError;

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Logger.Log("HTTP call succedded. URL:" + uri);
                    string responseStream = await response.Content.ReadAsStringAsync();
                    httpResp.ResponseString = responseStream;
                }
                else
                {
                    Logger.Log("HTTP call failed with reason:" + response.ReasonPhrase + " for URL:" + uri);
                    errorCode = ErrorCodes.APIErrorResponse;
                }
            }
            catch (Exception exp)
            {
                Logger.Log("Exception while calling HTTP Get for URI" + uri + "Details:" + exp.ToString());
                errorCode = ErrorCodes.IternalException;
            }
            httpResp.Code = errorCode;
            return httpResp;
        }
    }
}
