
namespace ImageSearch.DataModel
{
    public interface IDataSource
    {
        bool IsSelected { get; set; }
        string DataSourceName { get;}
    }
}
