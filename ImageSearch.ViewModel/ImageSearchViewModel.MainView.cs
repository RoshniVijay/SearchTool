using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SearchTool.Common;
using SearchTool.DataModel;
using SearchTool.DataModel.Contracts;
using SearchTool.SearchComponent;
using SearchTool.SearchComponent.Contracts;

namespace SearchTool.ViewModel
{
    /// <summary>
    /// Main View Model class. It acts as the view model for the common controls like search string, search button etc
    /// </summary>
    public partial class ImageSearchViewModel : INotifyPropertyChanged
    {
        #region Implement INotifyPropertyChanged Interface
        /// <summary>
        /// Public event that the view subscribes to keep the View and view model in sync
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Implement INotifyPropertyChanged Interface

        #region Main View Model elements

        /// <summary>
        /// Search string entered in UI
        /// </summary>
        private string m_imageSearchQuery = "Nature";    
        /// <summary>
        /// Source from which the query has to be made
        /// </summary>
        private ObservableCollection<string> m_DataSourceCollection;
        /// <summary>
        /// Application Configuration
        /// </summary>
        private readonly ApplicationConfiguration m_AppConfig;
        /// <summary>
        /// Currently selected data source in UI - flicker/twitter/NewsAPI etc
        /// </summary>
        private string m_CurrentSelectedDataSource;
        /// <summary>
        /// AbstractFactory for biz logic component
        /// </summary>
        private readonly ISearchComponentFactory m_SearchComponentFactory;
        /// <summary>
        /// Search command triggered by search in UI
        /// </summary>
        private ICommand m_searchCommand;
        /// <summary>
        /// Status of query/failure etc
        /// </summary>
        private string m_Status;

        #endregion Main View Model elements

        /// <summary>
        /// Constructor. Called only once during initialization of the view
        /// </summary>
        public ImageSearchViewModel()
        {
            m_SearchComponentFactory = new SearchComponentFactory();
            m_AppConfig = new ApplicationConfiguration();
            m_DataSourceCollection = new ObservableCollection<string>();
            InitializationMainVewData();
            //Initialize other partial classes that has specific view model/Controls for datasources like Flicker/twitter etc
            InitializeFlickerData();
            InitializeNewsAPIData();
        }

        /// <summary>
        /// Initialize main controls common for all view models
        /// </summary>
        private void InitializationMainVewData()
        {
            foreach (IDataSource dataSource in m_AppConfig.GetAvailableDataSources())
            {
                m_DataSourceCollection.Add(dataSource.DataSourceName);
                if(dataSource.IsSelected)
                {
                    m_CurrentSelectedDataSource = dataSource.DataSourceName;
                }
            }
            OnPropertyChange("DataSourceCollection");
            OnPropertyChange("CurrentDataSourceSelection");
        }

        protected void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region properties


        /// <summary>
        /// Currently selected data source - flicker/newsAPI
        /// </summary>
        public string CurrentDataSourceSelection
        {
            get => m_CurrentSelectedDataSource;
            set
            {
                if (m_CurrentSelectedDataSource != value)
                {
                    m_CurrentSelectedDataSource = value;
                    OnPropertyChange("CurrentDataSourceSelection");
                    m_AppConfig.SetCurrentDataSourceSelection(m_CurrentSelectedDataSource);
                }
            }

        }


        /// <summary>
        /// Available Data source collection
        /// </summary>
        public ObservableCollection<string> DataSourceCollection
        {
            get => m_DataSourceCollection;
            set
            {

                m_DataSourceCollection = value;
                OnPropertyChange("DataSourceCollection");
            }
        }

        /// <summary>
        /// Status text
        /// </summary>
        public string Status
        {
            get => m_Status;
            set
            {
                if (m_Status != value)
                {
                    m_Status = value;
                    OnPropertyChange("Status");
                }
            }
        }

        /// <summary>
        /// Query string from UI
        /// </summary>
        public string ImageSearchQuery
        {
            get => m_imageSearchQuery;
            set
            {
                if (m_imageSearchQuery != value)
                {
                    m_imageSearchQuery = value;
                    OnPropertyChange("ImageSearchQuery");
                }
            }
        }

        /// <summary>
        /// Search trigger for executing action
        /// </summary>
        public ICommand SearchCommand
        {
            get { return m_searchCommand ?? (m_searchCommand = new SearchCommand(Search)); }
            set
            {
                Status = "Searching ...";
                m_searchCommand.Execute(null);
                OnPropertyChange("SearchCommand");
            }
        }

        #endregion

        /// <summary>
        /// Perform search
        /// </summary>
        private async void Search()
        {
            DateTime curDateTime = DateTime.Now;
            ISearchComponent SearchComponent = m_SearchComponentFactory.CreateSearchComponent(m_AppConfig.CurrentDataSourceSelection);
            IQueryContext queryContext = new QueryContext();
            queryContext.ApplicationConfiguration = m_AppConfig;
            queryContext.QueryParam = ImageSearchQuery;
            Status = "Performing Search...";
            IResponseContext respContext = await SearchComponent.PerformSearch(queryContext);
            PopulateFlickerDataFields(respContext);
            PopulateNewsAPIDataFields(respContext);
            TimeSpan performance = DateTime.Now - curDateTime;
            
            Status = $"Completed query in {performance.Seconds}:{performance.Milliseconds} seconds";
        }
    }
}
