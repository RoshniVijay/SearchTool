﻿
namespace ImageSearch.Common
{
    public interface IDataSource
    {
        bool IsSelected { get; set; }
        string DataSourceName { get;}
        string DataSourceURI { get; }
        DataSources DataSourceEnum { get; set; }
    }
}
