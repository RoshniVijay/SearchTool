namespace ImageSearch.Common
{
    /// <summary>
    /// Interface to return the response from service component
    /// </summary>
    public interface IResponseContext
    {
        string Status { get; set; }
    }
}
