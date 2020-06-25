namespace ImageSearch.DataModel.Contracts
{
    public interface IHttpAPIResponse
    {
        string ResponseString { get; set; }
        ErrorCodes Code { get; set; }
    }

    public enum ErrorCodes
    {
        NoError,
        APIErrorResponse,
        IternalException
    }
}