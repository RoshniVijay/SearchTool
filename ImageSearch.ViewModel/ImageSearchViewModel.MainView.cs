using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using ImageSearch.ServiceComponent.Contracts;

namespace ImageSearch.ViewModel
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
        private ApplicationConfiguration m_AppConfig;
        /// <summary>
        /// Currently selected data source in UI - flicker/twitter/NewsAPI etc
        /// </summary>
        private string m_CurrentSelectedDataSource;
        /// <summary>
        /// AbstractFactory for biz logic component
        /// </summary>
        private AbstractServiceComponentFactory m_ServiceComponentFactory;
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
            m_ServiceComponentFactory = new ServiceComponentFactory();
            m_AppConfig = new ApplicationConfiguration();
            m_DataSourceCollection = new ObservableCollection<string>();
       
            //Initialize other partial classes that has specific view model/Controls for datasources like Flicker/twitter etc
            InitializeFlickerData();
            InitializeNewsAPIData();
        }

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

      

        public string CurrentDataSourceSelection
        {
            get { return m_CurrentSelectedDataSource; }
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



        public ObservableCollection<string> DataSourceCollection
        {
            get
            {
                return m_DataSourceCollection;
            }
            set
            {

                m_DataSourceCollection = value;
                OnPropertyChange("DataSourceCollection");
            }
        }

        
        public string Status
        {
            get { return m_Status; }
            set
            {
                if (m_Status != value)
                {
                    m_Status = value;
                    OnPropertyChange("Status");
                }
            }
        }

        public string ImageSearchQuery
        {
            get { return m_imageSearchQuery; }
            set
            {
                if (m_imageSearchQuery != value)
                {
                    m_imageSearchQuery = value;
                    OnPropertyChange("ImageSearchQuery");
                }
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                if (m_searchCommand == null)
                {
                    m_searchCommand = new SearchCommand(Search);
                }
                return m_searchCommand;
            }
            set
            {
                Status = "Searching ...";
                m_searchCommand.Execute(null);
                OnPropertyChange("SearchCommand");
            }
        }

        private async void Search()
        {
            DateTime curDateTime = DateTime.Now;
            IServiceComponent serviceComponent = m_ServiceComponentFactory.CreateSingleton(m_AppConfig.CurrentDataSourceSelection);
            IQueryContext queryContext = new QueryContext();
            queryContext.ApplicationConfiguration = m_AppConfig;
            queryContext.QueryParam = ImageSearchQuery;
            Status = "Performing Search...";
            IResponseContext respContext = await serviceComponent.PerformSearch(queryContext);
            PopulateFlickerDataFields(respContext);
            PopulateNewsAPIDataFields(respContext);
            TimeSpan performance = DateTime.Now - curDateTime;
            
            Status = String.Format("Completed query in {0}:{1} seconds",  performance.Seconds, performance.Milliseconds);
        }
    }
}
