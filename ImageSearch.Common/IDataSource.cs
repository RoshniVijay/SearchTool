
namespace ImageSearch.Common
{
    public interface IDataSource
    {
        bool IsSelected { get; set; }
        string DataSourceName { get;}
    }
}
