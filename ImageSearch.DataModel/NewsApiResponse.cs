using ImageSearch.DataModel.Contracts;
using System;
using System.Collections.Generic;
using System.IO;

namespace ImageSearch.DataModel
{
    public class HTTPAPIResponse : IHttpAPIResponse
    {
        public ErrorCodes Code { get; set; }
        public string ResponseString { get; set; }
    }

    public class NewsApiResponse
    {
        public ErrorCodes? Code { get; set; }
        public string Message { get; set; }
        public List<Articles> Articles { get; set; }
        public int TotalResults { get; set; }
    }

   
}
