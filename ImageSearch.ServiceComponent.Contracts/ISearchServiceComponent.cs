using ImageSearch.DataModel.Contracts;
using System.Threading.Tasks;

namespace ImageSearch.ServiceComponent.Contracts
{
    public interface ISearchServiceComponent
    {
        Task<IResponseContext> PerformImageSearch(IQueryContext queryContext);
    }
}
