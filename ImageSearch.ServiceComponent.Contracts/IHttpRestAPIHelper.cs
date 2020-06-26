using ImageSearch.DataModel.Contracts;
using System.Threading.Tasks;

namespace ImageSearch.ServiceComponent.Contracts
{ 
    public interface IHttpRestAPIHelper
    {
        Task<IHttpAPIResponse> Get(string uri);
    }
}