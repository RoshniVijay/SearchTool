using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearch.Common
{
    /// <summary>
    /// Data source details specific to flicker
    /// </summary>
    public class FlickerDataSource : IDataSource
    {
        public string DataSourceName { get; } = "Flicker";
        public string DataSourceURI { get; } = "https://www.flickr.com/services/feeds/photos_public.gne";
        public bool IsSelected { get; set; } = false;
        public DataSources DataSourceEnum { get; set; } = DataSources.Flicker;
    }

    /// <summary>
    /// Data source details specific to flicker
    /// </summary>
    public class NewsAPIDataSource : IDataSource
    {
        public string DataSourceName { get; } = "NewsAPI";
        public string DataSourceURI { get; } = "http://newsapi.org/v2/everything";
        public bool IsSelected { get; set; } = false;
        public DataSources DataSourceEnum { get; set; } = DataSources.NewsAPI;
    }

    /// <summary>
    /// Enums
    /// </summary>
    public enum DataSources
    {
        Flicker,
        NewsAPI
    }
}
