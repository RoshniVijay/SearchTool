using ImageSearch.DataModel;
using ImageSearch.DataModel.Contracts;
using ImageSearch.ServiceComponent.APIs;
using ImageSearch.ServiceComponent.Contracts;
using System.Collections.ObjectModel;

namespace ImageSearch.ViewModel
{
    public class TextSearchViewModel : BaseDataModel
    {
        private ObservableCollection<string> m_DisplayOptions;

        public TextSearchViewModel()
        {
           
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

        public string HTMLContent { get; set; }


         public override async void OnSearchCommand()
        {
            ISearchServiceComponent serviceComponent = new SearchServiceComponent();
            IResponseContext respContext = await serviceComponent.PerformSearch(DataSources.Twitter);

            HTMLContent = respContext.ToString();
           

            OnPropertyChange("HTMLContent");
        }

    }

 
}
