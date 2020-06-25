using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using ImageSearch.ServiceComponent.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ImageSearch.ViewModel
{
    public partial class ImageSearchViewModel : INotifyPropertyChanged
    {       
        private string imageSearchQuery = "Nature";
        public event PropertyChangedEventHandler PropertyChanged;
       
        public ObservableCollection<string> m_DataSourceCollection;
        ApplicationConfiguration m_AppConfig;
        
        private string m_CurrentSelectedDataSource;
        AbstractServiceComponentFactory m_ServiceComponentFactory = new ServiceComponentFactory();
        
        public ICommand m_searchCommand;

        public ImageSearchViewModel()
        {
            m_Size = new Size();
            m_Size.Height = 128;
            m_Size.Width = 128;
            m_AppConfig = new ApplicationConfiguration();

            m_DataSourceCollection = new ObservableCollection<string>();
            
            foreach (IDataSource ds in m_AppConfig.AvailableDataSources())
            {
                m_DataSourceCollection.Add(ds.DataSourceName);
                if(ds.IsSelected)
                {
                    m_CurrentSelectedDataSource = ds.DataSourceName;
                }
            }

            OnPropertyChange("CurrentSelection");

            m_DisplayOptions = new ObservableCollection<string>();
            m_DisplayOptions.Add("Small");
            m_DisplayOptions.Add("Medium");
            m_DisplayOptions.Add("Large");

            OnPropertyChange("ThumbnailDisplayOptions");
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

       
        public string CurrentSelectedDataSource
        {
            get { return m_CurrentSelectedDataSource; }
            set
            {
                m_CurrentSelectedDataSource = value;
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

        public string ImageSearchQuery
        {
            get { return imageSearchQuery; }
            set
            {
                if (imageSearchQuery != value)
                {
                    imageSearchQuery = value;
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
                m_searchCommand.Execute(null);
                OnPropertyChange("SearchCommand");
            }
        }

        private async void Search()
        {
            IServiceComponent serviceComponent = m_ServiceComponentFactory.CreateSingleton(m_AppConfig.CurrentDataSourceSelection);
            IQueryContext queryContext = new QueryContext();
            queryContext.ApplicationConfiguration = m_AppConfig;
            queryContext.QueryParam = ImageSearchQuery;
            IResponseContext respContext = await serviceComponent.PerformSearch(queryContext);
            PopulateFlickerDataFields(respContext);
            PopulateNewsAPIDataFields(respContext);
        }
    }
}
