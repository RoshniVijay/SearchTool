using ImageSearch.Common;

namespace ImageSearch.DataModel.Contracts
{
    public interface IQueryContext
    {
        string QueryParam { get; set; }
        ApplicationConfiguration ApplicationConfiguration { get; set; }
    }
}
