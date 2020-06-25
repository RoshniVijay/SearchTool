using ImageSearch.DataModel.Contracts;
using System.Threading.Tasks;

namespace ImageSearch.ServiceComponent.Contracts
{
    public interface IServiceComponent
    {
        Task<IResponseContext> PerformSearch(IQueryContext queryContext);
    }
}