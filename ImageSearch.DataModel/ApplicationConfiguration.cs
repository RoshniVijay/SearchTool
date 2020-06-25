
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageSearch.DataModel
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

    public static class ApplicationConfiguration
    {
        private static IDictionary<DataSources, IDataSource> AvailableDataSources = new Dictionary<DataSources, IDataSource>();
        private static bool m_Init = false;
        public static void InitializeApplicationConfiguration()
        {
            if(m_Init)
            {
                return;
            }
            AvailableDataSources.Add(DataSources.Flicker, new FlickerDataSource());
            AvailableDataSources.Add(DataSources.Twitter, new TwitterDataSource());
            SelectDataSource(DataSources.Flicker, true);
            m_Init = true;
           
        }

        public static void SelectDataSource(DataSources dataSource, bool bSelectionFlag)
        {
            AvailableDataSources[dataSource].IsSelected = bSelectionFlag;
        }

        public static ICollection<IDataSource> GetAvailableDataSources()
        {
            return AvailableDataSources.Values;
        }

        public static string QueryString { get; set; }

        public static IDataSource GetDataSource(DataSources dataSource)
        {
            return AvailableDataSources[dataSource];
        }

    }
}
