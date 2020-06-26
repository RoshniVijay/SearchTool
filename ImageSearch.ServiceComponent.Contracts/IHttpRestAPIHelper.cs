using ImageSearch.DataModel.Contracts;
using System.Threading.Tasks;

namespace ImageSearch.ServiceComponent.Contracts
{ 
    /// <summary>
    /// IHttpRestApi header
    /// </summary>
    public interface IHttpRestAPIHelper
    {
        Task<IHttpAPIResponse> Get(string uri);
    }
}