using ImageSearch.DataModel.Contracts;

namespace ImageSearch.Common
{
    /// <summary>
    /// API response from restclients to caller
    /// </summary>
    public interface IHttpAPIResponse
    {
        string ResponseString { get; set; }
        ErrorCodes Code { get; set; }
    }
}