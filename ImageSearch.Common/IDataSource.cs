
namespace ImageSearch.Common
{
    /// <summary>
    /// Interface for all the datasources supported - flicker/newsAPI etc
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// is this the currently selected option by user
        /// </summary>
        bool IsSelected { get; set; }
        /// <summary>
        /// Data source Name 
        /// </summary>
        string DataSourceName { get;}
        /// <summary>
        /// URL to query
        /// </summary>
        string DataSourceURI { get; }
        /// <summary>
        /// Enum for data source name
        /// </summary>
        DataSources DataSourceEnum { get; set; }
    }
}
