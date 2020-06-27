using SearchTool.Common;
using SearchTool.DataModel;
using SearchTool.DataModel.Contracts;
using System.Collections.ObjectModel;

namespace SearchTool.ViewModel
{
    /// <summary>
    /// View model partial class fot News API display
    /// </summary>
    public partial class ImageSearchViewModel
    {
        #region private variables

        private ObservableCollection<Articles> m_NewsData;

        #endregion private variables

        #region Init

        private void InitializeNewsAPIData()
        {
            m_NewsData = new ObservableCollection<Articles>();
        }

        #endregion Init

        #region public properties

        /// <summary>
        /// Response for news items display. Has members for title, description, link etc
        /// </summary>
        public ObservableCollection<Articles> NewsItemsResponseCollection
        {
            get
            {
                return m_NewsData;
            }
            set
            {
                m_NewsData = value;
                OnPropertyChange("NewsItemsResponseCollection");
            }
        }

        #endregion public properties

        #region private methods

        /// <summary>
        /// Populates the response to viewmodel properties that are relevant to news display
        /// </summary>
        /// <param name="response"></param>
        private void PopulateNewsAPIDataFields(IResponseContext response)
        {
            if (response is TextResponseDataModel respContext)
            {
                m_NewsData = respContext.NewsItems;
                OnPropertyChange("NewsItemsResponseCollection");

                //Update any status - error etc
                m_Status = respContext.Status;
                OnPropertyChange("Status");
            }

        }
        #endregion
    }
}
