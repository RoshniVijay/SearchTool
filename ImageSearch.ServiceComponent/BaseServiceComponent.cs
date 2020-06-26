using ImageSearch.Common;
using ImageSearch.ServiceComponent.Contracts;
using ImageSearch.ServiceComponent.Utilities;
using System.Threading.Tasks;

namespace ImageSearch.ServiceComponent
{
    /// <summary>
    /// Component to make specific query to Flicker server
    /// </summary>
    public abstract class AbstractServiceComponent : IServiceComponent
    {
        protected readonly IHttpRestAPIHelper m_HttpAPIHelper;

        protected AbstractServiceComponent()
        {
            m_HttpAPIHelper = new HttpRestAPIHelper();
        }

        public AbstractServiceComponent(IHttpRestAPIHelper httpHelper)
        {
            m_HttpAPIHelper = httpHelper;
        }

        public abstract Task<IResponseContext> PerformSearch(IQueryContext queryContext);
    }
}
