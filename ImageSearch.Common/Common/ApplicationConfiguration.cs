using System;
using System.Collections.Generic;

namespace SearchTool.Common
{
    /// <summary>
    /// Application Configuration to store current state of application. 
    /// Holds the available data sources, and details about each. based on user selection, returns appropriate datasource to caller to make the query
    /// </summary>
    public class ApplicationConfiguration
    {
        private readonly IDictionary<DataSources, IDataSource> m_AvailableDataSources = new Dictionary<DataSources, IDataSource>();

        public ApplicationConfiguration()
        {
            m_AvailableDataSources.Add(DataSources.Flicker, new FlickerDataSource());
            m_AvailableDataSources.Add(DataSources.NewsAPI, new NewsAPIDataSource());
            SelectDataSource(DataSources.Flicker, true); //flicker is default
        }

        #region Public APIs
        /// <summary>
        /// Marks the passed datasource as selected.
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="bSelectionFlag"></param>
        private void SelectDataSource(DataSources dataSource, bool bSelectionFlag)
        {
            m_AvailableDataSources[dataSource].IsSelected = bSelectionFlag;
        }

        /// <summary>
        /// returns the specified datasource
        /// </summary>
        /// <param name="dataSourceEnum"></param>
        /// <returns></returns>
        public IDataSource GetDataSource(DataSources dataSourceEnum)
        {
            return m_AvailableDataSources[dataSourceEnum];
        }

        /// <summary>
        /// return all available datasources
        /// </summary>
        /// <returns></returns>
        public ICollection<IDataSource> GetAvailableDataSources()
        {
            return m_AvailableDataSources.Values;
        }

        /// <summary>
        /// Sets and gets current selected datasource 
        /// </summary>
        public DataSources CurrentDataSourceSelection { get; private set; }

        /// <summary>
        /// Sets the current data source based on string passed
        /// </summary>
        /// <param name="dataSource"></param>
        public void SetCurrentDataSourceSelection(string dataSource)
        {
            CurrentDataSourceSelection = GetDataSource(dataSource).DataSourceEnum;
        }

        #endregion Public APIs


        #region Helper

        private IDataSource GetDataSource(string dataSourceName)
        {
            foreach (IDataSource dataSource in m_AvailableDataSources.Values)
            {
                if (dataSource.DataSourceName == dataSourceName)
                    return dataSource;
            }

            throw new Exception("Passed data source does not exist" + dataSourceName);
        }

        #endregion

    }
}
