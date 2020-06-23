using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using ImageSearch.ServiceComponent.Contracts;
using ImageSearch.ServiceComponent.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageSearch.ServiceComponent.APIs
{
    public class SearchServiceComponent : ISearchServiceComponent
    {
        private ApplicationConfiguration _applicationConfiguration;

        public SearchServiceComponent(ApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

        public async Task<IResponseContext> PerformImageSearch(IQueryContext queryContext)
        {
            List<string> resp = await HttpHelper.Get(queryContext);
            IResponseContext respContext = new ImageResponseData();
            respContext.ImageThumbnail = resp;
            return respContext;
        }
    }
}
