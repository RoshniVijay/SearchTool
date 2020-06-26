using ImageSearch.DataModel.Contracts;
using ImageSearch.Common;

namespace ImageSearch.DataModel
{
    /// <summary>
    /// Query data
    /// </summary>
    public class QueryContext : IQueryContext
    {
        public string QueryParam { get; set; }
        public ApplicationConfiguration ApplicationConfiguration { get; set; }

    }
}
