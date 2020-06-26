using ImageSearch.DataModel.Contracts;

namespace ImageSearch.DataModel
{
    /// <summary>
    /// Tab item header to dynamically update bsed on selection
    /// </summary>
    public class TabItem
    {
        public string Header { get; set; }
    }

    /// <summary>
    /// Response from rest client with status of HTTPcall
    /// </summary>
    public class HTTPAPIResponse : IHttpAPIResponse
    {
        public ErrorCodes Code { get; set; }
        public string ResponseString { get; set; }
    }
}
