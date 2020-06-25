
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageSearch.Common
{
    public class FlickerDataSource : IDataSource
    {
        private string m_DataSourceName = "Flicker";
        private string m_URI = "https://www.flickr.com/services/feeds/photos_public.gne";
        bool m_bISSelected = false;
        DataSources m_Type = DataSources.Flicker;

        public string DataSourceName
        {
            get
            {
                return m_DataSourceName ;
            }
        }
        public string DataSourceURI
        {
            get
            {
                return m_URI;
            }
        }
        public bool IsSelected
        {
            get
            {
                return m_bISSelected; 
            }
            set
            {
                m_bISSelected = value;
            }
               
        }

        public DataSources DataSourceEnum
        {
            get
            {
                return m_Type;
            }
            set
            {
                m_Type = value;
            }
        }
    }

    public class NewsAPIDataSource : IDataSource
    {
        private string m_DataSourceName = "NewsAPI";
        private string m_URI = "http://newsapi.org/v2/everything";
        bool m_bIsSelected = false;
        DataSources m_Type = DataSources.NewsAPI;

        public string DataSourceName
        {
            get
            {
                return m_DataSourceName; ;
            }
        }
        public string DataSourceURI
        {
            get
            {
                return m_URI;
            }
        }
        public bool IsSelected
        {
            get
            {
                return m_bIsSelected;
            }
            set
            {
                m_bIsSelected = value;
            }

        }

        public DataSources DataSourceEnum
        {
            get
            {
                return m_Type;
            }
            set
            {
                m_Type = value;
            }
        }
    }

    public enum DataSources
    {
        Flicker,

        NewsAPI
    }

    public class ApplicationConfiguration
    {
        private IDictionary<DataSources, IDataSource> m_AvailableDataSources = new Dictionary<DataSources, IDataSource>();

        public ApplicationConfiguration()
        {
            m_AvailableDataSources.Add(DataSources.Flicker, new FlickerDataSource());
            m_AvailableDataSources.Add(DataSources.NewsAPI, new NewsAPIDataSource());
            SelectDataSource(DataSources.Flicker, true);
        }

        public void SelectDataSource(DataSources dataSource, bool bSelectionFlag)
        {
            m_AvailableDataSources[dataSource].IsSelected = bSelectionFlag;
        }

        //TODO: can be removed
        public IDataSource GetDataSource(string dataSourceName)
        {
            foreach (IDataSource dataSource in m_AvailableDataSources.Values)
            {
                if (dataSource.DataSourceName == dataSourceName)
                    return dataSource;
            }
            
            throw new Exception("Passes data source does not exist" + dataSourceName);
        }

        public IDataSource GetDataSource(DataSources dataSourceEnum)
        {
            return m_AvailableDataSources[dataSourceEnum];
        }

        public ICollection<IDataSource> AvailableDataSources()
        {
            return m_AvailableDataSources.Values;
        }

        public DataSources CurrentDataSourceSelection { get; set; }

        public void SetCurrentDataSourceSelection(string dataSource)
        {
            CurrentDataSourceSelection = GetDataSource(dataSource).DataSourceEnum;
        }

    }
}
