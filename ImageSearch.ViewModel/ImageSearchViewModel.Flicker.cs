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
        private Size m_Size;
        public ObservableCollection<ListViewDataModel> m_ImageResponseURICollection;
        private ObservableCollection<string> m_DisplayOptions;


        public void InitializeFlickerData()
        {
            m_Size = new Size();
            m_Size.Height = 128;
            m_Size.Width = 128;


            m_DisplayOptions = new ObservableCollection<string>();
            m_DisplayOptions.Add("Small");
            m_DisplayOptions.Add("Medium");
            m_DisplayOptions.Add("Large");

            OnPropertyChange("ThumbnailDisplayOptions");

        }

        public Size ImageSize
        {
            get { return m_Size; }
            set
            {
                if (m_Size != value)
                {
                    m_Size = value;
                    OnPropertyChange("ImageSize");

                    OnPropertyChange("ImageResponseURICollection");
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
        private void PopulateFlickerDataFields(IResponseContext response)
        {

            ImageResponseDataModel respContext = response as ImageResponseDataModel;
            if (respContext != null)
            {
                m_ImageResponseURICollection = new ObservableCollection<ListViewDataModel>();

                m_ImageResponseURICollection = new ObservableCollection<ListViewDataModel>();

                foreach (string str in respContext.URI)
                {
                    ListViewDataModel lc = new ListViewDataModel();
                    lc.URI = str;
                    m_ImageResponseURICollection.Add(lc);
                }
                OnPropertyChange("ImageResponseURICollection");
            }

        }
    }
}
