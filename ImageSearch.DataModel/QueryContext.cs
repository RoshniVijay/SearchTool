using ImageSearch.DataModel.Contracts;

namespace ImageSearch.DataModel
{
    public class QueryContext : IQueryContext
    {
        public string QueryParam { get; set; }
    }
}
