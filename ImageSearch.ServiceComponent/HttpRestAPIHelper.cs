using ImageSearch.DataModel.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using ImageSearch.DataModel;
using System.Collections.ObjectModel;
using ImageSearch.Common;
using System.IO;

namespace ImageSearch.ServiceComponent.Utilities
{
    public static class HttpRestAPIHelper
    {
        private static HttpClient client = new HttpClient();
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
