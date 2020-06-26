using System;
using System.Collections.Generic;
using System.Linq;
using ImageSearch.Common;
using ImageSearch.ServiceComponent.Contracts;

namespace ImageSearch.ServiceComponent
{
    /// <summary>
    /// Factory to create service component.
    /// </summary>
    public class ServiceComponentFactory : IAbstractServiceComponentFactory
    {
        /// <summary>
        /// creates object first time and caches it
        /// </summary>
        private readonly Dictionary<DataSources, IServiceComponent> myServiceComponentPool = new Dictionary<DataSources, IServiceComponent>();

        /// <summary>
        /// Create the servicecomponent instance based on datasource passed. If already instantiates, it will be returned.
        /// Not thread safe. 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
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
