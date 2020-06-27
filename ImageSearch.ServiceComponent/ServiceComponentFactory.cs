using System;
using System.Collections.Generic;
using System.Linq;
using SearchTool.Common;
using SearchTool.SearchComponent.Contracts;

namespace SearchTool.SearchComponent
{
    /// <summary>
    /// Factory to create service component.
    /// </summary>
    public class SearchComponentFactory : ISearchComponentFactory
    {
        /// <summary>
        /// creates object first time and caches it
        /// </summary>
        private readonly Dictionary<DataSources, ISearchComponent> mySearchComponentPool = new Dictionary<DataSources, ISearchComponent>();

        /// <summary>
        /// Create the SearchComponent instance based on datasource passed. If already instantiates, it will be returned.
        /// Not thread safe. 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public ISearchComponent CreateSearchComponent(DataSources dataSource)
        {
            if (mySearchComponentPool.Keys.Contains(dataSource))
            {
                return mySearchComponentPool[dataSource];
            }
            ISearchComponent SearchComponent;
            //else go ahead and add
            switch (dataSource)
            {
                case DataSources.Flicker:
                    SearchComponent = new FlickerSearchSearchComponent();
                    break;

                case DataSources.NewsAPI:
                    SearchComponent = new NewsAPISearchhComponent();
                    break;

                default:
                    throw new NotImplementedException("Option is not implemented" + dataSource.ToString());
            }
            mySearchComponentPool.Add(dataSource, SearchComponent);//Add to the pool to avoid creation next time
            return SearchComponent;
        }
    }
}
