using ImageSearch.DataModel.Contracts;
using ImageSearch.Common;

namespace ImageSearch.DataModel
{
    public class QueryContext : IQueryContext
    {
        public string QueryParam { get; set; }
        public ApplicationConfiguration ApplicationConfiguration { get; set; }

    }
}
