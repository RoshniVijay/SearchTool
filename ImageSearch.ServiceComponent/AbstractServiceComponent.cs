using SearchTool.Common;
using SearchTool.SearchComponent.Contracts;
using SearchTool.SearchComponent.Utilities;
using System.Threading.Tasks;

namespace SearchTool.SearchComponent
{
    /// <summary>
    /// Abstract class acting as base class to all the search component
    /// </summary>
    public abstract class AbstractSearchComponent : ISearchComponent
    {
        protected ICommunicationHelper m_HttpAPIHelper;

        protected AbstractSearchComponent()
        {
            //Default communication HTTP. Derived classes can set their own communication method
            m_HttpAPIHelper = new HttpRestAPIHelper();
        }

        /// <summary>
        /// Abstract method to perform search. All derived classes must implement thier custom search
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        public abstract Task<IResponseContext> PerformSearch(IQueryContext queryContext);
    }
}
