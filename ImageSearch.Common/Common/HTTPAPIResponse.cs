using ImageSearch.DataModel.Contracts;

namespace ImageSearch.Common
{
    /// <summary>
    /// Response from rest client with status of HTTPcall
    /// </summary>
    public class HTTPAPIResponse : IHttpAPIResponse
    {
        public ErrorCodes Code { get; set; }
        public string ResponseString { get; set; }
    }
}