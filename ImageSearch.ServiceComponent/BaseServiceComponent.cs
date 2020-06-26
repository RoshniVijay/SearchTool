using ImageSearch.ServiceComponent.Contracts;
using ImageSearch.ServiceComponent.Utilities;

namespace ImageSearch.ServiceComponent
{
    /// <summary>
    /// Component to make specific query to Flicker server
    /// </summary>
    public class BaseServiceComponent 
    {
        protected readonly IHttpRestAPIHelper m_HttpAPIHelper;

        protected BaseServiceComponent()
        {
            m_HttpAPIHelper = new HttpRestAPIHelper();
        }

        public BaseServiceComponent(IHttpRestAPIHelper httpHelper)
        {
            m_HttpAPIHelper = httpHelper;
        }
    }
}
