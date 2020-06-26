using ImageSearch.Common;

namespace ImageSearch.DataModel.Contracts
{
    /// <summary>
    /// Query context with search string etc passed to the rest client
    /// </summary>
    public interface IQueryContext
    {
        string QueryParam { get; set; }
        ApplicationConfiguration ApplicationConfiguration { get; set; }
    }
}
