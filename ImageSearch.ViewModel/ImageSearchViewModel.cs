using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using ImageSearch.ServiceComponent.APIs;
using ImageSearch.ServiceComponent.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ImageSearch.ViewModel
{
    public class ImageSearchViewModel : INotifyPropertyChanged
    {
        private IResponseContext imageThumbnailData;
        private string imageSearchQuery = "Nature";
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ListViewDataModel> m_ImageResponseURICollection;
        public ObservableCollection<string> m_DataSourceCollection;
        ApplicationConfiguration m_AppConfig;
        private ObservableCollection<string> m_DisplayOptions;
        private ObservableCollection<string> m_URL;
        private ObservableCollection<string> m_TabHeaders;
        

        public ICommand m_searchCommand;

        public ImageSearchViewModel()
        {
            m_AppConfig = new ApplicationConfiguration();

            m_DataSourceCollection = new ObservableCollection<string>();
            m_TabHeaders = new ObservableCollection<string>();
            foreach (IDataSource ds in m_AppConfig.AvailableDataSources())
            {
                m_DataSourceCollection.Add(ds.DataSourceName);
                if(ds.IsSelected)
                {
                    m_TabHeaders.Add(ds.DataSourceName);
                }
            }

            m_DisplayOptions = new ObservableCollection<string>();
            m_DisplayOptions.Add("Small");
            m_DisplayOptions.Add("Medium");
            m_DisplayOptions.Add("Large");
    }


    protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public IResponseContext ImageThumbnailData
        {
            get { return imageThumbnailData; }
            set
            {
                if (imageThumbnailData != value)
                {
                    imageThumbnailData = value;
                    OnPropertyChange("ImageThumbnailData");
                }
            }

        }

        public ObservableCollection<string> ThumbnailDisplayOptions
        {
            get { return m_DisplayOptions; }
            set
            {
                m_DisplayOptions = value;
                OnPropertyChange("ThumbnailDisplayOptions");
            }

        }

        public ObservableCollection<ListViewDataModel> ImageResponseURICollection
        {
            get { return m_ImageResponseURICollection; }
            set
            {
                m_ImageResponseURICollection = value;
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

        public ObservableCollection<string> URL
        {
            get { return m_URL; }
            set
            {

                m_URL = value;
                OnPropertyChange("URL");
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
            

            ISearchServiceComponent serviceComponent = new SearchServiceComponent(m_AppConfig);
            IQueryContext qc = new QueryContext();
            qc.QueryParam = ImageSearchQuery;
            IResponseContext respContext = await serviceComponent.PerformImageSearch(qc);
            m_ImageResponseURICollection = new ObservableCollection<ListViewDataModel>();
            URL = new ObservableCollection<string>();
            foreach (string str in respContext.ImageThumbnail)
            {
                ListViewDataModel listView = new ListViewDataModel();
                listView.URI = str;
                m_ImageResponseURICollection.Add(listView);
                URL.Add(str);
            }
           OnPropertyChange("URL");

           OnPropertyChange("ImageResponseURICollection");
        }
    }
}
