using SearchTool.Common;
using System.Threading.Tasks;

namespace SearchTool.SearchComponent.Contracts
{ 
    /// <summary>
    /// IHttpRestApi header
    /// </summary>
    public interface ICommunicationHelper
    {
        Task<IHttpAPIResponse> Get(string uri);
    }
}