using ImageSearch.Common;
using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using ImageSearch.ServiceComponent.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ImageSearch.ViewModel
{
    public partial class ImageSearchViewModel
    {
        private ObservableCollection<ListViewDataModel> m_URL;


        public void InitializeNewsAPIData()
        {
            m_URL = new ObservableCollection<ListViewDataModel>();
        }


        public ObservableCollection<ListViewDataModel> URL
        {
            get { return m_URL; }
            set
            {

                m_URL = value;
                OnPropertyChange("URL");
            }
        }

        private void PopulateNewsAPIDataFields(IResponseContext response)
        {
            TextResponseDataModel respContext = response as TextResponseDataModel;
            if (respContext != null)
            {
                m_ImageResponseURICollection = new ObservableCollection<ListViewDataModel>();

                foreach (Articles str in respContext.NewsItems)
                {
                    ListViewDataModel lc = new ListViewDataModel();
                    lc.URI = str.Author;
                    m_ImageResponseURICollection.Add(lc);
                }
                OnPropertyChange("ImageResponseURICollection");
            }
        }
    }
}
