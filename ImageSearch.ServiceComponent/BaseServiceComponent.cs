using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml;
using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using ImageSearch.ServiceComponent.Contracts;
using ImageSearch.ServiceComponent.Utilities;

namespace ImageSearch.ServiceComponent
{
    /// <summary>
    /// Component to make specific query to Flicker server
    /// </summary>
    public class BaseServiceComponent 
    {
        protected IHttpRestAPIHelper m_HttpAPIHelper;

        public BaseServiceComponent()
        {
            m_HttpAPIHelper = new HttpRestAPIHelper();
        }

        public BaseServiceComponent(IHttpRestAPIHelper httpHelper)
        {
            m_HttpAPIHelper = httpHelper;
        }
    }
}
