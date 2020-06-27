using SearchTool.Common;
using System.Threading.Tasks;

namespace SearchTool.SearchComponent.Contracts
{
    public interface ISearchComponent
    {
        Task<IResponseContext> PerformSearch(IQueryContext queryContext);
    }
}