namespace SearchTool.Common
{
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
}