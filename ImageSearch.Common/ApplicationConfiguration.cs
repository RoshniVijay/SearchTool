
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageSearch.Common
{
    public class FlickerDataSource : IDataSource
    {
        private string m_DataSourceName = "Flicker";
        private string m_URI = "https://www.flickr.com/services/feeds/photos_public.gne";
        bool m_bISSelected = false;

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
    }

    public class TwitterDataSource : IDataSource
    {
        private string m_DataSourceName = "Twitter";
        private string m_URI = "https://api.twitter.com/1.1/search/tweets.json";
        bool m_bIsSelected = false;

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
    }

    public enum DataSources
    {
        Flicker,

        Twitter
    }

    public class ApplicationConfiguration
    {
        private IDictionary<DataSources, IDataSource> m_AvailableDataSources = new Dictionary<DataSources, IDataSource>();

        public ApplicationConfiguration()
        {
            m_AvailableDataSources.Add(DataSources.Flicker, new FlickerDataSource());
            m_AvailableDataSources.Add(DataSources.Twitter, new TwitterDataSource());
            SelectDataSource(DataSources.Flicker, true);
        }

        public void SelectDataSource(DataSources dataSource, bool bSelectionFlag)
        {
            m_AvailableDataSources[dataSource].IsSelected = bSelectionFlag;
        }

        public ICollection<IDataSource> AvailableDataSources()
        {
            return m_AvailableDataSources.Values;
        }

    }
}
