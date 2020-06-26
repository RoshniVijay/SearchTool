namespace ImageSearch.DataModel.Contracts
{
    /// <summary>
    /// API response from restclients to caller
    /// </summary>
    public interface IHttpAPIResponse
    {
        string ResponseString { get; set; }
        ErrorCodes Code { get; set; }
    }

    /// <summary>
    /// error codes
    /// </summary>
    public enum ErrorCodes
    {
        NoError,
        APIErrorResponse,
        IternalException
    }
}