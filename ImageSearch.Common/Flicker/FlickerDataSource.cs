namespace SearchTool.Common
{
    /// <summary>
    /// Data source details specific to flicker
    /// </summary>
    public class FlickerDataSource : IDataSource
    {
        public string DataSourceName { get; } = "Flickr";
        public string DataSourceURI { get; } = "https://www.flickr.com/services/feeds/photos_public.gne";
        public bool IsSelected { get; set; } = false;
        public DataSources DataSourceEnum { get; set; } = DataSources.Flicker;
    }
}
