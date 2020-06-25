using ImageSearch.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearch.ServiceComponent.Contracts
{
    public class ServiceComponentFactory : AbstractServiceComponentFactory
    {
        Dictionary<DataSources, IServiceComponent> myServiceComponentPool = new Dictionary<DataSources, IServiceComponent>();

        public IServiceComponent CreateSingleton(DataSources dataSource)
        {
            if (myServiceComponentPool.Keys.Contains(dataSource))
            {
                return myServiceComponentPool[dataSource];
            }
            IServiceComponent serviceComponent;
            //else go ahead and add
            switch (dataSource)
            {
                case DataSources.Flicker:
                    serviceComponent = new FlickerSearchServiceComponent();
                    break;

                case DataSources.NewsAPI:
                    serviceComponent = new NewsAPISearchServiceComponent();
                    break;

                default:
                    throw new NotImplementedException("Option is not implemented" + dataSource.ToString());
            }
            myServiceComponentPool.Add(dataSource, serviceComponent);//Add to the pool to avoid creation next time
            return serviceComponent;
        }
    }
}
