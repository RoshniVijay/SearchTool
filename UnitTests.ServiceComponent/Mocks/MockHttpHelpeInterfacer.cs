using ImageSearch.DataModel.Contracts;
using ImageSearch.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearch.ServiceComponent.Utilities
{
    public static class HttpRestAPIHelper
    {
        public static async Task<IHttpAPIResponse> Get(string uri)
        {
            IHttpAPIResponse resp = new ImageSearch.DataModel.HTTPAPIResponse();
            return resp;
        }
    }
}
